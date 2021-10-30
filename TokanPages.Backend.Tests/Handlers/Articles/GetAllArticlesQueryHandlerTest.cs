namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Core.Utilities.LoggerService;
    using Domain.Entities;
    using Cqrs.Handlers.Queries.Articles;

    public class GetAllArticlesQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection() 
        {
            // Arrange
            var databaseContext = GetTestDatabaseContext();
            var mockedLogger = new Mock<ILoggerService>();

            var getAllArticlesQuery = new GetAllArticlesQuery { IsPublished = false };
            var getAllArticlesQueryHandler = new GetAllArticlesQueryHandler(databaseContext, mockedLogger.Object);
            
            var user = new Users
            {
                UserAlias  = DataUtilityService.GetRandomString(),
                IsActivated = true,
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                Registered = DataUtilityService.GetRandomDateTime(),
                LastLogged = DataUtilityService.GetRandomDateTime(),
                LastUpdated = DataUtilityService.GetRandomDateTime(),
                AvatarName = DataUtilityService.GetRandomString(),
                ShortBio = DataUtilityService.GetRandomString(),
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();
            
            var articles = new List<Articles>
            {
                new ()
                {
                    Title = DataUtilityService.GetRandomString(),
                    Description = DataUtilityService.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = null,
                    UserId = user.Id
                },
                new ()
                {
                    Title = DataUtilityService.GetRandomString(),
                    Description = DataUtilityService.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-15),
                    UpdatedAt = null,
                    UserId = user.Id
                }
            };

            await databaseContext.Articles.AddRangeAsync(articles);
            await databaseContext.SaveChangesAsync();

            // Act
            var result = (await getAllArticlesQueryHandler
                .Handle(getAllArticlesQuery, CancellationToken.None))
                .ToList();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Id.Should().Be(articles[0].Id);
            result[1].Id.Should().Be(articles[1].Id);
        }
    }
}