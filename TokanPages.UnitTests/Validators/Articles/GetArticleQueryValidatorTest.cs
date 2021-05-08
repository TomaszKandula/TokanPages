using Xunit;
using System;
using FluentAssertions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace TokanPages.UnitTests.Validators.Articles
{
    public class GetArticleQueryValidatorTest
    {
        [Fact]
        public void GivenCorrectId_WhenGetArticle_ShouldFinishSuccessful() 
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
        public void GivenIncorrectId_WhenGetArticle_ShouldThrowError()
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
