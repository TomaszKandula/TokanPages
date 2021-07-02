using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;
using TokanPages.Backend.Core.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Handlers.Users
{
    public class GetAllUsersQueryHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public GetAllUsersQueryHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            var LUsers = new List<TokanPages.Backend.Domain.Entities.Users>
            {
                new ()
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    EmailAddress = FDataProviderService.GetRandomEmail(),
                    UserAlias = FDataProviderService.GetRandomString(),
                    FirstName = FDataProviderService.GetRandomString(),
                    LastName = FDataProviderService.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null,
                    CryptedPassword = FDataProviderService.GetRandomString()
                },
                new ()
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    EmailAddress = FDataProviderService.GetRandomEmail(),
                    UserAlias = FDataProviderService.GetRandomString(),
                    FirstName = FDataProviderService.GetRandomString(),
                    LastName = FDataProviderService.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null,
                    CryptedPassword = FDataProviderService.GetRandomString()
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllUsersQuery = new GetAllUsersQuery();
            var LGetAllUsersQueryHandler = new GetAllUsersQueryHandler(LDatabaseContext);

            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LResults = (await LGetAllUsersQueryHandler
                .Handle(LGetAllUsersQuery, CancellationToken.None))
                .ToList();

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);
        }
    }
}
