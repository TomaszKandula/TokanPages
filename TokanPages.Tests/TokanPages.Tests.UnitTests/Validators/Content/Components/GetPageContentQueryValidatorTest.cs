using FluentAssertions;
using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Application.Content.Components.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Components;

public class GetPageContentQueryValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenGetPageContent_ShouldSucceed()
    {
        // Arrange
        var query = new GetPageContentQuery
        {
            Language = "eng",
            Components = new List<ContentModel>
            {
                new()
                {
                    Name = DataUtilityService.GetRandomString(),
                    Type = DataUtilityService.GetRandomString()
                },
                new()
                {
                    Name = DataUtilityService.GetRandomString(),
                    Type = DataUtilityService.GetRandomString()
                }
            }
        };

        // Act
        var validator = new GetPageContentQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenGetPageContent_ShouldThrowError()
    {
        // Arrange
        var query = new GetPageContentQuery
        {
            Language = "eng",
            Components = new List<ContentModel>
            {
                new()
                {
                    Name = string.Empty,
                    Type = string.Empty
                },
                new()
                {
                    Name = string.Empty,
                    Type = string.Empty
                }
            }
        };

        // Act
        var validator = new GetPageContentQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(4);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}