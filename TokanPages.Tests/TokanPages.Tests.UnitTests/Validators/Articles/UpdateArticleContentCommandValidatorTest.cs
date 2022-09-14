using FluentAssertions;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Articles;

public class UpdateArticleContentCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenUpdateArticle_ShouldSucceed() 
    {
        // Arrange
        var command = new UpdateArticleContentCommand
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new UpdateArticleContentCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenUpdateArticle_ShouldThrowError() 
    {
        // Arrange
        var command = new UpdateArticleContentCommand
        {
            Id = Guid.Empty,
            Title = string.Empty,
            Description = string.Empty,
            TextToUpload = string.Empty,
            ImageToUpload = string.Empty
        };

        // Act
        var validator = new UpdateArticleContentCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(3);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
    
    [Fact]
    public void GivenTooLongStrings_WhenUpdateArticle_ShouldThrowError() 
    {
        // Arrange
        var command = new UpdateArticleContentCommand
        {
            Id = Guid.NewGuid(),
            Title = DataUtilityService.GetRandomString(500),
            Description = DataUtilityService.GetRandomString(500),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new UpdateArticleContentCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
    }
}