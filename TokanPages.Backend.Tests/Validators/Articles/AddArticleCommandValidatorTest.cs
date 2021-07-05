using Xunit;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Services.DataProviderService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace TokanPages.Backend.Tests.Validators.Articles
{
    public class AddArticleCommandValidatorTest
    {
        private readonly DataProviderService FDataProviderService;

        public AddArticleCommandValidatorTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenAllFieldsAreCorrect_WhenValidateAddArticle_ShouldFinishSuccessfully() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand 
            { 
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString()
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
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(256),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString()
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
                Title = FDataProviderService.GetRandomString(256),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString()
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
                Title = FDataProviderService.GetRandomString(),
                Description = string.Empty,
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString()
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
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString()
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
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = FDataProviderService.GetRandomString()
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
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
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
