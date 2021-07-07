using Xunit;
using FluentAssertions;
using System;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Validators.Articles
{
    public class UpdateArticleLikesCommandValidatorTest
    {
        private readonly DataProviderService FDataProviderService;

        public UpdateArticleLikesCommandValidatorTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenValidGuidAndLikes_WhenUpdateArticleLikes_ShouldReturnSuccess()
        {
            // Arrange
            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = Guid.NewGuid(),
                AddToLikes = FDataProviderService.GetRandomInteger(1, 25)
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
                AddToLikes = FDataProviderService.GetRandomInteger(-25, -1)
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
                AddToLikes = FDataProviderService.GetRandomInteger(-25, -1)
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