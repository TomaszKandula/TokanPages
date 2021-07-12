using Xunit;
using FluentAssertions;
using System;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace TokanPages.Backend.Tests.Validators.Articles
{
    public class UpdateArticleCountCommandValidatorTest
    {
        [Fact]
        public void GivenValidGuid_WhenUpdateArticleCount_ShouldReturnSuccessful()
        {
            // Arrange
            var LUpdateArticleCountCommand = new UpdateArticleCountCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            var LUpdateArticleCountCommandValidator = new UpdateArticleCountCommandValidator();
            var LResult = LUpdateArticleCountCommandValidator.Validate(LUpdateArticleCountCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyGuid_WhenUpdateArticleCount_ShouldThrowError()
        {
            // Arrange
            var LUpdateArticleCountCommand = new UpdateArticleCountCommand
            {
                Id = Guid.Empty
            };

            // Act
            var LUpdateArticleCountCommandValidator = new UpdateArticleCountCommandValidator();
            var LResult = LUpdateArticleCountCommandValidator.Validate(LUpdateArticleCountCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
        
    }
}