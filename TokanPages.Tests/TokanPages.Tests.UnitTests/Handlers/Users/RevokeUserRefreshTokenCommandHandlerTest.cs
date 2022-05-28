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
using TokanPages.Backend.Dto.Users;
using TokanPages.Services.UserService;
using Backend.Cqrs.Handlers.Commands.Users;
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
            .Setup(provider => provider.GetRequestIpAddress())
            .Returns(randomIpAddress);

        var userDto = new GetUserDto
        {
            UserId = userId, 
            AliasName = DataUtilityService.GetRandomString(),
            AvatarName = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Email = DataUtilityService.GetRandomEmail(),
            ShortBio = DataUtilityService.GetRandomString(),
            Registered= DataUtilityService.GetRandomDateTime(),
        };

        mockedUserService
            .Setup(service => service.GetUser(It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDto);

        mockedUserService
            .Setup(provider => provider
                .RevokeRefreshToken(
                    It.IsAny<RevokeRefreshTokenInput>(),
                    It.IsAny<CancellationToken>()));
            
        var revokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand { RefreshToken = token };
        var revokeUserRefreshTokenCommandHandler = new RevokeUserRefreshTokenCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object
        );

        // Act
        var result = await revokeUserRefreshTokenCommandHandler.Handle(revokeUserRefreshTokenCommand, CancellationToken.None);

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

        var randomIpAddress = DataUtilityService.GetRandomIpAddress().ToString(); 
        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(randomIpAddress);

        var userDto = new GetUserDto
        {
            UserId = userId, 
            AliasName = DataUtilityService.GetRandomString(),
            AvatarName = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            Email = DataUtilityService.GetRandomEmail(),
            ShortBio = DataUtilityService.GetRandomString(),
            Registered= DataUtilityService.GetRandomDateTime(),
        };

        mockedUserService
            .Setup(service => service.GetUser(It.IsAny<CancellationToken>()))
            .ReturnsAsync(userDto);

        mockedUserService
            .Setup(service => service
                .RevokeRefreshToken(
                    It.IsAny<RevokeRefreshTokenInput>(),
                    It.IsAny<CancellationToken>()));

        var revokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand { RefreshToken = token };
        var revokeUserRefreshTokenCommandHandler = new RevokeUserRefreshTokenCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object
        );

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AuthorizationException>(() 
            => revokeUserRefreshTokenCommandHandler.Handle(revokeUserRefreshTokenCommand, CancellationToken.None));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
    }
}