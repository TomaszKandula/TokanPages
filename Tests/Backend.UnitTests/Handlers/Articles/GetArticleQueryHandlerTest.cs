using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.TestData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using TokanPages.Backend.Cqrs.Services.UserProvider;

namespace Backend.UnitTests.Handlers.Articles
{
    public class GetArticleQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntity() 
        {
            // Arrange
            var LArticleId = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841");
            var LGetArticleQuery = new GetArticleQuery 
            { 
                Id = LArticleId
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LTestDate = DateTime.Now;
            var LUserId = Guid.Parse("d3e2543c-d454-40b6-b8c9-eb1a8845cc62");
            const string USER_ALIAS = "Ester";
            
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = LArticleId,
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = LTestDate,
                UpdatedAt = null,
                UserId = LUserId
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                Id = LUserId,
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = USER_ALIAS,
                Registered = DataProvider.GetRandomDate(),
                LastLogged = null,
                LastUpdated = null
            };

            var LLikes = new List<TokanPages.Backend.Domain.Entities.ArticleLikes> 
            { 
                new TokanPages.Backend.Domain.Entities.ArticleLikes
                {
                    Id = Guid.NewGuid(),
                    ArticleId = LArticleId,
                    UserId = null,
                    LikeCount = 10,
                    IpAddress = "255.255.255.255"
                },
                new TokanPages.Backend.Domain.Entities.ArticleLikes
                {
                    Id = Guid.NewGuid(),
                    ArticleId = LArticleId,
                    UserId = null,
                    LikeCount = 15,
                    IpAddress = "1.1.1.1"
                }
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.ArticleLikes.AddRangeAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns("255.255.255.255");

            var LGetArticleQueryHandler = new GetArticleQueryHandler(LDatabaseContext, LMockedUserProvider.Object);

            // Act
            var LResults = await LGetArticleQueryHandler.Handle(LGetArticleQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Title.Should().Be(LArticles.Title);
            LResults.Description.Should().Be(LArticles.Description);
            LResults.IsPublished.Should().BeFalse();
            LResults.ReadCount.Should().Be(LArticles.ReadCount);
            LResults.UserLikes.Should().Be(10);
            LResults.UpdatedAt.Should().BeNull();
            LResults.CreatedAt.Should().Be(LTestDate);
            LResults.LikeCount.Should().Be(25);
            LResults.Author.AliasName.Should().Be(USER_ALIAS);
            LResults.Author.AvatarName.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticle_ShouldThrowError()
        {
            // Arrange
            var LGetArticleQuery = new GetArticleQuery
            {
                Id = Guid.Parse("9bc64ac6-cb57-448e-81b7-32f9a8f2f27c")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Articles.AddAsync(new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = Guid.NewGuid()
            });
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserProvider>();
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns("255.255.255.255");

            var LGetArticleQueryHandler = new GetArticleQueryHandler(LDatabaseContext, LMockedUserProvider.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LGetArticleQueryHandler.Handle(LGetArticleQuery, CancellationToken.None));
        }
    }
}
