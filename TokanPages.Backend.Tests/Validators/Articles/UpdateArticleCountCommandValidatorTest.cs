namespace TokanPages.Backend.Tests.Validators.Articles
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Articles;
    using FluentAssertions;
    using Xunit;

    public class UpdateArticleCountCommandValidatorTest
    {
        [Fact]
        public void GivenValidGuid_WhenUpdateArticleCount_ShouldReturnSuccessful()
        {
            // Arrange
            var updateArticleCountCommand = new UpdateArticleCountCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            var updateArticleCountCommandValidator = new UpdateArticleCountCommandValidator();
            var result = updateArticleCountCommandValidator.Validate(updateArticleCountCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyGuid_WhenUpdateArticleCount_ShouldThrowError()
        {
            // Arrange
            var updateArticleCountCommand = new UpdateArticleCountCommand
            {
                Id = Guid.Empty
            };

            // Act
            var updateArticleCountCommandValidator = new UpdateArticleCountCommandValidator();
            var result = updateArticleCountCommandValidator.Validate(updateArticleCountCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
    }
}