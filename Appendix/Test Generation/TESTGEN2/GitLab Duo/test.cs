[Theory]
[InlineData(CadFileFormat.Pdf, VisualisationModes.Configuration, "blind1,blind2", true, CadRequestTypes.Viewer)]
[InlineData(CadFileFormat.Iges, VisualisationModes.Group, "blind3,blind4", false, CadRequestTypes.Email, CadRequestTypes.Download)]
[InlineData(CadFileFormat.Dwg, VisualisationModes.Blind, "blind5", true, CadRequestTypes.Download)]
[InlineData(CadFileFormat.Pdf, VisualisationModes.Configuration, "blind6,blind7", true, CadRequestTypes.Viewer, CadRequestTypes.Email)]
[InlineData(CadFileFormat.Iges, VisualisationModes.Group, "blind8", false, CadRequestTypes.Email)]
public void FindExistingResultTest(CadFileFormat testFileFormat, VisualisationModes testVisualisationMode, string concatenatedBlindIds, bool expectedResult, params CadRequestTypes[] testRequestTypes)
{
   // Arrange
   var testResult = new CadJobResultDto
   {
       FileFormat = testFileFormat,
       VisualisationMode = testVisualisationMode,
       ConcatenatedBlindIds = concatenatedBlindIds,
       RequestTypes = testRequestTypes.ToList(),
   };

   var existingResults = new List<CadJobResultDto>
   {
       new CadJobResultDto
       {
           FileFormat = CadFileFormat.Pdf,
           VisualisationMode = VisualisationModes.Configuration,
           ConcatenatedBlindIds = "blind1,blind2",
           RequestTypes = new List<CadRequestTypes> { CadRequestTypes.Viewer },
       },
       new CadJobResultDto
       {
           FileFormat = CadFileFormat.Dwg,
           VisualisationMode = VisualisationModes.Blind,
           ConcatenatedBlindIds = "blind5",
           RequestTypes = new List<CadRequestTypes> { CadRequestTypes.Download },
       },
       new CadJobResultDto
       {
           FileFormat = CadFileFormat.Pdf,
           VisualisationMode = VisualisationModes.Configuration,
           ConcatenatedBlindIds = "blind6,blind7",
           RequestTypes = new List<CadRequestTypes> { CadRequestTypes.Viewer, CadRequestTypes.Email },
       },
   };

   // Act
   bool result = CadJobService.FindExistingResult(existingResults, testResult);

   // Assert
   Assert.Equal(expectedResult, result);
}

