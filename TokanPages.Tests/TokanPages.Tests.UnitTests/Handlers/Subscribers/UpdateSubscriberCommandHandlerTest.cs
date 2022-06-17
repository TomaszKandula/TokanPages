namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using TokanPages.Backend.Dto.Users;
using TokanPages.Services.UserService;
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

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetUser(It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserDto)null!);
        
        var command = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            dateTimeService, 
            mockedUserService.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var entity = await databaseContext.Subscribers.FindAsync(command.Id);

        entity.Should().NotBeNull();
        entity.IsActivated.Should().BeTrue();
        entity.Email.Should().Be(command.Email);
        entity.Count.Should().Be(command.Count);
        entity.LastUpdated.Should().NotBeNull();//TODO: to be removed
        entity.ModifiedAt.Should().NotBeNull();
        entity.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
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

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var command = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = null,
            Count = null
        };

        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            dateTimeService, 
            mockedUserService.Object);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var entity = await databaseContext.Subscribers.FindAsync(command.Id);

        entity.Should().NotBeNull();
        entity.IsActivated.Should().BeTrue();
        entity.Email.Should().Be(command.Email);
        entity.Count.Should().Be(subscribers.Count);
        entity.LastUpdated.Should().NotBeNull();//TODO: to be removed
        entity.ModifiedAt.Should().NotBeNull();
        entity.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
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

        var subscribers = new Subscribers
        {
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            Registered = DateTime.Now,
            LastUpdated = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Subscribers.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            dateTimeService, 
            mockedUserService.Object);

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

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var command = new UpdateSubscriberCommand
        {
            Id = subscribers.Id,
            Email = testEmail,
            IsActivated = true,
            Count = 10
        };

        var handler = new UpdateSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            dateTimeService, 
            mockedUserService.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
    }
}