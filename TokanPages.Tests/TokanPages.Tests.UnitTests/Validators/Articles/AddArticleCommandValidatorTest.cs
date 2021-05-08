﻿using Xunit;
using FluentAssertions;
using TokanPages.Tests.DataProviders;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace TokanPages.Tests.UnitTests.Validators.Articles
{
    public class AddArticleCommandValidatorTest
    {
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenValidateAddArticle_ShouldFinishSuccessfully() 
        {
            // Arrange
            var LAddArticleCommand = new AddArticleCommand 
            { 
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(),
                ImageToUpload = StringProvider.GetRandomString()
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
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(256),
                TextToUpload = StringProvider.GetRandomString(),
                ImageToUpload = StringProvider.GetRandomString()
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
                Title = StringProvider.GetRandomString(256),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(),
                ImageToUpload = StringProvider.GetRandomString()
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
                Title = StringProvider.GetRandomString(),
                Description = string.Empty,
                TextToUpload = StringProvider.GetRandomString(),
                ImageToUpload = StringProvider.GetRandomString()
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
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(),
                ImageToUpload = StringProvider.GetRandomString()
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
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = StringProvider.GetRandomString()
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
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(),
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
