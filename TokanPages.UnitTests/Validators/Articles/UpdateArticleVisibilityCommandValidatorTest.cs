namespace TokanPages.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Articles;

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