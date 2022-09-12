namespace TokanPages.Tests.UnitTests.Validators.Articles;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Application.Handlers.Queries.Articles;

public class GetArticleQueryValidatorTest
{
    [Fact]
    public void GivenValidInput_WhenGetArticle_ShouldSucceed() 
    {
        // Arrange
        var query = new GetArticleQuery { Id = Guid.NewGuid() };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInput_WhenGetArticle_ShouldThrowError()
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery { Id = Guid.Empty };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}