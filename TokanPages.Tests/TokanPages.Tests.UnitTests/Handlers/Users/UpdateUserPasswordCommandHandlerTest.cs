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
using TokanPages.Services.CipheringService;
using Backend.Core.Utilities.DateTimeService;
using Backend.Cqrs.Services.UserServiceProvider;

public class UpdateUserPasswordCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidUserDataAndNewPasswordAsLoggedUser_WhenUpdateUserPassword_ShouldFinishSuccessful()
    {
        // Arrange
        var users = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            Registered = DateTimeService.Now,
            LastUpdated = null,
            LastLogged = null,
            CryptedPassword = string.Empty,
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = users.Id,
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedCipheringService = new Mock<ICipheringService>();

        mockedUserProvider
            .Setup(service => service.HasRoleAssigned(It.IsAny<string>()))
            .ReturnsAsync(true);
            
        var mockedPassword = DataUtilityService.GetRandomString();
        mockedCipheringService
            .Setup(service => service.GetHashedPassword(
                It.IsAny<string>(), 
                It.IsAny<string>()))
            .Returns(mockedPassword);

        mockedCipheringService
            .Setup(service => service.GenerateSalt(It.IsAny<int>()))
            .Returns(string.Empty);
            
        var updateUserCommandHandler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        await updateUserCommandHandler.Handle(updateUserPasswordCommand, CancellationToken.None);

        // Assert
        var userEntity = await databaseContext.Users.FindAsync(users.Id);
            
        userEntity.Should().NotBeNull();
        userEntity.CryptedPassword.Should().NotBeEmpty();
        userEntity.ResetId.Should().BeNull();
    }

    [Fact]
    public async Task GivenInvalidResetId_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var users = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            Registered = DateTimeService.Now,
            LastUpdated = null,
            LastLogged = null,
            CryptedPassword = string.Empty,
            ResetId = Guid.NewGuid(),
            ResetIdEnds = DateTime.Now.AddMinutes(30),
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = users.Id,
            ResetId = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedCipheringService = new Mock<ICipheringService>();

        var mockedPassword = DataUtilityService.GetRandomString();
        mockedCipheringService
            .Setup(service => service.GetHashedPassword(
                It.IsAny<string>(), 
                It.IsAny<string>()))
            .Returns(mockedPassword);

        mockedCipheringService
            .Setup(service => service.GenerateSalt(It.IsAny<int>()))
            .Returns(string.Empty);
            
        var updateUserCommandHandler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => updateUserCommandHandler.Handle(updateUserPasswordCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_RESET_ID));
    }

    [Fact]
    public async Task GivenInvalidUserId_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var users = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            Registered = DateTimeService.Now,
            LastUpdated = null,
            LastLogged = null,
            CryptedPassword = string.Empty,
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedCipheringService = new Mock<ICipheringService>();

        var mockedPassword = DataUtilityService.GetRandomString();
        mockedCipheringService
            .Setup(service => service.GetHashedPassword(
                It.IsAny<string>(), 
                It.IsAny<string>()))
            .Returns(mockedPassword);

        mockedCipheringService
            .Setup(service => service.GenerateSalt(It.IsAny<int>()))
            .Returns(string.Empty);
            
        var updateUserPasswordCommandHandler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() 
            => updateUserPasswordCommandHandler.Handle(updateUserPasswordCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenNoResetIdAsNotLoggedUser_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var users = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
            Registered = DateTimeService.Now,
            LastUpdated = null,
            LastLogged = null,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var updateUserPasswordCommand = new UpdateUserPasswordCommand
        {
            Id = users.Id,
            ResetId = null,
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserProvider = new Mock<IUserServiceProvider>();
        var mockedCipheringService = new Mock<ICipheringService>();

        mockedUserProvider
            .Setup(provider => provider.HasRoleAssigned(It.IsAny<string>()))
            .ReturnsAsync(false);
            
        var mockedPassword = DataUtilityService.GetRandomString();
        mockedCipheringService
            .Setup(service => service.GetHashedPassword(
                It.IsAny<string>(), 
                It.IsAny<string>()))
            .Returns(mockedPassword);

        mockedCipheringService
            .Setup(service => service.GenerateSalt(It.IsAny<int>()))
            .Returns(string.Empty);
            
        var updateUserPasswordCommandHandler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserProvider.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() 
            => updateUserPasswordCommandHandler.Handle(updateUserPasswordCommand, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }
}