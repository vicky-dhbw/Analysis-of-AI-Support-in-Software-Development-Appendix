[Theory]
[InlineData("Commission12345678901234567890", "Readable.Identifier", "Item123", "Commission1234567890_Readable-Identifier_Item123")]
[InlineData("ShortCommission", "Readable Identifier", "Item456", "ShortCommission_Readable_Identifier_Item456")]
[InlineData("Commission With Spaces", "Readable.Identifier", "Item789", "Commission-With-Spac_Readable-Identifier_Item789")]
[InlineData("CommissionWithSpecialChars!@#", "Readable.Identifier", "Item000", "CommissionWithSpecia_Readable-Identifier_Item000")]
[InlineData("CommissionWithVeryLongNameThatExceedsTwentyCharacters", "Readable.Identifier", "Item999", "CommissionWithVeryLo_Readable-Identifier_Item999")]
public void GetFileDownloadNameTest(string commission, string readableIdentifier, string itemNo, string expectedFileName)
{
   // Arrange
   var cadRequest = new CadRequest
   {
       Commission = commission,
       ReadableIdentifier = readableIdentifier,
   };

   // Act
   var result = CadJobService.GetFileDownloadName(itemNo, cadRequest);

   // Assert
   Assert.Equal(expectedFileName, result);
}