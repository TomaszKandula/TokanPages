using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;
using TokanPages.Backend.Core.Exceptions;

namespace Backend.UnitTests.Handlers.Subscribers
{

    public class RemoveSubscriberCommandHandlerTest : TestBase
    {

        [Fact]
        public async Task RemoveSubscriber_WhenIdIsCorrect_ShouldRemoveEntity() 
        {

            // Arrange
            var LRemoveSubscriberCommand = new RemoveSubscriberCommand 
            { 
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = "ester.exposito@gmail.com",
                IsActivated = true,
                Count = 50,
                Registered = DateTime.Now,
                LastUpdated = null
            };
            LDatabaseContext.Subscribers.Add(LSubscribers);
            LDatabaseContext.SaveChanges();

            // Act
            var LRemoveSubscriberCommandHandler = new RemoveSubscriberCommandHandler(LDatabaseContext);
            await LRemoveSubscriberCommandHandler.Handle(LRemoveSubscriberCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LSubscribersEntity = LAssertDbContext.Subscribers.Find(LRemoveSubscriberCommand.Id);
            LSubscribersEntity.Should().BeNull();

        }

        [Fact]
        public async Task RemoveSubscriber_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LRemoveSubscriberCommand = new RemoveSubscriberCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LRemoveSubscriberCommandHandler = new RemoveSubscriberCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LRemoveSubscriberCommandHandler.Handle(LRemoveSubscriberCommand, CancellationToken.None));

        }

    }

}
