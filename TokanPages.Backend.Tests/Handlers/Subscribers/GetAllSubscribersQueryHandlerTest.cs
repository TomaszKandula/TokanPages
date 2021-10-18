namespace TokanPages.Backend.Tests.Handlers.Subscribers
{   
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Cqrs.Handlers.Queries.Subscribers;
    using FluentAssertions;
    using Xunit;

    public class GetAllSubscribersQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task WhenGetAllSubscribers_ShouldReturnCollection()
        {
            // Arrange
            var databaseContext = GetTestDatabaseContext();
            var getAllSubscribersQuery = new GetAllSubscribersQuery();
            var getAllSubscribersQueryHandler = new GetAllSubscribersQueryHandler(databaseContext);
            var subscribers = new List<TokanPages.Backend.Domain.Entities.Subscribers>
            {
                new ()
                {
                    Email = DataUtilityService.GetRandomEmail(),
                    IsActivated = true,
                    Count = 10,
                    Registered = DateTime.Now,
                    LastUpdated = null
                },
                new ()
                {
                    Email = DataUtilityService.GetRandomEmail(),
                    IsActivated = true,
                    Count = 100,
                    Registered = DateTime.Now,
                    LastUpdated = null
                }
            };
            
            await databaseContext.Subscribers.AddRangeAsync(subscribers);
            await databaseContext.SaveChangesAsync();

            // Act
            var result = (await getAllSubscribersQueryHandler
                .Handle(getAllSubscribersQuery, CancellationToken.None))
                .ToList();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }
    }
}