namespace TokanPages.Tests.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Articles;

public class AddArticleCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenValidateAddArticle_ShouldSucceed() 
    {
        // Arrange
        var command = new AddArticleCommand 
        { 
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInputs_WhenValidateAddArticle_ShouldThrowError() 
    {
        // Arrange
        var command = new AddArticleCommand
        {
            Title = string.Empty,
            Description = string.Empty,
            TextToUpload = string.Empty,
            ImageToUpload = string.Empty,
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(4);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[2].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        result.Errors[3].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongStrings_WhenValidateAddArticle_ShouldThrowError() 
    {
        // Arrange
        var command = new AddArticleCommand
        {
            Title = DataUtilityService.GetRandomString(500),
            Description = DataUtilityService.GetRandomString(500),
            TextToUpload = DataUtilityService.GetRandomString(),
            ImageToUpload = DataUtilityService.GetRandomString()
        };

        // Act
        var validator = new AddArticleCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(2);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
        result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
    }
}