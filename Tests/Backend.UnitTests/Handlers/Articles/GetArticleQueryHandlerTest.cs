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
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class GetArticleQueryHandlerTest
    {

        [Fact]
        public async Task GetArticle_WhenIdIsCorrect_ShouldReturnArticle() 
        {

            // Arrange
            var LGetArticleQuery = new GetArticleQuery 
            { 
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841")
            };

            var LDatabaseContext = new Mock<DatabaseContext>();
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

            var LGetArticleQueryHandler = new GetArticleQueryHandler(LDatabaseContext.Object);

            // Act
            var LResults = await LGetArticleQueryHandler.Handle(LGetArticleQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Title.Should().Be("NET Core 5 is coming");
            LResults.Description.Should().Be("What's new?");
            LResults.IsPublished.Should().BeFalse();
            LResults.Likes.Should().Be(0);
            LResults.ReadCount.Should().Be(0);
            LResults.UpdatedAt.Should().BeNull();
            LResults.CreatedAt.Should().HaveYear(2020);

        }

        [Fact]
        public async Task GetArticle_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LGetArticleQuery = new GetArticleQuery
            {
                Id = Guid.Parse("9bc64ac6-cb57-448e-81b7-32f9a8f2f27c")
            };

            var LDatabaseContext = new Mock<DatabaseContext>();
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
                }
            };

            var LArticlesDbSet = LDummyLoad.AsQueryable().BuildMockDbSet();
            LDatabaseContext.Setup(AMainDbContext => AMainDbContext.Articles).Returns(LArticlesDbSet.Object);

            var LGetArticleQueryHandler = new GetArticleQueryHandler(LDatabaseContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LGetArticleQueryHandler.Handle(LGetArticleQuery, CancellationToken.None));

        }

    }

}
