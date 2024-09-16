[Theory]
[InlineData(CadFileFormat.Pdf, VisualisationModes.Configuration, "blindId1", true, CadRequestTypes.Viewer, CadRequestTypes.Download)]
[InlineData(CadFileFormat.Iges, VisualisationModes.Group, "blindId2", true, CadRequestTypes.Email)]
[InlineData(CadFileFormat.Dwg, VisualisationModes.Blind, "blindId3", true, CadRequestTypes.Viewer)]
[InlineData(CadFileFormat.Pdf, VisualisationModes.Configuration, "blindId4", false, CadRequestTypes.Download)]
public void FindExistingResultTest(CadFileFormat testFileFormat, VisualisationModes testVisualisationMode, string concatenatedBlindIds, bool expectedResult, params CadRequestTypes[] testRequestTypes)
{
   // Arrange
   var testResult = new CadJobResultDto
   {
       FileFormat = testFileFormat,
       VisualisationMode = testVisualisationMode,
       ConcatenatedBlindIds = concatenatedBlindIds,
       RequestTypes = new List<CadRequestTypes>(testRequestTypes),
   };

   var existingResults = new List<CadJobResultDto>
   {
       new CadJobResultDto
       {
           FileFormat = CadFileFormat.Pdf,
           VisualisationMode = VisualisationModes.Configuration,
           ConcatenatedBlindIds = "blindId1",
           RequestTypes = new List<CadRequestTypes> { CadRequestTypes.Viewer, CadRequestTypes.Download },
       },
       new CadJobResultDto
       {
           FileFormat = CadFileFormat.Iges,
           VisualisationMode = VisualisationModes.Group,
           ConcatenatedBlindIds = "blindId2",
           RequestTypes = new List<CadRequestTypes> { CadRequestTypes.Email },
       },
       new CadJobResultDto
       {
           FileFormat = CadFileFormat.Dwg,
           VisualisationMode = VisualisationModes.Blind,
           ConcatenatedBlindIds = "blindId3",
           RequestTypes = new List<CadRequestTypes> { CadRequestTypes.Viewer },
       },
   };

   // Act
   var result = CadJobService.FindExistingResult(existingResults, testResult);

   // Assert
   Assert.Equal(expectedResult, result);
}