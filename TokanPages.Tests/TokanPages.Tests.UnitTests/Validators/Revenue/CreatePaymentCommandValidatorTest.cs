using FluentAssertions;
using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Backend.Application.Revenue.Models;
using TokanPages.Backend.Application.Revenue.Models.Sections;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Revenue;

public class CreatePaymentCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenCreatePayment_ShouldSucceed()
    {
        // Arrange
        var command = new CreatePaymentCommand
        {
            UserId = Guid.NewGuid(),
            Request = new PaymentRequest
            {
                NotifyUrl = DataUtilityService.GetRandomString(),
                ContinueUrl = DataUtilityService.GetRandomString(),
                MerchantPosId = DataUtilityService.GetRandomString(),
                CardOnFile = DataUtilityService.GetRandomString(),
                Recurring = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TotalAmount = "1000.50",
                CurrencyCode = DataUtilityService.GetRandomString(3),
                ExtOrderId = DataUtilityService.GetRandomString(),
                Products = new List<Product>
                {
                    new()
                    {
                        Name = DataUtilityService.GetRandomString(),
                        UnitPrice = DataUtilityService.GetRandomString(),
                        Quantity = DataUtilityService.GetRandomString()
                    }
                },
                Buyer = new Buyer
                {
                    Email = DataUtilityService.GetRandomEmail(),
                    Language = DataUtilityService.GetRandomString(),
                    FirstName = DataUtilityService.GetRandomString(),
                    LastName = DataUtilityService.GetRandomString()
                }
            }
        };

        // Act
        var validator = new CreatePaymentCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GivenEmptyInput_WhenCreatePayment_ShouldFail()
    {
        // Arrange
        var command = new CreatePaymentCommand
        {
            UserId = Guid.Empty,
            Request = new PaymentRequest
            {
                NotifyUrl = string.Empty,
                ContinueUrl = string.Empty,
                MerchantPosId = string.Empty,
                CardOnFile = string.Empty,
                Recurring = string.Empty,
                Description = string.Empty,
                TotalAmount = string.Empty,
                CurrencyCode = string.Empty,
                ExtOrderId = string.Empty,
                Products = new List<Product>
                {
                    new()
                    {
                        Name = string.Empty,
                        UnitPrice = string.Empty,
                        Quantity = string.Empty,
                    }
                },
                Buyer = new Buyer
                {
                    Email = string.Empty,
                    Language = string.Empty,
                    FirstName = string.Empty,
                    LastName = string.Empty
                }
            }
        };

        // Act
        var validator = new CreatePaymentCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(17);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[5].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[6].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[7].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[8].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[9].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[10].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[11].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[12].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[13].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[14].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[15].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[16].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}