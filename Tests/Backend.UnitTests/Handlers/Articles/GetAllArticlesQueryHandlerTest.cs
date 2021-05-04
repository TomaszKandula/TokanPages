using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using Backend.DataProviders;

namespace Backend.UnitTests.Handlers.Articles
{
    public class GetAllArticlesQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection() 
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllArticlesQuery = new GetAllArticlesQuery { IsPublished = false };
            var LGetAllArticlesQueryHandler = new GetAllArticlesQueryHandler(LDatabaseContext);

            var LFirstArticleId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LSecondArticleId = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841");

            var LArticles = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = LFirstArticleId,
                    Title = StringProvider.GetRandomString(),
                    Description = StringProvider.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = null,
                    UserId = Guid.NewGuid()
                },
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = LSecondArticleId,
                    Title = StringProvider.GetRandomString(),
                    Description = StringProvider.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-15),
                    UpdatedAt = null,
                    UserId = Guid.NewGuid()
                }
            };

            await LDatabaseContext.Articles.AddRangeAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LResults = (await LGetAllArticlesQueryHandler
                .Handle(LGetAllArticlesQuery, CancellationToken.None))
                .ToList();

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);
            LResults[0].Id.Should().Be(LFirstArticleId);
            LResults[1].Id.Should().Be(LSecondArticleId);
        }
    }
}
