using Xunit;
using System;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace Backend.UnitTests.Validators.Articles
{

    public class UpdateArticleCommandValidatorTest
    {

        [Fact]
        public void UpdateArticle_WhenAllFieldsAreCorrect_ShouldFinishSuccessfull() 
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();

        }

        [Fact]
        public void UpdateArticle_WhenIdIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Empty,
                Title = "Title",
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void UpdateArticle_WhenTitleIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = string.Empty,
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void UpdateArticle_WhenTitleIsTooLong_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = new string('T', 256),
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));

        }

        [Fact]
        public void UpdateArticle_WhenDescriptionIsEmpty_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = string.Empty,
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));

        }

        [Fact]
        public void UpdateArticle_WhenDescriptionIsTooLong_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = new string('T', 256),
                TextToUpload = "AAA",
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));

        }

        [Fact]
        public void UpdateArticle_WhenTextToUploadIsEmpty_ShouldFinishSuccessfull()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                TextToUpload = string.Empty,
                ImageToUpload = "BBB",
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();

        }

        [Fact]
        public void UpdateArticle_WhenImageToUploadIsEmpty_ShouldFinishSuccessfull()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = string.Empty,
                IsPublished = false,
                Likes = 0,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();

        }

        [Fact]
        public void UpdateArticle_WhenLikesIsLessThanZero_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = string.Empty,
                IsPublished = false,
                Likes = -1,
                ReadCount = 0
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));

        }

        [Fact]
        public void UpdateArticle_WhenReadCountIsLessThanZero_ShouldThrowError()
        {

            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                TextToUpload = "AAA",
                ImageToUpload = string.Empty,
                IsPublished = false,
                Likes = 0,
                ReadCount = -1
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));

        }

    }

}
