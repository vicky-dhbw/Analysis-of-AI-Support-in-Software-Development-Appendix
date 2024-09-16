public async Task CancelObsoleteRunningJobAsync(CadJob cadJob, CancellationToken cancellationToken)
{
   if (cadJob.ConfigurationId is null || cadJob.BlindIds is null || cadJob.BlindIds.Count == 0)
   {
       return;
   }

   var runningJobs = await _cadJobRepository.GetRunningCadJobsByConfigurationIdAndBlindIdExceptAsync(
       cadJob.ConfigurationId.Value, cadJob.BlindIds, cadJob.Id, cancellationToken);

   foreach (var job in runningJobs)
   {
       if (job.ForgeWorkItemId is null)
       {
           continue;
       }

       var cancelResult = await _forgeApiService.CancelWorkItemAsync(job.ForgeWorkItemId);
       if (!cancelResult.Succeeded)
       {
           continue;
       }

       UpdateJobResultsState(job, ProcessingState.Canceled);
   }

   await _cadJobRepository.SaveAsync(cancellationToken);
}

private static void UpdateJobResultsState(CadJob job, ProcessingState state)
{
   foreach (var cadResult in job.Results)
   {
       cadResult.State = state;
   }
}