namespace TokanPages.Tests.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Commands.Articles;

public class UpdateArticleCountCommandValidatorTest
{
    [Fact]
    public void GivenValidGuid_WhenUpdateArticleCount_ShouldSucceed()
    {
        // Arrange
        var command = new UpdateArticleCountCommand { Id = Guid.NewGuid() };

        // Act
        var validator = new UpdateArticleCountCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyGuid_WhenUpdateArticleCount_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateArticleCountCommand { Id = Guid.Empty };

        // Act
        var validator = new UpdateArticleCountCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}