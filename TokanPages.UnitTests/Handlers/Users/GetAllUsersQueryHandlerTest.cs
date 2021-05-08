using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.DataProviders;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;

namespace TokanPages.UnitTests.Handlers.Users
{
    public class GetAllUsersQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            var LUsers = new List<TokanPages.Backend.Domain.Entities.Users>
            {
                new TokanPages.Backend.Domain.Entities.Users
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    EmailAddress = StringProvider.GetRandomEmail(),
                    UserAlias = StringProvider.GetRandomString(),
                    FirstName = StringProvider.GetRandomString(),
                    LastName = StringProvider.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null
                },
                new TokanPages.Backend.Domain.Entities.Users
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    EmailAddress = StringProvider.GetRandomEmail(),
                    UserAlias = StringProvider.GetRandomString(),
                    FirstName = StringProvider.GetRandomString(),
                    LastName = StringProvider.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null
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
