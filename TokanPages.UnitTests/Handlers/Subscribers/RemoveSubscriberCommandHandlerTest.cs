namespace TokanPages.UnitTests.Handlers.Subscribers;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Subscribers;

public class RemoveSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveSubscriber_ShouldRemoveEntity() 
    {
        // Arrange
        var subscribers = new Subscribers 
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            Registered = DateTime.Now,
            LastUpdated = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var removeSubscriberCommand = new RemoveSubscriberCommand { Id = subscribers.Id };
        var removeSubscriberCommandHandler = new RemoveSubscriberCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        await removeSubscriberCommandHandler.Handle(removeSubscriberCommand, CancellationToken.None);

        // Assert
        var assertDbContext = GetTestDatabaseContext();
        var subscribersEntity = await assertDbContext.Subscribers.FindAsync(removeSubscriberCommand.Id);
        subscribersEntity.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenRemoveSubscriber_ShouldThrowError()
    {
        // Arrange
        var removeSubscriberCommand = new RemoveSubscriberCommand
        {
            Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var removeSubscriberCommandHandler = new RemoveSubscriberCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() 
            => removeSubscriberCommandHandler.Handle(removeSubscriberCommand, CancellationToken.None));
    }
}