using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Subscribers.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

public class GetSubscriberQueryHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenGetSubscriber_ShouldReturnEntity() 
    {
        // Arrange
        var testDate = DateTime.Now;
        var subscribers = new TokanPages.Backend.Domain.Entities.Subscribers
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10,
            CreatedAt = testDate,
            CreatedBy = Guid.Empty
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var query = new GetSubscriberQuery { Id = subscribers.Id };
        var handler = new GetSubscriberQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(subscribers.Email);
        result.IsActivated.Should().BeTrue();
        result.NewsletterCount.Should().Be(subscribers.Count);
        result.CreatedAt.Should().Be(testDate);
        result.ModifiedAt.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetSubscriber_ShouldThrowError()
    {
        // Arrange
        var subscribers = new TokanPages.Backend.Domain.Entities.Subscribers
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var handler = new GetSubscriberQueryHandler(databaseContext, mockedLogger.Object);
        var query = new GetSubscriberQuery { Id = Guid.NewGuid() };

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
    }
}