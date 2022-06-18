namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Exceptions;
using Backend.Domain.Entities;
using Backend.Shared.Resources;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Users;
using Backend.Core.Utilities.DateTimeService;
using MediatR;

public class ActivateUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidActivationId_WhenActivateUser_ShouldFinishSuccessful()
    {
        // Arrange
        var activationId = Guid.NewGuid();
        var users = new Users
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString().ToLower(),
            CryptedPassword = DataUtilityService.GetRandomString(),
            ActivationId = activationId,
            ActivationIdEnds = DateTimeService.Now.AddMinutes(30)
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedDateTimeService
            .SetupGet(service => service.Now)
            .Returns(DateTimeService.Now);
            
        var command = new ActivateUserCommand { ActivationId = activationId };
        var handler = new ActivateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTimeService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenInvalidActivationId_WhenActivateUser_ShouldThrowError()
    {
        // Arrange
        var users = new Users
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString().ToLower(),
            CryptedPassword = DataUtilityService.GetRandomString(),
            ActivationId = Guid.NewGuid(),
            ActivationIdEnds = DateTimeService.Now.AddMinutes(30)
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedDateTimeService
            .SetupGet(service => service.Now)
            .Returns(DateTimeService.Now);
            
        var command = new ActivateUserCommand { ActivationId = Guid.NewGuid() };
        var handler = new ActivateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_ACTIVATION_ID));
    }
        
    [Fact]
    public async Task GivenExpiredActivationId_WhenActivateUser_ShouldThrowError()
    {
        // Arrange
        var activationId = Guid.NewGuid();
        var users = new Users
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString().ToLower(),
            CryptedPassword = DataUtilityService.GetRandomString(),
            ActivationId = activationId,
            ActivationIdEnds = DateTimeService.Now.AddMinutes(-100)
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedDateTimeService
            .SetupGet(service => service.Now)
            .Returns(DateTimeService.Now);
            
        var command = new ActivateUserCommand { ActivationId = activationId };
        var handler = new ActivateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID));
    }
}