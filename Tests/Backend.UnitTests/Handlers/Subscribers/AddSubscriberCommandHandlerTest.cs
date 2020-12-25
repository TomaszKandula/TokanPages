using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
                Email = "tokan@dfds.com"    
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
            LSubscribersEntity[0].Registered.Should().HaveDay(DateTime.Now.Day);
            LSubscribersEntity[0].Registered.Should().HaveMonth(DateTime.Now.Month);
            LSubscribersEntity[0].Registered.Should().HaveYear(DateTime.Now.Year);
            LSubscribersEntity[0].LastUpdated.Should().BeNull();

        }

    }

}
