using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Backend.TestData;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;

namespace Backend.UnitTests.Handlers.Users
{
    public class GetAllUsersQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllUsersQuery = new GetAllUsersQuery();
            var LGetAllUsersQueryHandler = new GetAllUsersQueryHandler(LDatabaseContext);
            await LDatabaseContext.Users.AddRangeAsync(new List<TokanPages.Backend.Domain.Entities.Users>
            {
                new TokanPages.Backend.Domain.Entities.Users
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    EmailAddress = DataProvider.GetRandomEmail(),
                    UserAlias = DataProvider.GetRandomString(),
                    FirstName = DataProvider.GetRandomString(),
                    LastName = DataProvider.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null
                },
                new TokanPages.Backend.Domain.Entities.Users
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    EmailAddress = DataProvider.GetRandomEmail(),
                    UserAlias = DataProvider.GetRandomString(),
                    FirstName = DataProvider.GetRandomString(),
                    LastName = DataProvider.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null
                }
            });
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LResults = await LGetAllUsersQueryHandler.Handle(LGetAllUsersQuery, CancellationToken.None);

            // Assert
            var LGetAllUsersQueryResults = LResults.ToList();
            LGetAllUsersQueryResults.Should().NotBeNull();
            LGetAllUsersQueryResults.Should().HaveCount(2);
        }
    }
}
