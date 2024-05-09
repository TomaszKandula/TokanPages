using FluentAssertions;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Articles;

public class GetArticleQueryValidatorTest
{
    [Fact]
    public void GivenValidId_WhenGetArticle_ShouldSucceed() 
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
    public void GivenEmptyId_WhenGetArticle_ShouldThrowError()
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery { Id = Guid.Empty };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }
    [Fact]

    public void GivenNoId_WhenGetArticle_ShouldSucceed()
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery { Id = null };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    
    [Fact]
    public void GivenValidTitle_WhenGetArticle_ShouldSucceed()
    {
        // Arrange
        var query = new GetArticleQuery { Title = "test-article" };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyTitle_WhenGetArticle_ShouldThrowError()
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery { Title = "" };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
    [Fact]

    public void GivenNoTitle_WhenGetArticle_ShouldSucceed()
    {
        // Arrange
        var getArticleQuery = new GetArticleQuery { Title = null };

        // Act
        var validator = new GetArticleQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Should().BeEmpty();
    }
    
}