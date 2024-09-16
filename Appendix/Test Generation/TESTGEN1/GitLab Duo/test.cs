[Theory]
[InlineData("ABC123", "Test Product", "123456", "ABC123_Test_Product_123456")]
[InlineData("ThisIsAVeryLongCommissionNumber", "Another Product", "789012", "ThisIsAVeryLongCommi_Another_Product_789012")]
[InlineData("Commission With Spaces", "Spaced Product", "345678", "Commission-With-Space_Spaced_Product_345678")]
[InlineData("XYZ789", "Special.Product Version 1.0", "901234", "XYZ789_Special-Product_Version_1-0_901234")]
[InlineData("", "Empty Commission", "000000", "_Empty_Commission_000000")]
[InlineData("Short", "", "111111", "Short__111111")]
[InlineData("Exactly20Characters", "Exact", "222222", "Exactly20Characters_Exact_222222")]
[InlineData("    Trim    Me    ", "Trim Test", "333333", "Trim----Me_Trim_Test_333333")]
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