namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Core.Utilities.LoggerService;
using Backend.Application.Handlers.Queries.Subscribers;

public class GetAllSubscribersQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllSubscribers_ShouldReturnCollection()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();

        var query = new GetAllSubscribersQuery();
        var handler = new GetAllSubscribersQueryHandler(databaseContext, mockedLogger.Object);

        var subscribers = new List<TokanPages.Backend.Domain.Entities.Subscribers>
        {
            new()
            {
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                CreatedAt = DataUtilityService.GetRandomDateTime(),
                CreatedBy = Guid.Empty
            },
            new()
            {
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 100,
                CreatedAt = DataUtilityService.GetRandomDateTime(),
                CreatedBy = Guid.Empty
            }
        };

        await databaseContext.Subscribers.AddRangeAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ToList().Should().NotBeNull();
        result.ToList().Should().HaveCount(2);
    }
}