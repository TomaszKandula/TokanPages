using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace Backend.UnitTests.Handlers.Subscribers
{
    
    public class UpdateSubscriberCommandHandlerTest : TestBase
    {

        [Fact]
        public async Task UpdateSubscriber_WhenIdIsCorrect_ShouldUpdateEntity()
        {

            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = "ester1990@gmail.com",
                IsActivated = true,
                Count = 10
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
            var LUpdateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(LDatabaseContext);
            await LUpdateSubscriberCommandHandler.Handle(LUpdateSubscriberCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LSubscribersEntity = LAssertDbContext.Subscribers.Find(LUpdateSubscriberCommand.Id);

            LSubscribersEntity.Should().NotBeNull();
            LSubscribersEntity.Email.Should().Be(LUpdateSubscriberCommand.Email);
            LSubscribersEntity.Count.Should().Be(LUpdateSubscriberCommand.Count);
            LSubscribersEntity.LastUpdated.Should().NotBeNull();

        }

        [Fact]
        public async Task UpdateSubscriber_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Parse("32fcefec-4c26-48bb-8717-31447cfda471"),
                Email = "ester.exposito@gmail.com",
                IsActivated = true,
                Count = 10
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

            var LUpdateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LUpdateSubscriberCommandHandler.Handle(LUpdateSubscriberCommand, CancellationToken.None));

        }

        [Fact]
        public async Task UpdateSubscriber_WhenEmailExists_ShouldThrowError()
        {

            // Arrange
            var LTestEmail = "tokan@dfds.com";
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = LTestEmail,
                IsActivated = true,
                Count = 10
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = LTestEmail,
                IsActivated = true,
                Count = 50,
                Registered = DateTime.Now,
                LastUpdated = null
            };
            LDatabaseContext.Subscribers.Add(LSubscribers);
            LDatabaseContext.SaveChanges();

            var LUpdateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LUpdateSubscriberCommandHandler.Handle(LUpdateSubscriberCommand, CancellationToken.None));

        }

    }

}
