using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Core.Generators;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace TokanPages.UnitTests.Handlers.Articles
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
            
            var LUser = new Backend.Domain.Entities.Users
            {
                UserAlias  = StringProvider.GetRandomString(),
                IsActivated = true,
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                EmailAddress = StringProvider.GetRandomEmail(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = DateTimeProvider.GetRandom(),
                LastUpdated = DateTimeProvider.GetRandom(),
                AvatarName = StringProvider.GetRandomString(),
                ShortBio = StringProvider.GetRandomString()
            };

            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();
            
            var LArticles = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Title = StringProvider.GetRandomString(),
                    Description = StringProvider.GetRandomString(),
                    IsPublished = false,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now.AddDays(-10),
                    UpdatedAt = null,
                    UserId = LUser.Id
                },
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Title = StringProvider.GetRandomString(),
                    Description = StringProvider.GetRandomString(),
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
