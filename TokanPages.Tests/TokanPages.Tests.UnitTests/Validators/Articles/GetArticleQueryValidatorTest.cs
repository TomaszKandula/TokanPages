using System;
using FluentAssertions;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Articles;

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