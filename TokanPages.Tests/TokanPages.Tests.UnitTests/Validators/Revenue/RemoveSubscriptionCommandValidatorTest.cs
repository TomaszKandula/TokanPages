using FluentAssertions;
using TokanPages.Backend.Application.Revenue.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Revenue;

public class RemoveSubscriptionCommandValidatorTest : TestBase
{
    [Fact]
    public async Task GivenUserId_WhenRemoveSubscription_ShouldSucceed()
    {
        // Arrange
        var command = new RemoveSubscriptionCommand
        {
            UserId = Guid.NewGuid()
        };

        // Act
        var validator = new RemoveSubscriptionCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyUserId_WhenRemoveSubscription_ShouldFail()
    {
        // Arrange
        var command = new RemoveSubscriptionCommand
        {
            UserId = Guid.Empty
        };

        // Act
        var validator = new RemoveSubscriptionCommandValidator();
        var result = await validator.ValidateAsync(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}