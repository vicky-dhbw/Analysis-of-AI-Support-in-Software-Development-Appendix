public Task<ResultBase<string>> Handle(GetCatalogUrlCommand request, CancellationToken cancellationToken)
{
   // Retrieve base URL from settings
   var baseUrl = _sysIntApiSettings.SysIntFrontendBaseUrl;

   // Construct the base catalog URL
   var catalogUrl = $"{baseUrl}catalog";

   // Initialize query parameters dictionary
   var queryParams = new Dictionary<string, string>
   {
       { "language", request.Language },
       { "showPrices", request.ShowPrices.ToString() },
   };

   // Conditionally set the catalogType query parameter
   if (request.CatalogType == CatalogType.WindowRelated)
   {
       queryParams.Add("catalogType", "window_related");
   }

   // Append query parameters to the URL
   var finalUrl = QueryHelpers.AddQueryString(catalogUrl, queryParams!);

   // Return the fully constructed URL encapsulated in the ResultBase wrapper
   return Task.FromResult(ResultBase<string>.Success(finalUrl));
}