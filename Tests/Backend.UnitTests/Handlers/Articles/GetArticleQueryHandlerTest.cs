using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class GetArticleQueryHandlerTest : TestBase
    {

        [Fact]
        public async Task GetArticle_WhenIdIsCorrect_ShouldReturnEntity() 
        {

            // Arrange
            var LGetArticleQuery = new GetArticleQuery 
            { 
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LTestDate = DateTime.Now;
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = false,
                Likes = 0,
                ReadCount = 0,
                CreatedAt = LTestDate,
                UpdatedAt = null
            };
            LDatabaseContext.Articles.Add(LArticles);
            LDatabaseContext.SaveChanges();

            var LGetArticleQueryHandler = new GetArticleQueryHandler(LDatabaseContext);

            // Act
            var LResults = await LGetArticleQueryHandler.Handle(LGetArticleQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Title.Should().Be(LArticles.Title);
            LResults.Description.Should().Be(LArticles.Description);
            LResults.IsPublished.Should().BeFalse();
            LResults.Likes.Should().Be(LArticles.Likes);
            LResults.ReadCount.Should().Be(LArticles.ReadCount);
            LResults.UpdatedAt.Should().BeNull();
            LResults.CreatedAt.Should().Be(LTestDate);

        }

        [Fact]
        public async Task GetArticle_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LGetArticleQuery = new GetArticleQuery
            {
                Id = Guid.Parse("9bc64ac6-cb57-448e-81b7-32f9a8f2f27c")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            LDatabaseContext.Articles.Add(new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = false,
                Likes = 0,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            });
            LDatabaseContext.SaveChanges();

            var LGetArticleQueryHandler = new GetArticleQueryHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LGetArticleQueryHandler.Handle(LGetArticleQuery, CancellationToken.None));

        }

    }

}
