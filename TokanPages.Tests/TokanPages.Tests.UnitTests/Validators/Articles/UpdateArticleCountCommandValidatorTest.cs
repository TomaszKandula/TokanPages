namespace TokanPages.Tests.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Articles;

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