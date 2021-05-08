using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Tests.DataProviders;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace TokanPages.Tests.UnitTests.Handlers.Subscribers
{
    public class RemoveSubscriberCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenRemoveSubscriber_ShouldRemoveEntity() 
        {
            // Arrange
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
            {
                Email = StringProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 50,
                Registered = DateTime.Now,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LRemoveSubscriberCommand = new RemoveSubscriberCommand { Id = LSubscribers.Id };
            var LRemoveSubscriberCommandHandler = new RemoveSubscriberCommandHandler(LDatabaseContext);

            // Act
            await LRemoveSubscriberCommandHandler.Handle(LRemoveSubscriberCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LSubscribersEntity = await LAssertDbContext.Subscribers.FindAsync(LRemoveSubscriberCommand.Id);
            LSubscribersEntity.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenRemoveSubscriber_ShouldThrowError()
        {
            // Arrange
            var LRemoveSubscriberCommand = new RemoveSubscriberCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LRemoveSubscriberCommandHandler = new RemoveSubscriberCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LRemoveSubscriberCommandHandler.Handle(LRemoveSubscriberCommand, CancellationToken.None));
        }
    }
}
