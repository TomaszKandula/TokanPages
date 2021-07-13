namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Core.Exceptions;
    using Cqrs.Handlers.Queries.Articles;
    using Cqrs.Services.UserServiceProvider;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class GetArticleQueryHandlerTest : TestBase
    {
        private const string USER_ALIAS = "Ester";

        private const string IP_ADDRESS_FIRST = "255.255.255.255";
        
        private const string IP_ADDRESS_SECOND = "1.1.1.1";
        
        private readonly DataUtilityService FDataUtilityService;

        public GetArticleQueryHandlerTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntity() 
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LTestDate = DateTime.Now;
            
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = USER_ALIAS,
                Registered = FDataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = LTestDate,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LLikes = new List<Domain.Entities.ArticleLikes> 
            { 
                new ()
                {
                    ArticleId = LArticles.Id,
                    UserId = null,
                    LikeCount = 10,
                    IpAddress = IP_ADDRESS_FIRST
                },
                new ()
                {
                    ArticleId = LArticles.Id,
                    UserId = null,
                    LikeCount = 15,
                    IpAddress = IP_ADDRESS_SECOND
                }
            };
            
            await LDatabaseContext.ArticleLikes.AddRangeAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedUserProvider = new Mock<UserServiceProvider>();
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS_FIRST);

            var LGetArticleQuery = new GetArticleQuery { Id = LArticles.Id };
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

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                Registered = FDataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>();
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS_FIRST);

            var LGetArticleQueryHandler = new GetArticleQueryHandler(LDatabaseContext, LMockedUserProvider.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LGetArticleQueryHandler.Handle(LGetArticleQuery, CancellationToken.None));
        }
    }
}