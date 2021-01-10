using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace Backend.UnitTests.Handlers.Subscribers
{

    public class AddSubscriberCommandHandlerTest : TestBase
    {

        [Fact]
        public async Task AddSubscriber_WhenEmailIsProvided_ShouldAddEntity() 
        {

            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = DataProvider.GetRandomEmail()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LAddSubscriberCommandHandler = new AddSubscriberCommandHandler(LDatabaseContext);

            // Act
            await LAddSubscriberCommandHandler.Handle(LAddSubscriberCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LSubscribersEntity = LAssertDbContext.Subscribers.ToList();

            LSubscribersEntity.Should().HaveCount(1);
            LSubscribersEntity[0].Email.Should().Be(LAddSubscriberCommand.Email);
            LSubscribersEntity[0].Count.Should().Be(0);
            LSubscribersEntity[0].IsActivated.Should().BeTrue();
            LSubscribersEntity[0].Registered.Should().HaveDay(DateTime.UtcNow.Day);
            LSubscribersEntity[0].Registered.Should().HaveMonth(DateTime.UtcNow.Month);
            LSubscribersEntity[0].Registered.Should().HaveYear(DateTime.UtcNow.Year);
            LSubscribersEntity[0].LastUpdated.Should().BeNull();

        }

        [Fact]
        public async Task AddSubscriber_WhenEmailExists_ShouldThrowError()
        {

            // Arrange
            var LTestEmail = DataProvider.GetRandomEmail();
            var LAddSubscriberCommand = new AddSubscriberCommand
            {
                Email = LTestEmail
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
            { 
                Id = Guid.Parse("ec2ebd48-3bf4-45a9-9030-f8ad52c5a8f8"),
                Email = LTestEmail,
                IsActivated = true,
                Count = 0,
                Registered = DateTime.Now,
                LastUpdated = null
            };
            LDatabaseContext.Subscribers.Add(LSubscribers);
            LDatabaseContext.SaveChanges();

            var LAddSubscriberCommandHandler = new AddSubscriberCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LAddSubscriberCommandHandler.Handle(LAddSubscriberCommand, CancellationToken.None));

        }

    }

}
