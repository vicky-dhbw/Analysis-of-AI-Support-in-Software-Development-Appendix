public Task<ResultBase<string>> Handle(GetCatalogUrlCommand command, CancellationToken cancellationToken)
{
   // Get the base URL from settings and append "catalog"
   var baseUrl = $"{_sysIntApiSettings.SysIntFrontendBaseUrl}catalog";

   // Create a dictionary to hold query parameters
   var queryParams = new Dictionary<string, string>
   {
       { "language", command.Language },
       { "showPrices", command.ShowPrices.ToString()},
   };

   // Add catalogType parameter only if it's WindowRelated
   if (command.CatalogType == CatalogType.WindowRelated)
   {
       queryParams.Add("catalogType", "window_related");
   }

   // Build the final URL with query parameters
   var finalUrl = QueryHelpers.AddQueryString(baseUrl, queryParams!);

   // Return the result wrapped in a ResultBase
   return Task.FromResult(ResultBase<string>.Success(finalUrl));
}