public async Task<ResultBase<CadJob>> StartJobAsync(
   CadRequest cadRequest,
   ConfigurationDbStructure configuration,
   VisualisationModes availableVisualisationModes,
   CancellationToken cancellationToken
)
{
   var cadJob = await CreateAndSaveCadJobAsync(cadRequest, cancellationToken);
   if (cadJob == null)
   {
       return Errors.CreateJobInDatabase;
   }

   var activityResult = await CreateActivityAndUpdateJobAsync(cadJob, configuration.GetWaremaCadIdentifier(), cancellationToken);
   if (!activityResult.Succeeded)
   {
       return activityResult.Errors;
   }

   var fileName = GetFileDownloadName(configuration.GetWaremaCadIdentifier(), cadRequest);

   var workItemResult = await StartWorkItemAndUpdateJobAsync(cadJob, configuration, fileName, availableVisualisationModes, cancellationToken);
   if (!workItemResult.Succeeded)
   {
       return workItemResult.Errors;
   }

   _backgroundJobClient.Enqueue<ICadJobService>(x => x.DeleteActivityAndThrowOnErrorAsync(cadJob.Id, cancellationToken));

   return ResultBase<CadJob>.Success(cadJob);
}

private async Task<CadJob?> CreateAndSaveCadJobAsync(CadRequest cadRequest, CancellationToken cancellationToken)
{
   var cadJob = _mapper.Map<CadJob>(cadRequest.Job);
   cadJob.ValidUntil = DateTime.Now.AddDays(1);

   try
   {
       foreach (var format in cadRequest.RequestedFormats)
       {
           cadJob.Results.Add(new CadJobResult
           {
               FileFormat = format,
               State = ProcessingState.Processing,
           });
       }

       return await _cadJobRepository.CreateAsync(cadJob, cancellationToken);
   }
   catch (Exception e)
   {
       _logger.LogFailedCreatingJob(e, cadJob.Id);
       return null;
   }
}