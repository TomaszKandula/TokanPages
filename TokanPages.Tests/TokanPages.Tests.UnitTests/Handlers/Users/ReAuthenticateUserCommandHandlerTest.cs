using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

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

        var command = new ReAuthenticateUserCommand { RefreshToken = refreshToken };
        var user = new Backend.Domain.Entities.Users
        {
            Id = userId,
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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserInfo.AddAsync(userInfo);
        await databaseContext.UserRefreshTokens.AddAsync(userRefreshToken);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserService
            .Setup(service => service.IsRefreshTokenRevoked(It.IsAny<UserRefreshTokens>()))
            .Returns(false);

        mockedUserService
            .Setup(service => service.IsRefreshTokenActive(It.IsAny<UserRefreshTokens>()))
            .Returns(true);

        mockedUserService
            .Setup(service => service
                .ReplaceRefreshToken(
                    It.IsAny<ReplaceRefreshTokenInput>(), 
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(newRefreshToken);

        mockedUserService
            .Setup(service => service
                .DeleteOutdatedRefreshTokens(
                    It.IsAny<Guid>(), 
                    It.IsAny<bool>(),
                    It.IsAny<CancellationToken>()));

        var newUserToken = DataUtilityService.GetRandomString();
        mockedUserService
            .Setup(service => service.GenerateUserToken(
                It.IsAny<Backend.Domain.Entities.Users>(), 
                It.IsAny<DateTime>(), 
                CancellationToken.None))
            .ReturnsAsync(newUserToken);

        var mockedConfig = new Mock<IConfiguration>();

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_Issuer"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_Audience"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_WebSecret"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<bool>("Ids_RequireHttps"))
            .Returns(false);

        mockedConfig
            .Setup(configuration => configuration.GetValue<int>("Ids_WebToken_Maturity"))
            .Returns(90);

        mockedConfig
            .Setup(configuration => configuration.GetValue<int>("Ids_RefreshToken_Maturity"))
            .Returns(120);

        var handler = new ReAuthenticateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTimeService.Object, 
            mockedUserService.Object, 
            mockedConfig.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

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
            
        var command = new ReAuthenticateUserCommand { RefreshToken = DataUtilityService.GetRandomString() };
        var user = new Backend.Domain.Entities.Users
        {
            Id = userId,
            EmailAddress = emailAddress,
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
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
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                null, 
                false,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserService
            .Setup(service => service.IsRefreshTokenRevoked(It.IsAny<UserRefreshTokens>()))
            .Returns(false);

        mockedUserService
            .Setup(service => service.IsRefreshTokenActive(It.IsAny<UserRefreshTokens>()))
            .Returns(false);

        var mockedConfig = new Mock<IConfiguration>();

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_Issuer"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_Audience"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_WebSecret"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<bool>("Ids_RequireHttps"))
            .Returns(false);

        mockedConfig
            .Setup(configuration => configuration.GetValue<int>("Ids_WebToken_Maturity"))
            .Returns(90);

        mockedConfig
            .Setup(configuration => configuration.GetValue<int>("Ids_RefreshToken_Maturity"))
            .Returns(120);

        var handler = new ReAuthenticateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTimeService.Object, 
            mockedUserService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
    }
        
    [Fact]
    public async Task GivenMissingRefreshToken_WhenReAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var command = new ReAuthenticateUserCommand { RefreshToken = DataUtilityService.GetRandomString() };
        var user = new Backend.Domain.Entities.Users
        {
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                null, 
                false,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var ipAddress = DataUtilityService.GetRandomIpAddress().ToString();
        mockedUserService
            .Setup(service => service.GetRequestIpAddress())
            .Returns(ipAddress);

        mockedUserService
            .Setup(service => service.IsRefreshTokenRevoked(It.IsAny<UserRefreshTokens>()))
            .Returns(false);

        mockedUserService
            .Setup(service => service.IsRefreshTokenActive(It.IsAny<UserRefreshTokens>()))
            .Returns(false);

        var mockedConfig = new Mock<IConfiguration>();

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_Issuer"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_Audience"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<string>("Ids_WebSecret"))
            .Returns(DataUtilityService.GetRandomString());

        mockedConfig
            .Setup(configuration => configuration.GetValue<bool>("Ids_RequireHttps"))
            .Returns(false);

        mockedConfig
            .Setup(configuration => configuration.GetValue<int>("Ids_WebToken_Maturity"))
            .Returns(90);

        mockedConfig
            .Setup(configuration => configuration.GetValue<int>("Ids_RefreshToken_Maturity"))
            .Returns(120);

        var handler = new ReAuthenticateUserCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedDateTimeService.Object, 
            mockedUserService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
    }
}