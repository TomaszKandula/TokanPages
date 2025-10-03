using FluentAssertions;
using TokanPages.Backend.Application.Articles.Commands;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Articles;

public class RetrieveArticleInfoCommandValidatorTest
{
    [Fact]
    public void GivenValidIds_WhenRetrieveArticleInfo_ShouldSucceed() 
    {
        // Arrange
        var query = new RetrieveArticleInfoCommand { ArticleIds = new List<Guid>
        {
            Guid.NewGuid(),
            Guid.NewGuid(),
            Guid.NewGuid()
        } };

        // Act
        var validator = new RetrieveArticleInfoCommandValidator();
        var result = validator.Validate(query);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyIds_WhenRetrieveArticleInfo_ShouldThrowError()
    {
        // Arrange
        var getArticleQuery = new RetrieveArticleInfoCommand { ArticleIds = new List<Guid>() };

        // Act
        var validator = new RetrieveArticleInfoCommandValidator();
        var result = validator.Validate(getArticleQuery);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}