﻿using Xunit;
using System;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Services.DataProviderService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace TokanPages.Backend.Tests.Validators.Articles
{
    public class UpdateArticleCommandValidatorTest
    {
        private readonly DataProviderService FDataProviderService;

        public UpdateArticleCommandValidatorTest() => FDataProviderService = new DataProviderService();
        
        [Fact]
        public void GivenAllFieldsAreCorrect_WhenUpdateArticle_ShouldFinishSuccessful() 
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyId_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.Empty,
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenEmptyTitle_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = string.Empty,
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTitleIsTooLong_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(256),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TITLE_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyDescription_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(),
                Description = string.Empty,
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }

        [Fact]
        public void GivenTooLongDescription_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(256),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = FDataProviderService.GetRandomString(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.DESCRIPTION_TOO_LONG));
        }

        [Fact]
        public void GivenEmptyTextToUpload_WhenUpdateArticle_ShouldFinishSuccessful()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = string.Empty,
                ImageToUpload = FDataProviderService.GetRandomString(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyImageToUpload_WhenUpdateArticle_ShouldFinishSuccessful()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = string.Empty,
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            // Act
            var LValidator = new UpdateArticleCommandValidator();
            var LResult = LValidator.Validate(LUpdateArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenLikesIsLessThanZero_WhenUpdateArticle_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCommand = new UpdateArticleCommand
            {
                Id = Guid.NewGuid(),
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                TextToUpload = FDataProviderService.GetRandomString(),
                ImageToUpload = string.Empty,
                IsPublished = false,
                AddToLikes = -1,
                UpReadCount = false
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
