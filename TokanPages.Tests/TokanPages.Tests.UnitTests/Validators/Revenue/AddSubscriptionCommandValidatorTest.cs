using FluentAssertions;
using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Revenue;

public class AddSubscriptionCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInput_WhenAddSubscription_ShouldSucceed()
    {
        // Arrange
        var command = new AddSubscriptionCommand
        {
            UserId = Guid.NewGuid(),
            SelectedTerm = TermType.OneMonth,
            UserCurrency = "PLN",
            UserLanguage = "POL"
        };

        // Act
        var validator = new AddSubscriptionCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyInput_WhenAddSubscription_ShouldFail()
    {
        // Arrange
        var command = new AddSubscriptionCommand
        {
            UserId = Guid.Empty,
            UserCurrency = string.Empty,
            UserLanguage = string.Empty
        };

        // Act
        var validator = new AddSubscriptionCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(5);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[4].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}