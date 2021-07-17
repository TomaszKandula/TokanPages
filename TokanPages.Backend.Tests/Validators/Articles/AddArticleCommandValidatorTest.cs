namespace TokanPages.Backend.Tests.Validators.Articles
{
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Articles;
    using FluentAssertions;
    using Xunit;

    public class AddArticleCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenValidateAddArticle_ShouldFinishSuccessfully() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand 
            { 
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenDescriptionTooLong_WhenValidateAddArticle_ShouldThrowError() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(256),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
        }

        [Fact]
        public void GivenTitleTooLong_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataUtilityService.GetRandomString(256),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
        }

        [Fact]
        public void GivenDescriptionEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataUtilityService.GetRandomString(),
                Description = string.Empty,
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTitleEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = string.Empty,
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTextToUploadEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenImageToUploadEmpty_WhenValidateAddArticle_ShouldThrowError()
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = string.Empty
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}