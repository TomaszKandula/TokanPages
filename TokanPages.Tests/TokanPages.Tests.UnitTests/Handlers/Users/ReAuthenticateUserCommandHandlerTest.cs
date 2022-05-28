namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Backend.Shared.Models;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Shared.Resources;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Users;
using TokanPages.Services.UserService.Models;
using Backend.Core.Utilities.DateTimeService;

public class ReAuthenticateUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidRefreshToken_WhenReAuthenticateUser_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var emailAddress = DataUtilityService.GetRandomEmail();
        var cryptedPassword = DataUtilityService.GetRandomString(60);
        var ipAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var expires = DateTimeService.Now.AddMinutes(300);
        var created = DateTimeService.Now.AddDays(-5);
        var refreshToken = DataUtilityService.GetRandomString(255);

        var reAuthenticateUserCommand = new ReAuthenticateUserCommand
        {
            RefreshToken = refreshToken
        };
        
        var user = new Users
        {
            Id = userId,
            EmailAddress = emailAddress,
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            LastLogged = null,
            CryptedPassword = cryptedPassword
        };

        var userInfo = new UserInfo
        {
            UserId = user.Id,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = null,
            UserVideoName = null,
            CreatedBy = Guid.Empty,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            ModifiedBy = null,
            ModifiedAt = null
        };

        var userRefreshToken = new UserRefreshTokens
        {
            UserId = user.Id,
            Token = refreshToken,
            Expires = expires,
            Created = created,
            CreatedByIp = ipAddress,
            Revoked = null,
            RevokedByIp = null,
            ReplacedByToken = null,
            ReasonRevoked = null
        };
            
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserInfo.AddAsync(userInfo);
        await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
        await databaseContext.SaveChangesAsync();
            
        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserServiceProvider = new Mock<IUserService>();

        mockedUserServiceProvider
            .Setup(service => service.GetUserId(It.IsAny<CancellationToken>()))
            .ReturnsAsync(userId);

        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserServiceProvider
            .Setup(service => service.IsRefreshTokenRevoked(It.IsAny<UserRefreshTokens>()))
            .Returns(false);

        mockedUserServiceProvider
            .Setup(service => service.IsRefreshTokenActive(It.IsAny<UserRefreshTokens>()))
            .Returns(true);
            
        var newRefreshToken = new UserRefreshTokens
        {
            UserId = user.Id,
            Token = DataUtilityService.GetRandomString(255),
            Expires = DateTimeService.Now.AddMinutes(120),
            Created = DateTimeService.Now,
            CreatedByIp = ipAddress,
            Revoked = null,
            RevokedByIp = null,
            ReplacedByToken = null,
            ReasonRevoked = null
        };

        mockedUserServiceProvider
            .Setup(service => service
                .ReplaceRefreshToken(
                    It.IsAny<ReplaceRefreshTokenInput>(), 
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(newRefreshToken);

        mockedUserServiceProvider
            .Setup(service => service
                .DeleteOutdatedRefreshTokens(
                    It.IsAny<Guid>(), 
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()));

        var newUserToken = DataUtilityService.GetRandomString();
        mockedUserServiceProvider
            .Setup(service => service.GenerateUserToken(
                It.IsAny<Users>(), 
                It.IsAny<DateTime>(), 
                CancellationToken.None))
            .ReturnsAsync(newUserToken);

        var identityServer = new IdentityServer
        {
            Issuer = DataUtilityService.GetRandomString(),
            Audience = DataUtilityService.GetRandomString(),
            WebSecret = DataUtilityService.GetRandomString(),
            RequireHttps = false,
            WebTokenExpiresIn = 90,
            RefreshTokenExpiresIn = 120
        };
            
        var mockedApplicationSettings = MockApplicationSettings(identityServer: identityServer);

        // Act
        var reAuthenticateUserCommandHandler = new ReAuthenticateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTimeService.Object, 
            mockedUserServiceProvider.Object, 
            mockedApplicationSettings.Object);

        var result = await reAuthenticateUserCommandHandler.Handle(reAuthenticateUserCommand, CancellationToken.None);
            
        // Assert
        result.UserId.Should().Be(user.Id);
        result.AliasName.Should().Be(user.UserAlias);
        result.UserToken.Should().Be(newUserToken);
            
        var userTokens = await databaseContext.UserTokens
            .Where(tokens => tokens.UserId == user.Id)
            .ToListAsync();

        userTokens.Should().NotHaveCount(0);
        var userToken = userTokens.First();

        userToken.UserId.Should().Be(user.Id);
        userToken.Token.Should().NotBeEmpty();
        userToken.Expires.Should().NotBeSameDateAs(DateTimeService.Now);
        userToken.Created.Should().NotBeSameDateAs(DateTimeService.Now);
        userToken.CreatedByIp.Should().NotBeEmpty();
        userToken.Command.Should().Be(nameof(ReAuthenticateUserCommand));

        var userRefreshTokens = await databaseContext.UserRefreshTokens
            .Where(tokens => tokens.UserId == user.Id)
            .ToListAsync();

        userRefreshTokens.Should().HaveCount(2);
            
        userRefreshTokens[1].UserId.Should().Be(user.Id);
        userRefreshTokens[1].Token.Should().Be(newRefreshToken.Token);
        userRefreshTokens[1].Expires.Should().Be(newRefreshToken.Expires);
        userRefreshTokens[1].Created.Should().Be(newRefreshToken.Created);
        userRefreshTokens[1].CreatedByIp.Should().Be(newRefreshToken.CreatedByIp);
        userRefreshTokens[1].Revoked.Should().Be(newRefreshToken.Revoked);
        userRefreshTokens[1].RevokedByIp.Should().Be(newRefreshToken.RevokedByIp);
        userRefreshTokens[1].ReplacedByToken.Should().Be(newRefreshToken.ReplacedByToken);
        userRefreshTokens[1].ReasonRevoked.Should().Be(newRefreshToken.ReasonRevoked);
    }
        
    [Fact]
    public async Task GivenInactiveRefreshToken_WhenReAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var emailAddress = DataUtilityService.GetRandomEmail();
        var cryptedPassword = DataUtilityService.GetRandomString(60);
        var ipAddress = DataUtilityService.GetRandomIpAddress().ToString();
        var generateUserRefreshToken = DataUtilityService.GetRandomString(255);
        var expires = DateTimeService.Now.AddMinutes(300);
        var created = DateTimeService.Now.AddDays(-5);
            
        var reAuthenticateUserCommand = new ReAuthenticateUserCommand { RefreshToken = DataUtilityService.GetRandomString() };
        var user = new Users
        {
            Id = userId,
            EmailAddress = emailAddress,
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            LastLogged = null,
            CryptedPassword = cryptedPassword
        };

        var userRefreshToken = new UserRefreshTokens
        {
            UserId = user.Id,
            Token = generateUserRefreshToken,
            Expires = expires,
            Created = created,
            CreatedByIp = ipAddress,
            Revoked = null,
            RevokedByIp = null,
            ReplacedByToken = null,
            ReasonRevoked = null
        };
            
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
        await databaseContext.SaveChangesAsync();
            
        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserServiceProvider = new Mock<IUserService>();

        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserServiceProvider
            .Setup(service => service.IsRefreshTokenRevoked(It.IsAny<UserRefreshTokens>()))
            .Returns(false);

        mockedUserServiceProvider
            .Setup(service => service.IsRefreshTokenActive(It.IsAny<UserRefreshTokens>()))
            .Returns(false);
            
        var identityServer = new IdentityServer
        {
            Issuer = DataUtilityService.GetRandomString(),
            Audience = DataUtilityService.GetRandomString(),
            WebSecret = DataUtilityService.GetRandomString(),
            RequireHttps = false,
            WebTokenExpiresIn = 90,
            RefreshTokenExpiresIn = 120
        };
            
        var mockedApplicationSettings = MockApplicationSettings(identityServer: identityServer);

        // Act
        // Assert
        var reAuthenticateUserCommandHandler = new ReAuthenticateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTimeService.Object, 
            mockedUserServiceProvider.Object, 
            mockedApplicationSettings.Object);

        var result = await Assert.ThrowsAsync<AccessException>(() => 
            reAuthenticateUserCommandHandler.Handle(reAuthenticateUserCommand, CancellationToken.None));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
    }
        
    [Fact]
    public async Task GivenMissingRefreshToken_WhenReAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var reAuthenticateUserCommand = new ReAuthenticateUserCommand { RefreshToken = DataUtilityService.GetRandomString() };
        var databaseContext = GetTestDatabaseContext();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserServiceProvider = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();

        // Act
        // Assert
        var reAuthenticateUserCommandHandler = new ReAuthenticateUserCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedDateTimeService.Object, 
            mockedUserServiceProvider.Object, 
            mockedApplicationSettings.Object);

        var result = await Assert.ThrowsAsync<AccessException>(() => 
            reAuthenticateUserCommandHandler.Handle(reAuthenticateUserCommand, CancellationToken.None));

        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
    }
}