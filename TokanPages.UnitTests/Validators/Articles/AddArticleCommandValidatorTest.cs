namespace TokanPages.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Articles;

public class AddArticleCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenAllFieldsAreCorrect_WhenValidateAddArticle_ShouldFinishSuccessfully() 
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand 
        { 
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(addArticleCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenDescriptionTooLong_WhenValidateAddArticle_ShouldThrowError() 
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(256),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(addArticleCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
    }

    [Fact]
    public void GivenTitleTooLong_WhenValidateAddArticle_ShouldThrowError()
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = DataUtilityService.GetRandomString(256),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(addArticleCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
    }

    [Fact]
    public void GivenDescriptionEmpty_WhenValidateAddArticle_ShouldThrowError()
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = DataUtilityService.GetRandomString(),
            Description = string.Empty,
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(addArticleCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTitleEmpty_WhenValidateAddArticle_ShouldThrowError()
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = string.Empty,
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(addArticleCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTextToUploadEmpty_WhenValidateAddArticle_ShouldThrowError()
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = string.Empty,
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(addArticleCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenImageToUploadEmpty_WhenValidateAddArticle_ShouldThrowError()
    {
        // Arrange
        var addArticleCommand = new AddArticleCommand
        {
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = string.Empty
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(addArticleCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}