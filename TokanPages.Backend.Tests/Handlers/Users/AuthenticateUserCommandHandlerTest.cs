namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared.Models;
    using Domain.Entities;
    using Core.Exceptions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Cqrs.Services.CipheringService;
    using Core.Utilities.DateTimeService;
    using Cqrs.Services.UserServiceProvider;
    using Identity.Services.JwtUtilityService;

    public class AuthenticateUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidCredentials_WhenAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LPlainTextPassword = DataUtilityService.GetRandomString(10);
            var LCryptedPassword = DataUtilityService.GetRandomString(60);
            var LGeneratedUserToken = DataUtilityService.GetRandomString(255);
            var LIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            
            var LUser = new Users
            {
                Id = Guid.NewGuid(),
                EmailAddress = LEmailAddress,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = LCryptedPassword
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = LEmailAddress,
                Password = LPlainTextPassword
            };

            var LMockedCipheringService = new Mock<ICipheringService>();
            LMockedCipheringService
                .Setup(AService => AService
                    .VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            
            var LRefreshTokenExpires = DateTimeService.Now.AddDays(10);
            var LRefreshTokenCreated = DateTimeService.Now;
            var LGeneratedRefreshToken = new RefreshToken
            {
                Token = DataUtilityService.GetRandomString(),
                Expires = LRefreshTokenExpires,
                Created = LRefreshTokenCreated,
                CreatedByIp = LIpAddress
            };

            var LMockedJwtUtilityService = new Mock<IJwtUtilityService>();
            LMockedJwtUtilityService
                .Setup(AUtilityService => AUtilityService
                    .GenerateRefreshToken(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(LGeneratedRefreshToken);
            
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LRandomDateTime = DataUtilityService.GetRandomDateTime();
            LMockedDateTimeService
                .SetupGet(ADateTimeService => ADateTimeService.Now)
                .Returns(LRandomDateTime);
            
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GenerateUserToken(
                    It.IsAny<Users>(), 
                    It.IsAny<DateTime>(), 
                    CancellationToken.None))
                .ReturnsAsync(LGeneratedUserToken);
            
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GetRequestIpAddress())
                .Returns(LIpAddress);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .SetRefreshTokenCookie(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<string>()));

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .DeleteOutdatedRefreshTokens(
                        It.IsAny<Guid>(),
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()));
            
            var LIdentityServer = new IdentityServer
            {
                Issuer = DataUtilityService.GetRandomString(),
                Audience = DataUtilityService.GetRandomString(),
                WebSecret = DataUtilityService.GetRandomString(),
                RequireHttps = false,
                WebTokenExpiresIn = 90,
                RefreshTokenExpiresIn = 120
            };
            
            // Act
            var LAuthenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                LDatabaseContext, 
                LMockedCipheringService.Object, 
                LMockedJwtUtilityService.Object, 
                LMockedDateTimeService.Object, 
                LMockedUserServiceProvider.Object, 
                LIdentityServer);
            
            var LResult = await LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None);

            // Assert
            LResult.UserId.Should().Be(LUser.Id);
            LResult.AliasName.Should().Be(LUser.UserAlias);
            LResult.AvatarName.Should().Be(LUser.AvatarName);
            LResult.FirstName.Should().Be(LUser.FirstName);
            LResult.LastName.Should().Be(LUser.LastName);
            LResult.ShortBio.Should().Be(LUser.ShortBio);
            LResult.Registered.Should().Be(LUser.Registered);
            LResult.UserToken.Should().NotBeNullOrEmpty();
            LResult.UserToken.Length.Should().BeGreaterThan(0);

            var LUserTokens = await LDatabaseContext.UserTokens
                .Where(AUserToken => AUserToken.UserId == LUser.Id)
                .ToListAsync();

            LUserTokens.Should().NotHaveCount(0);
            var LUserToken = LUserTokens.First();

            LUserToken.UserId.Should().Be(LUser.Id);
            LUserToken.Token.Should().NotBeEmpty();
            LUserToken.Expires.Should().NotBeSameDateAs(DateTimeService.Now);
            LUserToken.Created.Should().NotBeSameDateAs(DateTimeService.Now);
            LUserToken.CreatedByIp.Should().NotBeEmpty();

            var LUserRefreshTokens = await LDatabaseContext.UserRefreshTokens
                .Where(AUserRefreshToken => AUserRefreshToken.UserId == LUser.Id)
                .ToListAsync();

            LUserRefreshTokens.Should().NotHaveCount(0);
            var LUserRefreshToken = LUserRefreshTokens.First();

            LUserRefreshToken.UserId.Should().Be(LUser.Id);
            LUserRefreshToken.Token.Should().NotBeEmpty();
            LUserRefreshToken.Expires.Should().Be(LRefreshTokenExpires);
            LUserRefreshToken.Created.Should().Be(LRefreshTokenCreated);
            LUserRefreshToken.CreatedByIp.Should().NotBeEmpty();
            LUserRefreshToken.Revoked.Should().BeNull();
            LUserRefreshToken.RevokedByIp.Should().BeNull();
            LUserRefreshToken.ReplacedByToken.Should().BeNull();
            LUserRefreshToken.ReasonRevoked.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenInvalidEmailAddress_WhenAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LPlainTextPassword = DataUtilityService.GetRandomString(10);
            var LCryptedPassword = DataUtilityService.GetRandomString(60);

            var LUser = new Users
            {
                Id = Guid.NewGuid(),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = LCryptedPassword
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = LEmailAddress,
                Password = LPlainTextPassword
            };

            var LMockedCipheringService = new Mock<ICipheringService>();
            var LMockedJwtUtilityService = new Mock<IJwtUtilityService>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();
            var LMockedIdentityServer = new Mock<IdentityServer>();

            // Act
            var LAuthenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                LDatabaseContext,
                LMockedCipheringService.Object,
                LMockedJwtUtilityService.Object,
                LMockedDateTimeService.Object,
                LMockedUserServiceProvider.Object,
                LMockedIdentityServer.Object);

            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_CREDENTIALS));
        }
        
        [Fact]
        public async Task GivenInvalidPassword_WhenAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LPlainTextPassword = DataUtilityService.GetRandomString(10);
            var LCryptedPassword = DataUtilityService.GetRandomString(60);

            var LUser = new Users
            {
                Id = Guid.NewGuid(),
                EmailAddress = LEmailAddress,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = LCryptedPassword
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = LEmailAddress,
                Password = LPlainTextPassword
            };

            var LMockedCipheringService = new Mock<ICipheringService>();
            LMockedCipheringService
                .Setup(AService => AService
                    .VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var LMockedJwtUtilityService = new Mock<IJwtUtilityService>();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();
            var LMockedIdentityServer = new Mock<IdentityServer>();

            // Act
            var LAuthenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                LDatabaseContext,
                LMockedCipheringService.Object,
                LMockedJwtUtilityService.Object,
                LMockedDateTimeService.Object,
                LMockedUserServiceProvider.Object,
                LMockedIdentityServer.Object);

            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_CREDENTIALS));
        }
        
        [Fact]
        public async Task GivenInactiveAccount_WhenAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LPlainTextPassword = DataUtilityService.GetRandomString(10);
            var LCryptedPassword = DataUtilityService.GetRandomString(60);
            var LGeneratedUserToken = DataUtilityService.GetRandomString(255);
            var LIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            
            var LUser = new Users
            {
                Id = Guid.NewGuid(),
                EmailAddress = LEmailAddress,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = false,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = LCryptedPassword
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.SaveChangesAsync();

            var LAuthenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = LEmailAddress,
                Password = LPlainTextPassword
            };

            var LMockedCipheringService = new Mock<ICipheringService>();
            LMockedCipheringService
                .Setup(AService => AService
                    .VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            
            var LRefreshTokenExpires = DateTimeService.Now.AddDays(10);
            var LRefreshTokenCreated = DateTimeService.Now;
            var LGeneratedRefreshToken = new RefreshToken
            {
                Token = DataUtilityService.GetRandomString(),
                Expires = LRefreshTokenExpires,
                Created = LRefreshTokenCreated,
                CreatedByIp = LIpAddress
            };

            var LMockedJwtUtilityService = new Mock<IJwtUtilityService>();
            LMockedJwtUtilityService
                .Setup(AUtilityService => AUtilityService
                    .GenerateRefreshToken(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(LGeneratedRefreshToken);
            
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LRandomDateTime = DataUtilityService.GetRandomDateTime();
            LMockedDateTimeService
                .SetupGet(ADateTimeService => ADateTimeService.Now)
                .Returns(LRandomDateTime);
            
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GenerateUserToken(
                    It.IsAny<Users>(), 
                    It.IsAny<DateTime>(), 
                    CancellationToken.None))
                .ReturnsAsync(LGeneratedUserToken);
            
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GetRequestIpAddress())
                .Returns(LIpAddress);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .SetRefreshTokenCookie(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<string>()));

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .DeleteOutdatedRefreshTokens(
                        It.IsAny<Guid>(),
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()));
            
            var LIdentityServer = new IdentityServer
            {
                Issuer = DataUtilityService.GetRandomString(),
                Audience = DataUtilityService.GetRandomString(),
                WebSecret = DataUtilityService.GetRandomString(),
                RequireHttps = false,
                WebTokenExpiresIn = 90,
                RefreshTokenExpiresIn = 120
            };
            
            // Act
            var LAuthenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                LDatabaseContext, 
                LMockedCipheringService.Object, 
                LMockedJwtUtilityService.Object, 
                LMockedDateTimeService.Object, 
                LMockedUserServiceProvider.Object, 
                LIdentityServer);
            
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE));
        }
    }
}