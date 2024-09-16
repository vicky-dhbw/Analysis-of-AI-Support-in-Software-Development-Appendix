public Task<ResultBase<string>> Handle(GetCatalogUrlCommand request, CancellationToken cancellationToken)
{
   // Extract the base URL and concatenate "catalog"
   var baseUrl = _sysIntApiSettings.SysIntFrontendBaseUrl;
   var catalogUrl = $"{baseUrl}catalog";

   // Create query parameters
   var queryParams = new Dictionary<string, string>
   {
       { "language", request.Language },
       { "showPrices", request.ShowPrices.ToString() },
   };

   // Conditionally add catalogType to query parameters
   if (request.CatalogType == CatalogType.WindowRelated)
   {
       queryParams.Add("catalogType", "window_related");
   }

   // Append query parameters to the URL
   var fullUrl = QueryHelpers.AddQueryString(catalogUrl, queryParams!);

   // Return the full URL wrapped in ResultBase<string>
   return Task.FromResult(ResultBase<string>.Success(fullUrl));
}