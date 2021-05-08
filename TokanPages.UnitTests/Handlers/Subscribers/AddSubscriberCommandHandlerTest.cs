using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.DataProviders;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Cqrs.Handlers.Commands.Subscribers;

namespace TokanPages.UnitTests.Handlers.Subscribers
{
    public class AddSubscriberCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenProvidedEmail_WhenAddSubscriber_ShouldAddEntity() 
        {
            // Arrange
            var LAddSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = StringProvider.GetRandomEmail()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTime = new Mock<DateTimeService>();

            const string TEST_DATE_TIME = "2020-01-01";
            LMockedDateTime
                .Setup(ADateTime => ADateTime.Now)
                .Returns(DateTime.Parse(TEST_DATE_TIME));
            
            var LAddSubscriberCommandHandler = new AddSubscriberCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            // Act
            await LAddSubscriberCommandHandler.Handle(LAddSubscriberCommand, CancellationToken.None);

            // Assert
            var LSubscribersEntity = LDatabaseContext.Subscribers.ToList();

            LSubscribersEntity.Should().HaveCount(1);
            LSubscribersEntity[0].Email.Should().Be(LAddSubscriberCommand.Email);
            LSubscribersEntity[0].Count.Should().Be(0);
            LSubscribersEntity[0].IsActivated.Should().BeTrue();
            LSubscribersEntity[0].Registered.Should().HaveDay(DateTime.Parse(TEST_DATE_TIME).Day);
            LSubscribersEntity[0].Registered.Should().HaveMonth(DateTime.Parse(TEST_DATE_TIME).Month);
            LSubscribersEntity[0].Registered.Should().HaveYear(DateTime.Parse(TEST_DATE_TIME).Year);
            LSubscribersEntity[0].LastUpdated.Should().BeNull();
        }

        [Fact]
        public async Task GivenExistingEmail_WhenAddSubscriber_ShouldThrowError()
        {
            // Arrange
            var LTestEmail = StringProvider.GetRandomEmail();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
            { 
                Email = LTestEmail,
                IsActivated = true,
                Count = 0,
                Registered = DateTime.Now,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedDateTime = new Mock<DateTimeService>();

            var LAddSubscriberCommand = new AddSubscriberCommand { Email = LTestEmail };
            var LAddSubscriberCommandHandler = new AddSubscriberCommandHandler(LDatabaseContext, LMockedDateTime.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LAddSubscriberCommandHandler.Handle(LAddSubscriberCommand, CancellationToken.None));
        }
    }
}
