using FluentAssertions;
using TokanPages.Backend.Application.Content.Components.Commands;
using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Content.Components;

public class RequestPageDataCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenRequestPageData_ShouldSucceed()
    {
        // Arrange
        var query = new RequestPageDataCommand
        {
            Language = "eng",
            Components = new List<ContentModel>
            {
                new()
                {
                    ContentName = DataUtilityService.GetRandomString()
                },
                new()
                {
                    ContentName = DataUtilityService.GetRandomString()
                }
            }
        };

        // Act
        var validator = new RequestPageDataCommandValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenRequestPageData_ShouldThrowError()
    {
        // Arrange
        var query = new RequestPageDataCommand
        {
            Language = "eng",
            Components = new List<ContentModel>
            {
                new()
                {
                    ContentName = string.Empty
                },
                new()
                {
                    ContentName = string.Empty
                }
            }
        };

        // Act
        var validator = new RequestPageDataCommandValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenNoInput_WhenRequestPageData_ShouldThrowError()
    {
        // Arrange
        var query = new RequestPageDataCommand
        {
            Language = "eng",
            Components = new List<ContentModel>()
        };

        // Act
        var validator = new RequestPageDataCommandValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LIST_EMPTY));
    }
}