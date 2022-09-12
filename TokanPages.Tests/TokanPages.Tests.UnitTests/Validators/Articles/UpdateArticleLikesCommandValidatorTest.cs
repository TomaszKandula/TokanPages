namespace TokanPages.Tests.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Articles;

public class UpdateArticleLikesCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenUpdateArticleLikes_ShouldSucceed()
    {
        // Arrange
        var command = new UpdateArticleLikesCommand
        {
            Id = Guid.NewGuid(),
            AddToLikes = DataUtilityService.GetRandomInteger(1, 25)
        };

        // Act
        var validator = new UpdateArticleLikesCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenUpdateArticleLikes_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateArticleLikesCommand
        {
            Id = Guid.Empty,
            AddToLikes = DataUtilityService.GetRandomInteger(1, 25)
        };

        // Act
        var validator = new UpdateArticleLikesCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenNegativeCount_WhenUpdateArticleLikes_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateArticleLikesCommand
        {
            Id = Guid.NewGuid(),
            AddToLikes = DataUtilityService.GetRandomInteger(-25, -1)
        };

        // Act
        var validator = new UpdateArticleLikesCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.LESS_THAN_ZERO));
    }
}