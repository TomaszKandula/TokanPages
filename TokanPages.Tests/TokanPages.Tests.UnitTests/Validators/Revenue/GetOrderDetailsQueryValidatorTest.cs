using FluentAssertions;
using TokanPages.Backend.Application.Revenue.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Revenue;

public class GetOrderDetailsQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenOrderId_WhenGetOrderDetails_ShouldSucceed()
    {
        // Arrange
        var query = new GetOrderDetailsQuery
        {
            OrderId = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetOrderDetailsQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenEmptyOrderId_WhenGetOrderDetails_ShouldFail()
    {
        // Arrange
        var query = new GetOrderDetailsQuery
        {
            OrderId = string.Empty
        };

        // Act
        var validator = new GetOrderDetailsQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}