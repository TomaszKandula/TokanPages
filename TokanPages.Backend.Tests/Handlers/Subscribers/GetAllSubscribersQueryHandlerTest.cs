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
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetAllSubscribersQuery = new GetAllSubscribersQuery();
            var LGetAllSubscribersQueryHandler = new GetAllSubscribersQueryHandler(LDatabaseContext);
            var LSubscribers = new List<TokanPages.Backend.Domain.Entities.Subscribers>
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
            
            await LDatabaseContext.Subscribers.AddRangeAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LResults = (await LGetAllSubscribersQueryHandler
                .Handle(LGetAllSubscribersQuery, CancellationToken.None))
                .ToList();

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);
        }
    }
}