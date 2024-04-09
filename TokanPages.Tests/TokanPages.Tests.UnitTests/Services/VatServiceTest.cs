using FluentAssertions;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.VatService;
using TokanPages.Services.VatService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class VatServiceTest : TestBase
{
	[Theory]
	[InlineData("PL759-313-71-92")]
	[InlineData("PL759-31-37-192")]
	[InlineData("PL7593137192")]
	[InlineData("PL3396107049")]
	[InlineData("759-313-71-92")]
	[InlineData("759-31-37-192")]
	[InlineData("5268630415")]
	[InlineData("3928035475")]
	public void GivenCorrectPolishVatNumber_WhenValidateVatNumber_ShouldSucceed(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().BeEmpty();
	}

	[Theory]
	[InlineData("PL3333333333")]
	[InlineData("PL7777777777")]
	[InlineData("1111111111")]
	[InlineData("5555555555")]
	public void GivenPolishVatNumberHaveAllDigitsSame_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_ALL_DIGITS_ARE_SAME));
	}

	[Theory]
	[InlineData("PL93137192")]
	[InlineData("PL107049")]
	[InlineData("520415")]
	[InlineData("3975")]
	public void GivenPolishVatNumberHaveIncorrectLength_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_INCORRECT_LENGTH));
	}

	[Theory]
	[InlineData("PL759A1V7192")]
	[InlineData("PL3396107ABB")]
	[InlineData("52686AAAEE")]
	[InlineData("PLAAABBBCCCC")]
	public void GivenPolishVatNumberContainsLetters_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_INCORRECT_SIGN));
	}

	[Theory]
	[InlineData("PL75-931-371-92")]
	[InlineData("PL75-93-137-192")]
	public void GivenPolishVatNumberHaveIncorrectFormat_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_INCORRECT_FORMAT));
	}

	[Theory]
	[InlineData("PL0593137192")]
	[InlineData("PL3306107049")]
	[InlineData("0268630415")]
	[InlineData("3908035475")]
	public void GivenPolishVatNumberHaveZeroAtFirstOrThirdPosition_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_ZERO_AT_FIRST_OR_THIRD_POSSITION));
	}

	[Theory]
	[InlineData("PL059-313-71-92")]
	[InlineData("PL059-31-37-192")]
	[InlineData("PL7503137192")]
	[InlineData("PL3306107049")]
	[InlineData("059-313-71-92")]
	[InlineData("750-31-37-192")]
	[InlineData("0268630415")]
	[InlineData("3908035475")]
	public void GivenPolishVatNumberHaveZeroAtFirstOrThirdPositionAndAllowZerosAndNoCheckSum_WhenValidateVatNumber_ShouldSucceed(string vatNumber)
	{
		// Arrange
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(false, false);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().BeEmpty();
	}

	[Theory]
	[InlineData("PL1231434455")]
	[InlineData("1231434455")] 
	public void GivenPolishVatNumberHaveIncorrectCheckSum_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_INCORRECT_CHECK_SUM));
	}

	[Theory]
	[InlineData("ATU99999999")]
	[InlineData("BE0999999911")]
	[InlineData("BG999999999")]
	[InlineData("CY99999999L")]
	[InlineData("CZ12345618")]
	[InlineData("DK99999999")]
	[InlineData("EE123456789")]
	[InlineData("FI99999999")]
	[InlineData("FR33999999999")]
	[InlineData("EL123456789")]
	[InlineData("ESX12345678")]
	[InlineData("NL999999999B01")]
	[InlineData("IE1234567T")]
	[InlineData("LT123456789123")]
	[InlineData("LU12345678")]
	[InlineData("LV12345678914")]
	[InlineData("MT98745612")]
	[InlineData("DE999999999")]
	[InlineData("PT123456789")]
	[InlineData("RO12345678")]
	[InlineData("SK1234567891")]
	[InlineData("SI99999999")]
	[InlineData("SE999999999901")]
	[InlineData("HU12345678")]
	[InlineData("GB999999973")]
	[InlineData("IT12345678914")]
	[InlineData("CH123456")]
	[InlineData("HR12345678901")]
	public void GivenVatNumberFormatCorrect_WhenValidateVatNumber_ShouldSucceed(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().BeEmpty();
	}

	[Theory]
	[InlineData("ATU99999999A")]
	[InlineData("BE09999999110")]
	[InlineData("BG999999999PL")]
	[InlineData("CY9999999L")]
	[InlineData("CZ1234618")]
	[InlineData("DK9999999")]
	[InlineData("EE1234567890")]
	[InlineData("FI9999999")]
	[InlineData("FR339999999")]
	[InlineData("EL12345678900")]
	[InlineData("ESX12347811000")]
	[InlineData("NL9999999B01")]
	[InlineData("IE123450067T")]
	[InlineData("LT1456789123")]
	[InlineData("LU1002345678")]
	[InlineData("LV12311145678914")]
	[InlineData("MT98712")]
	[InlineData("DE999999")]
	[InlineData("PT12113456789")]
	[InlineData("RO1234AA5678")]
	[InlineData("SK127891")]
	[InlineData("SI999999")]
	[InlineData("SE9999889999999901")]
	[InlineData("HU1278")]
	[InlineData("GB999973AA465")]
	[InlineData("IT12678914")]
	[InlineData("CH12000003456")]
	[InlineData("HR12345678901TA")]
	public void GivenVatNumberFormatIncorrect_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_INCORRECT_FORMAT));
	}

	[Theory]
	[InlineData("120000034")]
	[InlineData("PL120000034")]
	public void GivenVatNumberBeingNineDigits_WhenValidateVatNumber_ShouldThrowError(string vatNumber)
	{
		// Arrange
		var service = new VatService();
		var options = new PolishVatNumberOptions(true, true);
		var request = new VatValidationRequest(vatNumber, GetPatternList(), options);

		// Act
		var result = service.ValidateVatNumber(request);

		// Assert
		result.Errors.Should().NotBeNullOrEmpty();
		result.Errors[0].ErrorCode.Should().Be(nameof(ErrorCodes.VAT_NUM_LENGTH_NINE));
	}
	    
	private static IEnumerable<VatNumberPatterns> GetPatternList()
	{
		return new List<VatNumberPatterns>
		{
			new() { CountryCode = "PL", Pattern = @"^PL[ ]?\d{10}$|^\d{3}-\d{3}-\d{2}-\d{2}$|^\d{3}-\d{2}-\d{2}-\d{3}$" },
			new() { CountryCode = "AT", Pattern = @"^AT[ ]?U\d{8}$" },
			new() { CountryCode = "BE", Pattern = @"^BE[ ]?(0?\d{9})$" },
			new() { CountryCode = "BG", Pattern = @"^BG[ ]?\d{9,10}$" },
			new() { CountryCode = "CY", Pattern = @"^CY[ ]?\d{8}[a-zA-z]{1}$" },
			new() { CountryCode = "CZ", Pattern = @"^CZ[ ]?\d{8,10}$" },
			new() { CountryCode = "DK", Pattern = @"^DK[ ]?\d{8}$" },
			new() { CountryCode = "EE", Pattern = @"^EE[ ]?\d{9}$" },
			new() { CountryCode = "FI", Pattern = @"^FI[ ]?\d{8}$" },
			new() { CountryCode = "FR", Pattern = @"^FR[ ]?(([a-zA-Z]{1})|(\d{1}))(([a-zA-Z]{1})|(\d{1}))(\d{9})$" },
			new() { CountryCode = "EL", Pattern = @"^EL[ ]?\d{9}$" },
			new() { CountryCode = "ES", Pattern = @"^ES[ ]?((([a-zA-Z]{1})|(\d{1}))(\d{7})(([a-zA-Z]{1})|(\d{1})))$" },
			new() { CountryCode = "NL", Pattern = @"^NL[ ]?\d{9}B{1}\d{2}$" },
			new() { CountryCode = "IE", Pattern = @"^IE[ ]?\d{1}(([a-zA-Z]{1})|(\d{1})|(\+{1})|(\*{1}))\d{5}[a-zA-Z]{1,2}$" },
			new() { CountryCode = "LT", Pattern = @"^LT[ ]?((\d{9})|(\d{12}))$" },
			new() { CountryCode = "LU", Pattern = @"^LU[ ]?\d{8}$" },
			new() { CountryCode = "LV", Pattern = @"^LV[ ]?\d{11}$" },
			new() { CountryCode = "MT", Pattern = @"^MT[ ]?\d{8}$" },
			new() { CountryCode = "DE", Pattern = @"^DE[ ]?\d{9}$" },
			new() { CountryCode = "PT", Pattern = @"^PT[ ]?\d{9}$" },
			new() { CountryCode = "RO", Pattern = @"^RO[ ]?\d{2,10}$" },
			new() { CountryCode = "SK", Pattern = @"^SK[ ]?\d{9,10}$" },
			new() { CountryCode = "SI", Pattern = @"^SI[ ]?\d{8}$" },
			new() { CountryCode = "SE", Pattern = @"^SE[ ]?\d{12}$" },
			new() { CountryCode = "HU", Pattern = @"^HU[ ]?\d{8}$" },
			new() { CountryCode = "GB", Pattern = @"^GB[ ]?(((\d{2})|(G{1}D{1})|(H{1}A{1}))((\d{3})|(\d{7})|(\d{10})))$" },
			new() { CountryCode = "IT", Pattern = @"^IT[ ]?\d{11}$" },
			new() { CountryCode = "CH", Pattern = @"^CH[ ]?\d{6}$" },
			new() { CountryCode = "HR", Pattern = @"^HR[ ]?\d{11}$" }
		};
	}
}