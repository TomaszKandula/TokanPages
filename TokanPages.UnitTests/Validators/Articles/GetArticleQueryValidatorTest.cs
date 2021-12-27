namespace TokanPages.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Queries.Articles;

public class GetArticleQueryValidatorTest
{
    [Fact]
    public void GivenCorrectId_WhenGetArticle_ShouldFinishSuccessful() 
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery 
        { 
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenIncorrectId_WhenGetArticle_ShouldThrowError()
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}