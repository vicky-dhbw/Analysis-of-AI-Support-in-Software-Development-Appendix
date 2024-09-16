public async Task<ResultBase<CadJob>> StartJobAsync(
   CadRequest cadRequest,
   ConfigurationDbStructure configuration,
   VisualisationModes availableVisualisationModes,
   CancellationToken cancellationToken
)
{
   var cadJob = _mapper.Map<CadJob>(cadRequest.Job);
   cadJob.ValidUntil = DateTime.Now.AddDays(1);

   try
   {
       cadRequest.RequestedFormats.ForEach(format => cadJob.Results.Add(new CadJobResult { FileFormat = format, State = ProcessingState.Processing }));

       cadJob = await _cadJobRepository.CreateAsync(cadJob, cancellationToken);
   }
   catch (Exception e)
   {
       _logger.LogFailedCreatingJob(e, cadJob.Id);
       return Errors.CreateJobInDatabase;
   }

   var activityResult = await CreateActivityAndUpdateJobAsync(cadJob, configuration.GetWaremaCadIdentifier(), cancellationToken);
   if (!activityResult.Succeeded)
   {
       return activityResult.Errors;
   }

   var commission = cadRequest.Commission.Length > 20 ? cadRequest.Commission[..20] : cadRequest.Commission;
   var fileName = $"{commission.Trim().Replace(' ', '-')}_{cadRequest.ReadableIdentifier.Replace(' ', '_').Replace('.', '-')}_{configuration.ItemNo}";

   var workItemResult = await _forgeApiService.StartWorkItemAsync(cadJob, configuration, fileName, availableVisualisationModes, cancellationToken);
   if (!workItemResult.Succeeded)
   {
       cadJob.Results.ToList().ForEach(cadResult => cadResult.State = ProcessingState.Failed);
   }

   await _cadJobRepository.UpdateAsync(cadJob, cancellationToken);

   if (!workItemResult.Succeeded)
   {
       return workItemResult.Errors;
   }

   _backgroundJobClient.Enqueue<ICadJobService>(x => x.DeleteActivityAndThrowOnErrorAsync(cadJob.Id, cancellationToken));

   return ResultBase<CadJob>.Success(cadJob);
}

