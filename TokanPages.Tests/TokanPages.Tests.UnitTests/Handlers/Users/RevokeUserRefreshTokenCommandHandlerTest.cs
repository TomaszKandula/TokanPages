namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using TokanPages.Services.UserService;
using Backend.Application.Handlers.Commands.Users;
using Backend.Core.Utilities.LoggerService;
using TokanPages.Services.UserService.Models;
using MediatR;

public class RevokeUserRefreshTokenCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidRefreshToken_WhenRevokeUserRefreshToken_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var token = DataUtilityService.GetRandomString(100);
        var user = new Users
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var userRefreshToken = new UserRefreshTokens
        {
            UserId = userId,
            Token = token,
            Expires = DateTimeService.Now,
            Created = DateTimeService.Now.AddMinutes(300),
            CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
        await databaseContext.SaveChangesAsync();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        var randomIpAddress = DataUtilityService.GetRandomIpAddress().ToString(); 
        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(randomIpAddress);

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service
                .RevokeRefreshToken(
                    It.IsAny<RevokeRefreshTokenInput>(),
                    It.IsAny<CancellationToken>()));
            
        var command = new RevokeUserRefreshTokenCommand { RefreshToken = token };
        var handler = new RevokeUserRefreshTokenCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenInvalidRefreshToken_WhenRevokeUserRefreshToken_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var token = DataUtilityService.GetRandomString(100);
        var user = new Users
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service
                .RevokeRefreshToken(
                    It.IsAny<RevokeRefreshTokenInput>(),
                    It.IsAny<CancellationToken>()));

        var command = new RevokeUserRefreshTokenCommand { RefreshToken = token };
        var handler = new RevokeUserRefreshTokenCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AuthorizationException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
    }
}