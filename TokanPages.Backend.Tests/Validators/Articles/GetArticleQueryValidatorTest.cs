namespace TokanPages.Backend.Tests.Validators.Articles
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Queries.Articles;
    using FluentAssertions;
    using Xunit;

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