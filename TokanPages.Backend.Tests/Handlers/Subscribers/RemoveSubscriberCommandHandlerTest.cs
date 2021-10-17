namespace TokanPages.Backend.Tests.Handlers.Subscribers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Commands.Subscribers;
    using FluentAssertions;
    using Xunit;

    public class RemoveSubscriberCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenRemoveSubscriber_ShouldRemoveEntity() 
        {
            // Arrange
            var subscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
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

            var removeSubscriberCommand = new RemoveSubscriberCommand { Id = subscribers.Id };
            var removeSubscriberCommandHandler = new RemoveSubscriberCommandHandler(databaseContext);

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
            var removeSubscriberCommandHandler = new RemoveSubscriberCommandHandler(databaseContext);

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => removeSubscriberCommandHandler.Handle(removeSubscriberCommand, CancellationToken.None));
        }
    }
}