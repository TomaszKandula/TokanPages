namespace TokanPages.Backend.Tests.Validators.Articles
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Articles;
    using FluentAssertions;
    using Xunit;

    public class UpdateArticleVisibilityCommandValidatorTest
    {
        [Fact]
        public void GivenValidGuidAndPublishFlag_WhenUpdateArticleVisibility_ShouldReturnSuccess()
        {
            // Arrange
            var updateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
            {
                Id = Guid.NewGuid(),
                IsPublished = true
            };

            // Act
            var updateArticleVisibilityCommandValidator = new UpdateArticleVisibilityCommandValidator();
            var result = updateArticleVisibilityCommandValidator.Validate(updateArticleVisibilityCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }
        
        [Fact]
        public void GivenEmptyGuidAndPublishFlag_WhenUpdateArticleVisibility_ShouldReturnSuccess()
        {
            // Arrange
            var updateArticleVisibilityCommand = new UpdateArticleVisibilityCommand
            {
                Id = Guid.Empty,
                IsPublished = true
            };

            // Act
            var updateArticleVisibilityCommandValidator = new UpdateArticleVisibilityCommandValidator();
            var result = updateArticleVisibilityCommandValidator.Validate(updateArticleVisibilityCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}