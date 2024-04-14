using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

public class RemoveSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveSubscriber_ShouldRemoveEntity() 
    {
        // Arrange
        var subscribers = new Backend.Domain.Entities.Newsletters 
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Newsletters.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var command = new RemoveNewsletterCommand { Id = subscribers.Id };
        var handler = new RemoveNewsletterCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var assertDbContext = GetTestDatabaseContext();
        var subscribersEntity = await assertDbContext.Newsletters.FindAsync(command.Id);
        subscribersEntity.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenRemoveSubscriber_ShouldThrowError()
    {
        // Arrange
        var command = new RemoveNewsletterCommand { Id = Guid.NewGuid() };
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var handler = new RemoveNewsletterCommandHandler(databaseContext, mockedLogger.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
    }
}