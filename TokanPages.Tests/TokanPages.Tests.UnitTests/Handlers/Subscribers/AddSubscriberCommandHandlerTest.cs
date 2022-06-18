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
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var dateTimeService = new DateTimeService();

        var command = new AddSubscriberCommand { Email = DataUtilityService.GetRandomEmail() };
        var handler = new AddSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            dateTimeService);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var entity = databaseContext.Subscribers.ToList();

        entity.Should().HaveCount(1);
        entity[0].Email.Should().Be(command.Email);
        entity[0].Count.Should().Be(0);
        entity[0].IsActivated.Should().BeTrue();
        entity[0].CreatedAt.Should().BeBefore(DateTime.UtcNow);
        entity[0].CreatedBy.Should().Be(Guid.Empty);
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