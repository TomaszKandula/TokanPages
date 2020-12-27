using Xunit;
using FluentAssertions;
using Backend.TestData;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace Backend.UnitTests.Validators.Articles
{

    public class AddArticleCommandValidatorTest
    {

        [Fact]
        public void ValidateAddArticle_WhenAllFieldsAreCorrect_ShouldFinishSuccessfully() 
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand 
            { 
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(),
                ImageToUpload = DataProvider.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();

        }

        [Fact]
        public void ValidateAddArticle_WhenDescriptionTooLong_ShouldThrowError() 
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(256),
                TextToUpload = DataProvider.GetRandomString(),
                ImageToUpload = DataProvider.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));

        }

        [Fact]
        public void ValidateAddArticle_WhenTitleTooLong_ShouldThrowError()
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataProvider.GetRandomString(256),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(),
                ImageToUpload = DataProvider.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));

        }

        [Fact]
        public void ValidateAddArticle_WhenDescriptionEmpty_ShouldThrowError()
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataProvider.GetRandomString(),
                Description = string.Empty,
                TextToUpload = DataProvider.GetRandomString(),
                ImageToUpload = DataProvider.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void ValidateAddArticle_WhenTitleEmpty_ShouldThrowError()
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = string.Empty,
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(),
                ImageToUpload = DataProvider.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void ValidateAddArticle_WhenTextToUploadEmpty_ShouldThrowError()
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = DataProvider.GetRandomString()
            };

            // Act
            var LValidator = new AddArticleCommandValidator();
            var LResult = LValidator.Validate(LAddArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void ValidateAddArticle_WhenImageToUploadEmpty_ShouldThrowError()
        {

            // Arrange
            var LAddArticleCommand = new AddArticleCommand
            {
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(),
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
