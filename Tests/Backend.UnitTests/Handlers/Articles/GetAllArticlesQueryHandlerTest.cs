using Xunit;
using FluentAssertions;
using Moq;
using MockQueryable.Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Database;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class GetAllArticlesQueryHandlerTest
    {

        [Fact]
        public async Task GetAllArticles_ShouldReturnCollection() 
        {

            // Arrange
            var LDatabaseContext = new Mock<DatabaseContext>();
            var LGetAllArticlesQuery = new GetAllArticlesQuery { };
            var LGetAllArticlesQueryHandler = new GetAllArticlesQueryHandler(LDatabaseContext.Object);
            var LDummyLoad = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    Title = "Why C# is great?",
                    Description = "More on C#",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    Title = "NET Core 5 is coming",
                    Description = "What's new?",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            };

            var LArticlesDbSet = LDummyLoad.AsQueryable().BuildMockDbSet();
            LDatabaseContext.Setup(AMainDbContext => AMainDbContext.Articles).Returns(LArticlesDbSet.Object);

            // Act
            var LResults = await LGetAllArticlesQueryHandler.Handle(LGetAllArticlesQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);

        }

    }

}
