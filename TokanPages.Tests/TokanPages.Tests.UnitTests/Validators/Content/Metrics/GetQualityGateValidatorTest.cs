using FluentAssertions;
using TokanPages.Backend.Application.Content.Metrics.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Metrics;

public class GetQualityGateValidatorTest : TestBase
{
    [Fact]
    public async Task GivenProjectName_WhenGetQualityGateQuery_ShouldSucceed()
    {
        // Arrange
        var query = new GetQualityGateQuery
        {
            Project = DataUtilityService.GetRandomString()
        };
        
        // Act
        var validator = new GetQualityGateValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenNoProjectName_WhenGetQualityGateQuery_ShouldFail()
    {
        // Arrange
        var query = new GetQualityGateQuery
        {
            Project = string.Empty
        };
        
        // Act
        var validator = new GetQualityGateValidator();
        var result = await validator.ValidateAsync(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}