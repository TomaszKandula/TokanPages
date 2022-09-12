using System;
using FluentAssertions;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Articles;

public class RemoveArticleCommandValidatorTest
{
    [Fact]
    public void GivenValidInput_WhenRemoveArticle_ShouldSucceed() 
    {
        // Arrange
        var removeArticleCommand = new RemoveArticleCommand { Id = Guid.NewGuid() };

        // Act
        var validator = new RemoveArticleCommandValidator();
        var result = validator.Validate(removeArticleCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInput_WhenRemoveArticle_ShouldThrowError()
    {
        // Arrange
        var removeArticleCommand = new RemoveArticleCommand { Id = Guid.Empty };

        // Act
        var validator = new RemoveArticleCommandValidator();
        var result = validator.Validate(removeArticleCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}