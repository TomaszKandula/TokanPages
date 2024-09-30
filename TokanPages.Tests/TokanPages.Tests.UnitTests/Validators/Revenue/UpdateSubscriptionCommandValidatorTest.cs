using FluentAssertions;
using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Revenue;

public class UpdateSubscriptionCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenUpdateSubscription_ShouldSucceed()
    {
        // Arrange
        var command = new UpdateSubscriptionCommand
        {
            UserId = Guid.NewGuid(),
            AutoRenewal = true,
            Term = TermType.OneMonth,
            TotalAmount = 1000,
            CurrencyIso = "PLN"
        };

        // Act
        var validator = new UpdateSubscriptionCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyInput_WhenUpdateSubscription_ShouldFail()
    {
        // Arrange
        var command = new UpdateSubscriptionCommand
        {
            UserId = Guid.Empty,
            TotalAmount = 0,
            CurrencyIso = string.Empty
        };

        // Act
        var validator = new UpdateSubscriptionCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public async Task GivenInvalidCurrencyIso_WhenUpdateSubscription_ShouldFail()
    {
        // Arrange
        var command = new UpdateSubscriptionCommand
        {
            UserId = Guid.NewGuid(),
            TotalAmount = 1000,
            CurrencyIso = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new UpdateSubscriptionCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.ISO_VALUE_TOO_LONG));
    }
}