using Xunit;
using FluentAssertions;
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
                Title = "Title",
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = "BBB"
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
                Title = "Title",
                Description = new string('T', 256),
                TextToUpload = "AAA",
                ImageToUpload = "BBB"
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
                Title = new string('T', 256),
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = "BBB"
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
                Title = "Title",
                Description = string.Empty,
                TextToUpload = "AAA",
                ImageToUpload = "BBB"
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
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = "BBB"
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
                Title = "Title",
                Description = "Description",
                TextToUpload = string.Empty,
                ImageToUpload = "BBB"
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
                Title = "Title",
                Description = "Description",
                TextToUpload = "AAA",
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
