namespace TokanPages.UnitTests.Validators.Articles
{
    using Xunit;
    using FluentAssertions;
    using System;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Commands.Articles;

    public class RemoveArticleCommandValidatorTest
    {
        [Fact]
        public void GivenCorrectId_WhenRemoveArticle_ShouldFinishSuccessfully() 
        {
            // Arrange
            var removeArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            var validator = new RemoveArticleCommandValidator();
            var result = validator.Validate(removeArticleCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenIncorrectId_WhenRemoveArticle_ShouldThrowError()
        {
            // Arrange
            var removeArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Empty
            };

            // Act
            var validator = new RemoveArticleCommandValidator();
            var result = validator.Validate(removeArticleCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}