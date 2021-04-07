using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;
using Backend.TestData;

namespace Backend.UnitTests.Handlers.Subscribers
{   
    public class GetAllSubscribersQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GetAllSubscribers_ShouldReturnCollection()
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllSubscribersQuery = new GetAllSubscribersQuery();
            var LGetAllSubscribersQueryHandler = new GetAllSubscribersQueryHandler(LDatabaseContext);
            var LSubscribers = new List<TokanPages.Backend.Domain.Entities.Subscribers>
            {
                new TokanPages.Backend.Domain.Entities.Subscribers
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    Email = DataProvider.GetRandomEmail(),
                    IsActivated = true,
                    Count = 10,
                    Registered = DateTime.Now,
                    LastUpdated = null
                },
                new TokanPages.Backend.Domain.Entities.Subscribers
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    Email = DataProvider.GetRandomEmail(),
                    IsActivated = true,
                    Count = 100,
                    Registered = DateTime.Now,
                    LastUpdated = null
                }
            };
            await LDatabaseContext.Subscribers.AddRangeAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LResults = await LGetAllSubscribersQueryHandler.Handle(LGetAllSubscribersQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);
        }
    }
}
