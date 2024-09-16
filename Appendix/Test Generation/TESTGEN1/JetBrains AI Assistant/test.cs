public class GetFileDownloadNameTest
{
   [Theory]
   [InlineData("COMM1111", "ReadableIdentifier1", "Item1", "COMM1111_ReadableIdentifier1_Item1")]
   [InlineData("COMMISSION WITH LONG NAME TRIMMED", "ReadableIdentifier2", "Item2", "COMMISSION-WITH-LONG_ReadableIdentifier2_Item2")]
   [InlineData("COMMISSION WITH SPACES", "Readable Identifier", "Item 3", "COMMISSION-WITH-SPAC_Readable_Identifier_Item 3")]
   // Add more InlineData to test specific use cases and edge cases
   public void TestGetFileDownloadName(string commission, string readableIdentifier, string itemNo, string expectedFileName)
   {
       // Arrange
       var cadRequest = new CadRequest
       {
           Commission = commission,
           ReadableIdentifier = readableIdentifier,
       };

       // Act
       var actualFileName = CadJobService.GetFileDownloadName(itemNo, cadRequest);

       // Assert
       Assert.Equal(expectedFileName, actualFileName);
   }
}