using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using Backend.TestData;

namespace Backend.UnitTests.Handlers.Articles
{
    public class GetAllArticlesQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GetAllArticles_ShouldReturnCollection() 
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllArticlesQuery = new GetAllArticlesQuery { IsPublished = false };
            var LGetAllArticlesQueryHandler = new GetAllArticlesQueryHandler(LDatabaseContext);

            var FirstArticleId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var SecondArticleId = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841");

            var LArticles = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = FirstArticleId,
                    Title = DataProvider.GetRandomString(),
                    Description = DataProvider.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = null,
                    UserId = Guid.NewGuid()
                },
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = SecondArticleId,
                    Title = DataProvider.GetRandomString(),
                    Description = DataProvider.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-15),
                    UpdatedAt = null,
                    UserId = Guid.NewGuid()
                }
            };

            LDatabaseContext.Articles.AddRange(LArticles);
            LDatabaseContext.SaveChanges();

            // Act
            var LResults = (await LGetAllArticlesQueryHandler.Handle(LGetAllArticlesQuery, CancellationToken.None)).ToList();

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);
            LResults[0].Id.Should().Be(FirstArticleId);
            LResults[1].Id.Should().Be(SecondArticleId);
        }
    }
}
