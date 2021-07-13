namespace TokanPages.Backend.Tests.Validators.Articles
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Articles;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;

    public class UpdateArticleContentCommandValidatorTest
    {
        private readonly DataUtilityService FDataUtilityService;

        public UpdateArticleContentCommandValidatorTest() => FDataUtilityService = new DataUtilityService();
        
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenUpdateArticle_ShouldFinishSuccessful() 
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.Empty,
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyTitle_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = string.Empty,
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTitleIsTooLong_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataUtilityService.GetRandomString(256),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyDescription_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataUtilityService.GetRandomString(),
                Description = string.Empty,
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongDescription_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(256),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyTextToUpload_WhenUpdateArticle_ShouldFinishSuccessful()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = FDataUtilityService.GetRandomString()
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyImageToUpload_WhenUpdateArticle_ShouldFinishSuccessful()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleContentCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                TextToUpload = FDataUtilityService.GetRandomString(),
                ImageToUpload = string.Empty
            };

            // Act
            var LValidator = new UpdateArticleContentCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }
    }
}