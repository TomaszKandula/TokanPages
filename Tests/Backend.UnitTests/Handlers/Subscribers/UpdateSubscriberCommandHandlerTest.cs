using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataProviders;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace Backend.UnitTests.Handlers.Subscribers
{
    public class UpdateSubscriberCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenUpdateSubscriber_ShouldUpdateEntity()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = StringProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 10
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = StringProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 50,
                Registered = DateTime.Now,
                LastUpdated = null
            };
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(LDatabaseContext, LMockedDateTime.Object);
            await LUpdateSubscriberCommandHandler.Handle(LUpdateSubscriberCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LSubscribersEntity = await LAssertDbContext.Subscribers.FindAsync(LUpdateSubscriberCommand.Id);

            LSubscribersEntity.Should().NotBeNull();
            LSubscribersEntity.IsActivated.Should().BeTrue();
            LSubscribersEntity.Email.Should().Be(LUpdateSubscriberCommand.Email);
            LSubscribersEntity.Count.Should().Be(LUpdateSubscriberCommand.Count);
            LSubscribersEntity.LastUpdated.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenCorrectIdAndCountIsNullAndIsActivatedIsNull_WhenUpdateSubscriber_ShouldUpdateEntity()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = StringProvider.GetRandomEmail(),
                IsActivated = null,
                Count = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = StringProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 50,
                Registered = DateTime.Now,
                LastUpdated = null
            };
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            // Act
            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(LDatabaseContext, LMockedDateTime.Object);
            await LUpdateSubscriberCommandHandler.Handle(LUpdateSubscriberCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LSubscribersEntity = await LAssertDbContext.Subscribers.FindAsync(LUpdateSubscriberCommand.Id);

            LSubscribersEntity.Should().NotBeNull();
            LSubscribersEntity.IsActivated.Should().BeTrue();
            LSubscribersEntity.Email.Should().Be(LUpdateSubscriberCommand.Email);
            LSubscribersEntity.Count.Should().Be(LSubscribers.Count);
            LSubscribersEntity.LastUpdated.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var LUpdateSubscriberCommand = new UpdateSubscriberCommand
            {
                Id = Guid.Parse("32fcefec-4c26-48bb-8717-31447cfda471"),
                Email = StringProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 10
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Email = StringProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 50,
                Registered = DateTime.Now,
                LastUpdated = null
            };
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateSubscriberCommandHandler.Handle(LUpdateSubscriberCommand, CancellationToken.None));
        }

        [Fact]
        public async Task GivenExistingEmail_WhenUpdateSubscriber_ShouldThrowError()
        {
            // Arrange
            var LTestEmail = StringProvider.GetRandomEmail();
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
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTime = new Mock<DateTimeService>();
            var LUpdateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateSubscriberCommandHandler.Handle(LUpdateSubscriberCommand, CancellationToken.None));
        }
    }
}
