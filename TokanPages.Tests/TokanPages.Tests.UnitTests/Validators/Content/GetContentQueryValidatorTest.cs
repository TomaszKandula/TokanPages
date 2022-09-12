namespace TokanPages.Tests.UnitTests.Validators.Content;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Queries.Content;

public class GetContentQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenGetContent_ShouldSucceed()
    {
        // Arrange
        var query = new GetContentQuery
        {
            Type = DataUtilityService.GetRandomString(),
            Name = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new GetContentQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenGetContent_ShouldThrowError()
    {
        // Arrange
        var query = new GetContentQuery
        {
            Type = string.Empty,
            Name = string.Empty
        };

        // Act
        var validator = new GetContentQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}