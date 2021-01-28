using Xunit;
using FluentAssertions;
using System;
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
            LDatabaseContext.Articles.AddRange(new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    Title = DataProvider.GetRandomString(),
                    Description = DataProvider.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    Title = DataProvider.GetRandomString(),
                    Description = DataProvider.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            });
            LDatabaseContext.SaveChanges();

            // Act
            var LResults = await LGetAllArticlesQueryHandler.Handle(LGetAllArticlesQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);
        }
    }
}
