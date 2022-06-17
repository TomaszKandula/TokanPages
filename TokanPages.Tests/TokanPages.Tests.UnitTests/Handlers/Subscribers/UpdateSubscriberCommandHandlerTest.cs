namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

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

        var command = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedDateTime.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var subscribersEntity = await databaseContext.Subscribers.FindAsync(command.Id);

        subscribersEntity.Should().NotBeNull();
        subscribersEntity.IsActivated.Should().BeTrue();
        subscribersEntity.Email.Should().Be(command.Email);
        subscribersEntity.Count.Should().Be(command.Count);
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

        var command = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = null,
            Count = null
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedDateTime.Object);
            
        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var subscribersEntity = await databaseContext.Subscribers.FindAsync(command.Id);

        subscribersEntity.Should().NotBeNull();
        subscribersEntity.IsActivated.Should().BeTrue();
        subscribersEntity.Email.Should().Be(command.Email);
        subscribersEntity.Count.Should().Be(subscribers.Count);
        subscribersEntity.LastUpdated.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenUpdateSubscriber_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateSubscriberCommand
        {
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var databaseContext = GetTestDatabaseContext();
        var subscribers = new Subscribers
        {
            Id = Guid.NewGuid(),
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
        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
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

        var command = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = testEmail,
            IsActivated = true,
            Count = 10
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
    }
}