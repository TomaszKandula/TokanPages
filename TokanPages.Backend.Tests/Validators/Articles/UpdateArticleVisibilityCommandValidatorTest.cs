using Xunit;
using FluentAssertions;
using System;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace TokanPages.Backend.Tests.Validators.Articles
{
    public class UpdateArticleVisibilityCommandValidatorTest
    {
        [Fact]
        public void GivenValidGuidAndPublishFlag_WhenUpdateArticleVisibility_ShouldReturnSuccess()
        {
            // Arrange
            var LUpdateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };

            // Act
            var LUpdateArticleVisibilityCommandValidator = new UpdateArticleVisibilityCommandValidator();
            var LResult = LUpdateArticleVisibilityCommandValidator.Validate(LUpdateArticleVisibilityCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }
        
        [Fact]
        public void GivenEmptyGuidAndPublishFlag_WhenUpdateArticleVisibility_ShouldReturnSuccess()
        {
            // Arrange
            var LUpdateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
            {
                Id = Guid.Empty,
                IsPublished = true
            };

            // Act
            var LUpdateArticleVisibilityCommandValidator = new UpdateArticleVisibilityCommandValidator();
            var LResult = LUpdateArticleVisibilityCommandValidator.Validate(LUpdateArticleVisibilityCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}