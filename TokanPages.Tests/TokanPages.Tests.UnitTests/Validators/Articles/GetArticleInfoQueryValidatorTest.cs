using FluentAssertions;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Articles;

public class GetArticleInfoQueryValidatorTest
{
    [Fact]
    public void GivenValidId_WhenGetArticleInfo_ShouldSucceed() 
    {
        // Arrange
        var query = new GetArticleInfoQuery { Id = Guid.NewGuid() };

        // Act
        var validator = new GetArticleInfoQueryValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenGetArticleInfo_ShouldThrowError()
    {
        // Arrange
        var getArticleQuery = new GetArticleInfoQuery { Id = Guid.Empty };

        // Act
        var validator = new GetArticleInfoQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.INVALID_GUID_VALUE));
    }

    [Fact]
    public void GivenNoId_WhenGetArticleInfo_ShouldSucceed()
    {
        // Arrange
        var getArticleQuery = new GetArticleInfoQuery { Id = null };

        // Act
        var validator = new GetArticleInfoQueryValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Should().BeEmpty();
    }
}