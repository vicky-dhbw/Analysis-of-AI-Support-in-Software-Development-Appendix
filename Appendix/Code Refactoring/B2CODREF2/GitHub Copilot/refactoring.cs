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

       job.Results.ToList().ForEach(result => result.State = ProcessingState.Canceled);
   }

   await _cadJobRepository.SaveAsync(cancellationToken);
}