using Xunit;
using System;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace TokanPages.Tests.UnitTests.Validators.Articles
{
    public class RemoveArticleCommandValidatorTest
    {
        [Fact]
        public void GivenCorrectId_WhenRemoveArticle_ShouldFinishSuccessfully() 
        {
            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            var LValidator = new RemoveArticleCommandValidator();
            var LResult = LValidator.Validate(LRemoveArticleCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenIncorrectId_WhenRemoveArticle_ShouldThrowError()
        {
            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Empty
            };

            // Act
            var LValidator = new RemoveArticleCommandValidator();
            var LResult = LValidator.Validate(LRemoveArticleCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}
