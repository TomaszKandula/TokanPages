using FluentAssertions;
using TokanPages.Backend.Application.Revenue.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Revenue;

public class GetOrderTransactionsQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenOrderId_WhenGetOrderTransactions_ShouldSucceed()
    {
        // Arrange
        var query = new GetOrderTransactionsQuery
        {
            OrderId = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetOrderTransactionsQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GivenEmptyOrderId_WhenGetOrderTransactions_ShouldFail()
    {
        // Arrange
        var query = new GetOrderTransactionsQuery
        {
            OrderId = string.Empty
        };

        // Act
        var validator = new GetOrderTransactionsQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}