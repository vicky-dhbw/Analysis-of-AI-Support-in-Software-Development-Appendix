public class FindExistingResultTests
{
   [Theory]
   [InlineData(CadFileFormat.Pdf, VisualisationModes.Configuration, "BlindIds1", true, new[]
   {
       CadRequestTypes.Viewer,
   })]
   [InlineData(CadFileFormat.Iges, VisualisationModes.Group, "BlindIds2", true, new[]
   {
       CadRequestTypes.Email,
       CadRequestTypes.Download,
   })]
   // add more InlineData attribute here to cover various test cases
   public void FindExistingResultTest(CadFileFormat testFileFormat, VisualisationModes testVisualisationMode, string concatenatedBlindIds, bool expectedResult, CadRequestTypes[] testRequestTypes)
   {
       List<CadJobResultDto> results = new List<CadJobResultDto>()
       {
           new CadJobResultDto()
           {
               FileFormat = testFileFormat,
               VisualisationMode = testVisualisationMode,
               ConcatenatedBlindIds = concatenatedBlindIds,
               RequestTypes = testRequestTypes.ToList(),
           },
       };

       CadJobResultDto result = new CadJobResultDto()
       {
           FileFormat = testFileFormat,
           VisualisationMode = testVisualisationMode,
           ConcatenatedBlindIds = concatenatedBlindIds,
           RequestTypes = testRequestTypes.ToList(),
       };

       bool actualResult = CadJobService.FindExistingResult(results, result);

       Assert.Equal(expectedResult, actualResult);
   }
}
