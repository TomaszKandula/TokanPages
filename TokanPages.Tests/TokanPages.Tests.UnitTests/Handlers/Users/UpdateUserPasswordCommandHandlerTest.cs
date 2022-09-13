using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.CipheringService;
using TokanPages.Services.UserService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class UpdateUserPasswordCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidUserDataAndNewPasswordAsLoggedUser_WhenUpdateUserPassword_ShouldFinishSuccessful()
    {
        // Arrange
        var users = new Backend.Domain.Entities.Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = string.Empty,
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var command = new UpdateUserPasswordCommand
        {
            Id = users.Id,
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedCipheringService = new Mock<ICipheringService>();

        mockedUserService
            .Setup(service => service.HasRoleAssigned(
                It.IsAny<string>(),
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
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

        var handler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        await handler.Handle(command, CancellationToken.None);

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
        var users = new Backend.Domain.Entities.Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = string.Empty,
            ResetId = Guid.NewGuid(),
            ResetIdEnds = DateTime.Now.AddMinutes(30),
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var command = new UpdateUserPasswordCommand
        {
            Id = users.Id,
            ResetId = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
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

        var handler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AuthorizationException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_RESET_ID));
    }

    [Fact]
    public async Task GivenInvalidUserId_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var users = new Backend.Domain.Entities.Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = string.Empty,
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var command = new UpdateUserPasswordCommand
        {
            Id = Guid.NewGuid(),
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
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

        var handler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AuthorizationException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenNoResetIdAsNotLoggedUser_WhenUpdateUserPassword_ShouldThrowError()
    {
        // Arrange
        var users = new Backend.Domain.Entities.Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            ResetIdEnds = null,
            ActivationId = null,
            ActivationIdEnds = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var command = new UpdateUserPasswordCommand
        {
            Id = users.Id,
            ResetId = null,
            NewPassword = DataUtilityService.GetRandomString()
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedCipheringService = new Mock<ICipheringService>();

        mockedUserService
            .Setup(provider => provider.HasRoleAssigned(
                It.IsAny<string>(), 
                It.IsAny<Guid?>(),
                It.IsAny<CancellationToken>()))
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

        var handler = new UpdateUserPasswordCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedUserService.Object,
            mockedCipheringService.Object,
            mockedDateTimeService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }
}