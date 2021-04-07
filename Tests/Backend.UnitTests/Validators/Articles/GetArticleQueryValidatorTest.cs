using Xunit;
using System;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace Backend.UnitTests.Validators.Articles
{
    public class GetArticleQueryValidatorTest
    {
        [Fact]
        public void GetArticle_WhenIdIsCorrect_ShouldFinishSuccessful() 
        {
            // Arrange
            var LGetArticleQuery = new GetArticleQuery 
            { 
                Id = Guid.NewGuid()
            };

            // Act
            var LValidator = new GetArticleQueryValidator();
            var LResult = LValidator.Validate(LGetArticleQuery);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GetArticle_WhenIdIsIncorrect_ShouldThrowError()
        {
            // Arrange
            var LGetArticleQuery = new GetArticleQuery
            {
                Id = Guid.Empty
            };

            // Act
            var LValidator = new GetArticleQueryValidator();
            var LResult = LValidator.Validate(LGetArticleQuery);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}
