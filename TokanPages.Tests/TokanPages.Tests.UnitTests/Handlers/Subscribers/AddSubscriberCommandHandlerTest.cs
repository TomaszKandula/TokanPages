namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Exceptions;
using Backend.Core.Utilities.LoggerService;
using Backend.Core.Utilities.DateTimeService;
using Backend.Cqrs.Handlers.Commands.Subscribers;

public class AddSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenProvidedEmail_WhenAddSubscriber_ShouldAddEntity() 
    {
        // Arrange
        var command = new AddSubscriberCommand 
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
            
        var handler = new AddSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var subscribersEntity = databaseContext.Subscribers.ToList();

        subscribersEntity.Should().HaveCount(1);
        subscribersEntity[0].Email.Should().Be(command.Email);
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

        var command = new AddSubscriberCommand { Email = testEmail };
        var handler = new AddSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
    }
}