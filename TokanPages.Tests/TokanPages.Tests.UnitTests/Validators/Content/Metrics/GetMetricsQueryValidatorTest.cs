using FluentAssertions;
using TokanPages.Backend.Application.Content.Metrics.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Metrics;

public class GetMetricsQueryValidatorTest : TestBase
{
    [Fact]
    public async Task GivenValidInputs_WhenGetMetricsQuery_ShouldSucceed()
    {
        // Arrange
        var query = new GetMetricsQuery
        {
            Project = DataUtilityService.GetRandomString(),
            Metric = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetMetricsQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task GivenEmptyInputs_WhenGetMetricsQuery_ShouldFail()
    {
        // Arrange
        var query = new GetMetricsQuery
        {
            Project = string.Empty,
            Metric = string.Empty
        };

        // Act
        var validator = new GetMetricsQueryValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}