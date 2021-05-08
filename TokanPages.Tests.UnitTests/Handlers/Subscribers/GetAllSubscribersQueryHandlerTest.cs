using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using TokanPages.Tests.DataProviders;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

namespace TokanPages.Tests.UnitTests.Handlers.Subscribers
{   
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
                new TokanPages.Backend.Domain.Entities.Subscribers
                {
                    Email = StringProvider.GetRandomEmail(),
                    IsActivated = true,
                    Count = 10,
                    Registered = DateTime.Now,
                    LastUpdated = null
                },
                new TokanPages.Backend.Domain.Entities.Subscribers
                {
                    Email = StringProvider.GetRandomEmail(),
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
