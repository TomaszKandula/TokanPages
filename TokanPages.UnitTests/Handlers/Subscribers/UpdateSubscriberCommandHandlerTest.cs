namespace TokanPages.UnitTests.Handlers.Subscribers;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Core.Utilities.LoggerService;
using Backend.Core.Utilities.DateTimeService;
using Backend.Cqrs.Handlers.Commands.Subscribers;

public class UpdateSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenUpdateSubscriber_ShouldUpdateEntity()
    {
        // Arrange
        var subscribers = new Subscribers 
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            Registered = DateTime.Now,
            LastUpdated = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var updateSubscriberCommand = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var updateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedDateTime.Object);

        // Act
        await updateSubscriberCommandHandler.Handle(updateSubscriberCommand, CancellationToken.None);

        // Assert
        var subscribersEntity = await databaseContext.Subscribers.FindAsync(updateSubscriberCommand.Id);

        subscribersEntity.Should().NotBeNull();
        subscribersEntity.IsActivated.Should().BeTrue();
        subscribersEntity.Email.Should().Be(updateSubscriberCommand.Email);
        subscribersEntity.Count.Should().Be(updateSubscriberCommand.Count);
        subscribersEntity.LastUpdated.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenCorrectIdAndCountIsNullAndIsActivatedIsNull_WhenUpdateSubscriber_ShouldUpdateEntity()
    {
        // Arrange
        var subscribers = new Subscribers
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            Registered = DateTime.Now,
            LastUpdated = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var updateSubscriberCommand = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = null,
            Count = null
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var updateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedDateTime.Object);
            
        // Act
        await updateSubscriberCommandHandler.Handle(updateSubscriberCommand, CancellationToken.None);

        // Assert
        var subscribersEntity = await databaseContext.Subscribers.FindAsync(updateSubscriberCommand.Id);

        subscribersEntity.Should().NotBeNull();
        subscribersEntity.IsActivated.Should().BeTrue();
        subscribersEntity.Email.Should().Be(updateSubscriberCommand.Email);
        subscribersEntity.Count.Should().Be(subscribers.Count);
        subscribersEntity.LastUpdated.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenUpdateSubscriber_ShouldThrowError()
    {
        // Arrange
        var updateSubscriberCommand = new UpdateSubscriberCommand
        {
            Id = Guid.Parse("32fcefec-4c26-48bb-8717-31447cfda471"),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var databaseContext = GetTestDatabaseContext();
        var subscribers = new Subscribers
        {
            Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            Registered = DateTime.Now,
            LastUpdated = null
        };
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var updateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() 
            => updateSubscriberCommandHandler.Handle(updateSubscriberCommand, CancellationToken.None));
    }

    [Fact]
    public async Task GivenExistingEmail_WhenUpdateSubscriber_ShouldThrowError()
    {
        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var subscribers = new Subscribers
        {
            Email = testEmail,
            IsActivated = true,
            Count = 50,
            Registered = DateTime.Now,
            LastUpdated = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var updateSubscriberCommand = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = testEmail,
            IsActivated = true,
            Count = 10
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var updateSubscriberCommandHandler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() 
            => updateSubscriberCommandHandler.Handle(updateSubscriberCommand, CancellationToken.None));
    }
}