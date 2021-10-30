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
            var updateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = Guid.NewGuid(),
                AddToLikes = DataUtilityService.GetRandomInteger(1, 25)
            };

            // Act
            var updateArticleLikesCommandValidator = new UpdateArticleLikesCommandValidator();
            var result = updateArticleLikesCommandValidator.Validate(updateArticleLikesCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenValidGuidNegativeLikes_WhenUpdateArticleLikes_ShouldThrownError()
        {
            // Arrange
            var updateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = Guid.NewGuid(),
                AddToLikes = DataUtilityService.GetRandomInteger(-25, -1)
            };

            // Act
            var updateArticleLikesCommandValidator = new UpdateArticleLikesCommandValidator();
            var result = updateArticleLikesCommandValidator.Validate(updateArticleLikesCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
        }
        
        [Fact]
        public void GivenEmptyGuidAndNegativeLikes_WhenUpdateArticleLikes_ShouldThrownError()
        {
            // Arrange
            var updateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = Guid.Empty,
                AddToLikes = DataUtilityService.GetRandomInteger(-25, -1)
            };

            // Act
            var updateArticleLikesCommandValidator = new UpdateArticleLikesCommandValidator();
            var result = updateArticleLikesCommandValidator.Validate(updateArticleLikesCommand);

            // Assert
            result.Errors.Count.Should().Be(2);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
            result.Errors[1].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
        }
    }
}