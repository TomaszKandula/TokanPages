using System;
using FluentAssertions;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Articles;

public class UpdateArticleVisibilityCommandValidatorTest
{
    [Fact]
    public void GivenValidInputs_WhenUpdateArticleVisibility_ShouldSucceed()
    {
        // Arrange
        var command = new UpdateArticleVisibilityCommand
        {
            Id = Guid.NewGuid(),
            IsPublished = true
        };

        // Act
        var validator = new UpdateArticleVisibilityCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }
        
    [Fact]
    public void GivenEmptyId_WhenUpdateArticleVisibility_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateArticleVisibilityCommand
        {
            Id = Guid.Empty,
            IsPublished = true
        };

        // Act
        var validator = new UpdateArticleVisibilityCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}