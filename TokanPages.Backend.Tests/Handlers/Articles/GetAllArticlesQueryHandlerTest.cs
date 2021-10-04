namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Core.Utilities.DataUtilityService;
    using Cqrs.Handlers.Queries.Articles;
    using FluentAssertions;
    using Xunit;

    public class GetAllArticlesQueryHandlerTest : TestBase
    {
        private readonly DataUtilityService FDataUtilityService;

        public GetAllArticlesQueryHandlerTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection() 
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllArticlesQuery = new GetAllArticlesQuery { IsPublished = false };
            var LGetAllArticlesQueryHandler = new GetAllArticlesQueryHandler(LDatabaseContext);
            
            var LUser = new Backend.Domain.Entities.Users
            {
                UserAlias  = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                Registered = FDataUtilityService.GetRandomDateTime(),
                LastLogged = FDataUtilityService.GetRandomDateTime(),
                LastUpdated = FDataUtilityService.GetRandomDateTime(),
                AvatarName = FDataUtilityService.GetRandomString(),
                ShortBio = FDataUtilityService.GetRandomString(),
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();
            
            var LArticles = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new ()
                {
                    Title = FDataUtilityService.GetRandomString(),
                    Description = FDataUtilityService.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = null,
                    UserId = LUser.Id
                },
                new ()
                {
                    Title = FDataUtilityService.GetRandomString(),
                    Description = FDataUtilityService.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-15),
                    UpdatedAt = null,
                    UserId = LUser.Id
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
            LResults[0].Id.Should().Be(LArticles[0].Id);
            LResults[1].Id.Should().Be(LArticles[1].Id);
        }
    }
}