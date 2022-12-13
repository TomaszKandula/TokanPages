using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.ApplicationSettings.Models;
using TokanPages.Services.CipheringService;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService;
using TokanPages.Services.WebTokenService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class AuthenticateUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenValidCredentials_WhenAuthenticateUser_ShouldSucceed()
    {
        // Arrange
        var emailAddress = DataUtilityService.GetRandomEmail();
        var plainTextPassword = DataUtilityService.GetRandomString(10);
        var cryptedPassword = DataUtilityService.GetRandomString(60);
        var generatedUserToken = DataUtilityService.GetRandomString(255);
        var ipAddress = DataUtilityService.GetRandomIpAddress().ToString();

        var user = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            EmailAddress = emailAddress,
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserInfo.AddAsync(userInfo);
        await databaseContext.SaveChangesAsync();

        var command = new AuthenticateUserCommand
        {
            EmailAddress = emailAddress,
            Password = plainTextPassword
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedCipheringService = new Mock<ICipheringService>();

        mockedCipheringService
            .Setup(service => service
                .VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);

        var refreshTokenExpires = DateTimeService.Now.AddDays(10);
        var refreshTokenCreated = DateTimeService.Now;
        var generatedRefreshToken = new RefreshToken
        {
            Token = DataUtilityService.GetRandomString(),
            Expires = refreshTokenExpires,
            Created = refreshTokenCreated,
            CreatedByIp = ipAddress
        };

        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        mockedJwtUtilityService
            .Setup(service => service
                .GenerateRefreshToken(
                    It.IsAny<string>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
            .Returns(generatedRefreshToken);

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var randomDateTime = DataUtilityService.GetRandomDateTime();
        mockedDateTimeService
            .SetupGet(service => service.Now)
            .Returns(randomDateTime);

        var mockedUserServiceProvider = new Mock<IUserService>();
        mockedUserServiceProvider
            .Setup(service => service.GenerateUserToken(
                It.IsAny<Backend.Domain.Entities.Users>(), 
                It.IsAny<DateTime>(), 
                CancellationToken.None))
            .ReturnsAsync(generatedUserToken);

        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserServiceProvider
            .Setup(service => service
                .DeleteOutdatedRefreshTokens(
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()));

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
        var handler = new AuthenticateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedCipheringService.Object, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedUserServiceProvider.Object, 
            mockedApplicationSettings.Object);
            
        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.UserId.Should().Be(user.Id);
        result.AliasName.Should().Be(user.UserAlias);
        result.UserToken.Should().NotBeNullOrEmpty();
        result.UserToken.Length.Should().BeGreaterThan(0);

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
        userToken.Command.Should().Be(nameof(AuthenticateUserCommand));

        var userRefreshTokens = await databaseContext.UserRefreshTokens
            .Where(tokens => tokens.UserId == user.Id)
            .ToListAsync();

        userRefreshTokens.Should().NotHaveCount(0);
        var userRefreshToken = userRefreshTokens.First();

        userRefreshToken.UserId.Should().Be(user.Id);
        userRefreshToken.Token.Should().NotBeEmpty();
        userRefreshToken.Expires.Should().Be(refreshTokenExpires);
        userRefreshToken.Created.Should().Be(refreshTokenCreated);
        userRefreshToken.CreatedByIp.Should().NotBeEmpty();
        userRefreshToken.Revoked.Should().BeNull();
        userRefreshToken.RevokedByIp.Should().BeNull();
        userRefreshToken.ReplacedByToken.Should().BeNull();
        userRefreshToken.ReasonRevoked.Should().BeNull();
    }

    [Fact]
    public async Task GivenInvalidEmailAddress_WhenAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var emailAddress = DataUtilityService.GetRandomEmail();
        var plainTextPassword = DataUtilityService.GetRandomString(10);
        var cryptedPassword = DataUtilityService.GetRandomString(60);

        var user = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = cryptedPassword
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var command = new AuthenticateUserCommand
        {
            EmailAddress = emailAddress,
            Password = plainTextPassword
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedCipheringService = new Mock<ICipheringService>();
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserServiceProvider = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();
        var handler = new AuthenticateUserCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedCipheringService.Object,
            mockedJwtUtilityService.Object,
            mockedDateTimeService.Object,
            mockedUserServiceProvider.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_CREDENTIALS));
    }
        
    [Fact]
    public async Task GivenInvalidPassword_WhenAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var emailAddress = DataUtilityService.GetRandomEmail();
        var plainTextPassword = DataUtilityService.GetRandomString(10);
        var cryptedPassword = DataUtilityService.GetRandomString(60);

        var user = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            EmailAddress = emailAddress,
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = cryptedPassword
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var command = new AuthenticateUserCommand
        {
            EmailAddress = emailAddress,
            Password = plainTextPassword
        };

        var mockedCipheringService = new Mock<ICipheringService>();
        mockedCipheringService
            .Setup(service => service
                .VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(false);

        var mockedLogger = new Mock<ILoggerService>();
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserServiceProvider = new Mock<IUserService>();
        var mockedApplicationSettings = MockApplicationSettings();
        var handler = new AuthenticateUserCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedCipheringService.Object,
            mockedJwtUtilityService.Object,
            mockedDateTimeService.Object,
            mockedUserServiceProvider.Object,
            mockedApplicationSettings.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_CREDENTIALS));
    }
        
    [Fact]
    public async Task GivenInactiveAccount_WhenAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var emailAddress = DataUtilityService.GetRandomEmail();
        var plainTextPassword = DataUtilityService.GetRandomString(10);
        var cryptedPassword = DataUtilityService.GetRandomString(60);
        var generatedUserToken = DataUtilityService.GetRandomString(255);
        var ipAddress = DataUtilityService.GetRandomIpAddress().ToString();
            
        var user = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            EmailAddress = emailAddress,
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = false,
            CryptedPassword = cryptedPassword
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var command = new AuthenticateUserCommand
        {
            EmailAddress = emailAddress,
            Password = plainTextPassword
        };

        var mockedLogger = new Mock<ILoggerService>();
        var mockedCipheringService = new Mock<ICipheringService>();

        mockedCipheringService
            .Setup(service => service
                .VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
            .Returns(true);
            
        var refreshTokenExpires = DateTimeService.Now.AddDays(10);
        var refreshTokenCreated = DateTimeService.Now;
        var generatedRefreshToken = new RefreshToken
        {
            Token = DataUtilityService.GetRandomString(),
            Expires = refreshTokenExpires,
            Created = refreshTokenCreated,
            CreatedByIp = ipAddress
        };

        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        mockedJwtUtilityService
            .Setup(service => service
                .GenerateRefreshToken(
                    It.IsAny<string>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
            .Returns(generatedRefreshToken);
            
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var randomDateTime = DataUtilityService.GetRandomDateTime();
        mockedDateTimeService
            .SetupGet(service => service.Now)
            .Returns(randomDateTime);
            
        var mockedUserServiceProvider = new Mock<IUserService>();
        mockedUserServiceProvider
            .Setup(service => service.GenerateUserToken(
                It.IsAny<Backend.Domain.Entities.Users>(), 
                It.IsAny<DateTime>(), 
                CancellationToken.None))
            .ReturnsAsync(generatedUserToken);
            
        mockedUserServiceProvider
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserServiceProvider
            .Setup(service => service
                .DeleteOutdatedRefreshTokens(
                    It.IsAny<Guid>(),
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()));
            
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
            
        var handler = new AuthenticateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedCipheringService.Object, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedUserServiceProvider.Object, 
            mockedApplicationSettings.Object);
            
        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE));
    }
}