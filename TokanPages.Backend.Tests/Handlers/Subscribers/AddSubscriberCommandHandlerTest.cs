namespace TokanPages.Backend.Tests.Handlers.Subscribers
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Utilities.LoggerService;
    using Core.Exceptions;
    using Core.Utilities.DateTimeService;
    using Cqrs.Handlers.Commands.Subscribers;

    public class AddSubscriberCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenProvidedEmail_WhenAddSubscriber_ShouldAddEntity() 
        {
            // Arrange
            var addSubscriberCommand = new AddSubscriberCommand 
            { 
                Email = DataUtilityService.GetRandomEmail()
            };

            var databaseContext = GetTestDatabaseContext();
            var mockedLogger = new Mock<ILoggerService>();
            var mockedDateTime = new Mock<IDateTimeService>();

            const string testDateTime = "2020-01-01";
            mockedDateTime
                .Setup(dateTime => dateTime.Now)
                .Returns(DateTime.Parse(testDateTime));
            
            var addSubscriberCommandHandler = new AddSubscriberCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedDateTime.Object);

            // Act
            await addSubscriberCommandHandler.Handle(addSubscriberCommand, CancellationToken.None);

            // Assert
            var subscribersEntity = databaseContext.Subscribers.ToList();

            subscribersEntity.Should().HaveCount(1);
            subscribersEntity[0].Email.Should().Be(addSubscriberCommand.Email);
            subscribersEntity[0].Count.Should().Be(0);
            subscribersEntity[0].IsActivated.Should().BeTrue();
            subscribersEntity[0].Registered.Should().HaveDay(DateTime.Parse(testDateTime).Day);
            subscribersEntity[0].Registered.Should().HaveMonth(DateTime.Parse(testDateTime).Month);
            subscribersEntity[0].Registered.Should().HaveYear(DateTime.Parse(testDateTime).Year);
            subscribersEntity[0].LastUpdated.Should().BeNull();
        }

        [Fact]
        public async Task GivenExistingEmail_WhenAddSubscriber_ShouldThrowError()
        {
            // Arrange
            var testEmail = DataUtilityService.GetRandomEmail();
            var subscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
            { 
                Email = testEmail,
                IsActivated = true,
                Count = 0,
                Registered = DateTime.Now,
                LastUpdated = null
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Subscribers.AddAsync(subscribers);
            await databaseContext.SaveChangesAsync();

            var mockedLogger = new Mock<ILoggerService>();
            var mockedDateTime = new Mock<IDateTimeService>();

            var addSubscriberCommand = new AddSubscriberCommand { Email = testEmail };
            var addSubscriberCommandHandler = new AddSubscriberCommandHandler(
                databaseContext, 
                mockedLogger.Object,
                mockedDateTime.Object);

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => addSubscriberCommandHandler.Handle(addSubscriberCommand, CancellationToken.None));
        }
    }
}