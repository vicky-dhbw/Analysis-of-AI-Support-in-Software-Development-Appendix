public async Task<ResultBase<CadJob>> StartJobAsync(
   CadRequest request,
   ConfigurationDbStructure config,
   VisualisationModes availableModes,
   CancellationToken cancellationToken
)
{
   var job = PrepareJob(request);
   try
   {
       job = await _cadJobRepository.CreateAsync(job, cancellationToken);

       var activityResult = await CreateActivityAndUpdateJobAsync(job, config.GetWaremaCadIdentifier(), cancellationToken);
       if (!activityResult.Succeeded)
       {
           return activityResult.Errors;
       }

       var workItemResult = await RunActivityAndCreateWorkItemAsync(job, request, config, availableModes, cancellationToken);
       if (!workItemResult.Succeeded)
       {
           return workItemResult.Errors;
       }

       _backgroundJobClient.Enqueue<ICadJobService>(x => x.DeleteActivityAndThrowOnErrorAsync(job.Id, cancellationToken));
       return ResultBase<CadJob>.Success(job);
   }
   catch (Exception ex)
   {
       _logger.LogFailedCreatingJob(ex, job.Id);
       return Errors.CreateJobInDatabase;
   }
}

private CadJob PrepareJob(CadRequest request)
{
   var job = _mapper.Map<CadJob>(request.Job);
   job.ValidUntil = DateTime.Now.AddDays(1);
   foreach (var format in request.RequestedFormats)
   {
       var result = new CadJobResult
       {
           FileFormat = format,
           State = ProcessingState.Processing,
       };
       job.Results.Add(result);
   }

   return job;
}

private async Task<ResultBase<CadJob>> RunActivityAndCreateWorkItemAsync(CadJob job, CadRequest request, ConfigurationDbStructure config, VisualisationModes availableModes,
   CancellationToken cancellationToken)
{
   var cleanCommission = (request.Commission.Length > 20 ? request.Commission[..20] : request.Commission).Trim().Replace(' ', '-');
   var readableId = request.ReadableIdentifier.Replace(' ', '_').Replace('.', '-');
   var fileName = $"{cleanCommission}_{readableId}_{config.GetWaremaCadIdentifier()}";

   var workItemResult = await _forgeApiService.StartWorkItemAsync(job, config, fileName, availableModes, cancellationToken);
  
   if (!workItemResult.Succeeded)
   {
       foreach (var cadResult in job.Results)
       {
           cadResult.State = ProcessingState.Failed;
       }
   }

   await _cadJobRepository.UpdateAsync(job, cancellationToken);
   return workItemResult.Succeeded ? ResultBase<CadJob>.Success(job) : workItemResult.Errors;
}

