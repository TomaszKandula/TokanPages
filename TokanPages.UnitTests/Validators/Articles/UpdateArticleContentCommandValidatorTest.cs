namespace TokanPages.UnitTests.Validators.Articles
{
    using Xunit;
    using FluentAssertions;
    using System;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Commands.Articles;

    public class UpdateArticleContentCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenUpdateArticle_ShouldFinishSuccessful() 
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.Empty,
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyTitle_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = string.Empty,
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTitleIsTooLong_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(256),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyDescription_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = string.Empty,
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongDescription_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(256),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyTextToUpload_WhenUpdateArticle_ShouldFinishSuccessful()
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = DataUtilityService.GetRandomString()
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyImageToUpload_WhenUpdateArticle_ShouldFinishSuccessful()
        {
            // Arrange
            var updateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                TextToUpload = DataUtilityService.GetRandomString(),
                ImageToUpload = string.Empty
            };

            // Act
            var validator = new UpdateArticleContentCommandValidator();
            var result = validator.Validate(updateArticleCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }
    }
}