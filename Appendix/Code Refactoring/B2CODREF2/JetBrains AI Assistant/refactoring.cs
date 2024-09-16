public async Task CancelObsoleteRunningJobAsync(CadJob cadJob, CancellationToken cancellationToken)
{
   if (!IsValidConfiguration(cadJob))
   {
       return;
   }

   var runningJobs = await _cadJobRepository.GetRunningCadJobsByConfigurationIdAndBlindIdExceptAsync(cadJob.ConfigurationId!.Value, cadJob.BlindIds!, cadJob.Id, cancellationToken);
   foreach (var job in runningJobs)
   {
       await ProcessJobAsync(job);
   }

   await _cadJobRepository.SaveAsync(cancellationToken);
}

private static bool IsValidConfiguration(CadJob cadJob)
{
   return !(cadJob.ConfigurationId is null || cadJob.BlindIds is null || cadJob.BlindIds.Count == 0);
}

private async Task ProcessJobAsync(CadJob job)
{
   if (job.ForgeWorkItemId is null)
   {
       return;
   }

   var cancelResult = await _forgeApiService.CancelWorkItemAsync(job.ForgeWorkItemId);
   if (cancelResult.Succeeded)
   {
       UpdateJobResultsState(job);
   }
}

private static void UpdateJobResultsState(CadJob job)
{
   foreach (var cadResult in job.Results)
   {
       cadResult.State = ProcessingState.Canceled;
   }
}