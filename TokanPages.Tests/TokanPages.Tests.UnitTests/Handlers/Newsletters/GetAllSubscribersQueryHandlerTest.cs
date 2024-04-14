using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Newsletters.Queries;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Newsletters;

public class GetAllSubscribersQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllSubscribers_ShouldReturnCollection()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();

        var query = new GetAllNewslettersQuery();
        var handler = new GetAllNewslettersQueryHandler(databaseContext, mockedLogger.Object);

        var subscribers = new List<TokanPages.Backend.Domain.Entities.Newsletters>
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

        await databaseContext.Newsletters.AddRangeAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ToList().Should().NotBeNull();
        result.ToList().Should().HaveCount(2);
    }
}