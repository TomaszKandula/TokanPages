namespace TokanPages.Backend.Tests.Validators.Articles
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Articles;
    using FluentAssertions;
    using Xunit;

    public class UpdateArticleLikesCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenValidGuidAndLikes_WhenUpdateArticleLikes_ShouldReturnSuccess()
        {
            // Arrange
            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = Guid.NewGuid(),
                AddToLikes = DataUtilityService.GetRandomInteger(1, 25)
            };

            // Act
            var LUpdateArticleLikesCommandValidator = new UpdateArticleLikesCommandValidator();
            var LResult = LUpdateArticleLikesCommandValidator.Validate(LUpdateArticleLikesCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenValidGuidNegativeLikes_WhenUpdateArticleLikes_ShouldThrownError()
        {
            // Arrange
            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = Guid.NewGuid(),
                AddToLikes = DataUtilityService.GetRandomInteger(-25, -1)
            };

            // Act
            var LUpdateArticleLikesCommandValidator = new UpdateArticleLikesCommandValidator();
            var LResult = LUpdateArticleLikesCommandValidator.Validate(LUpdateArticleLikesCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
        }
        
        [Fact]
        public void GivenEmptyGuidAndNegativeLikes_WhenUpdateArticleLikes_ShouldThrownError()
        {
            // Arrange
            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = Guid.Empty,
                AddToLikes = DataUtilityService.GetRandomInteger(-25, -1)
            };

            // Act
            var LUpdateArticleLikesCommandValidator = new UpdateArticleLikesCommandValidator();
            var LResult = LUpdateArticleLikesCommandValidator.Validate(LUpdateArticleLikesCommand);

            // Assert
            LResult.Errors.Count.Should().Be(2);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            LResult.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
        }
    }
}