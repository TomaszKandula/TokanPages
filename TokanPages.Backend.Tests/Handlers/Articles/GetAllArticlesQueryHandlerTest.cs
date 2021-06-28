using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using TokanPages.Backend.Core.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Handlers.Articles
{
    public class GetAllArticlesQueryHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public GetAllArticlesQueryHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection() 
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllArticlesQuery = new GetAllArticlesQuery { IsPublished = false };
            var LGetAllArticlesQueryHandler = new GetAllArticlesQueryHandler(LDatabaseContext);
            
            var LUser = new Backend.Domain.Entities.Users
            {
                UserAlias  = FDataProviderService.GetRandomString(),
                IsActivated = true,
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                EmailAddress = FDataProviderService.GetRandomEmail(),
                Registered = FDataProviderService.GetRandomDateTime(),
                LastLogged = FDataProviderService.GetRandomDateTime(),
                LastUpdated = FDataProviderService.GetRandomDateTime(),
                AvatarName = FDataProviderService.GetRandomString(),
                ShortBio = FDataProviderService.GetRandomString()
            };

            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();
            
            var LArticles = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new ()
                {
                    Title = FDataProviderService.GetRandomString(),
                    Description = FDataProviderService.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = null,
                    UserId = LUser.Id
                },
                new ()
                {
                    Title = FDataProviderService.GetRandomString(),
                    Description = FDataProviderService.GetRandomString(),
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
