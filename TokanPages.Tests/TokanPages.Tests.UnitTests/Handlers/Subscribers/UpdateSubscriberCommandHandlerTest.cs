using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

public class UpdateSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenUpdateSubscriber_ShouldUpdateEntity()
    {
        // Arrange
        var subscribers = new Backend.Domain.Entities.Newsletters 
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty,
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Newsletters.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var dateTimeService = new DateTimeService();
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetUser(It.IsAny<CancellationToken>()))
            .ReturnsAsync((GetUserOutput)null!);
        
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
        var entity = await databaseContext.Newsletters.FindAsync(command.Id);

        entity.Should().NotBeNull();
        entity?.IsActivated.Should().BeTrue();
        entity?.Email.Should().Be(command.Email);
        entity?.Count.Should().Be(command.Count);
        entity?.ModifiedAt.Should().NotBeNull();
        entity?.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
    }

    [Fact]
    public async Task GivenCorrectIdAndCountIsNullAndIsActivatedIsNull_WhenUpdateSubscriber_ShouldUpdateEntity()
    {
        // Arrange
        var subscribers = new Backend.Domain.Entities.Newsletters
        {
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty,
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Newsletters.AddAsync(subscribers);
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
        var entity = await databaseContext.Newsletters.FindAsync(command.Id);

        entity.Should().NotBeNull();
        entity?.IsActivated.Should().BeTrue();
        entity?.Email.Should().Be(command.Email);
        entity?.Count.Should().Be(subscribers.Count);
        entity?.ModifiedAt.Should().NotBeNull();
        entity?.ModifiedAt.Should().BeBefore(DateTime.UtcNow);
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

        var subscribers = new Backend.Domain.Entities.Newsletters
        {
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 50,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty,
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Newsletters.AddAsync(subscribers);
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
        var subscribers = new Backend.Domain.Entities.Newsletters
        {
            Email = testEmail,
            IsActivated = true,
            Count = 50,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty,
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Newsletters.AddAsync(subscribers);
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