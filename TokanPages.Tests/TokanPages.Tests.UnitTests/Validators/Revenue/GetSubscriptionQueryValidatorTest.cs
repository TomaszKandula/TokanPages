using FluentAssertions;
using TokanPages.Backend.Application.Revenue.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Revenue;

public class GetSubscriptionQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenUserId_WhenGetSubscription_ShouldSucceed()
    {
        // Arrange
        var query = new GetSubscriptionQuery
        {
            UserId = Guid.NewGuid()
        };

        // Act
        var validator = new GetSubscriptionQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenNoUserId_WhenGetSubscription_ShouldSucceed()
    {
        // Arrange
        var query = new GetSubscriptionQuery();

        // Act
        var validator = new GetSubscriptionQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyUserId_WhenGetSubscription_ShouldFail()
    {
        // Arrange
        var query = new GetSubscriptionQuery
        {
            UserId = Guid.Empty
        };

        // Act
        var validator = new GetSubscriptionQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
}