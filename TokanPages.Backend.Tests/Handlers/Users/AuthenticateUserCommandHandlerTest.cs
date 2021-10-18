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
            var emailAddress = DataUtilityService.GetRandomEmail();
            var plainTextPassword = DataUtilityService.GetRandomString(10);
            var cryptedPassword = DataUtilityService.GetRandomString(60);
            var generatedUserToken = DataUtilityService.GetRandomString(255);
            var ipAddress = DataUtilityService.GetRandomIpAddress().ToString();
            
            var user = new Users
            {
                Id = Guid.NewGuid(),
                EmailAddress = emailAddress,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = cryptedPassword
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = emailAddress,
                Password = plainTextPassword
            };

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

            var mockedJwtUtilityService = new Mock<IJwtUtilityService>();
            mockedJwtUtilityService
                .Setup(service => service
                    .GenerateRefreshToken(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(generatedRefreshToken);
            
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var randomDateTime = DataUtilityService.GetRandomDateTime();
            mockedDateTimeService
                .SetupGet(service => service.Now)
                .Returns(randomDateTime);
            
            var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
            mockedUserServiceProvider
                .Setup(service => service.GenerateUserToken(
                    It.IsAny<Users>(), 
                    It.IsAny<DateTime>(), 
                    CancellationToken.None))
                .ReturnsAsync(generatedUserToken);
            
            mockedUserServiceProvider
                .Setup(service => service.GetRequestIpAddress())
                .Returns(ipAddress);

            mockedUserServiceProvider
                .Setup(service => service
                    .SetRefreshTokenCookie(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<string>()));

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
            
            // Act
            var authenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                databaseContext, 
                mockedCipheringService.Object, 
                mockedJwtUtilityService.Object, 
                mockedDateTimeService.Object, 
                mockedUserServiceProvider.Object, 
                identityServer);
            
            var result = await authenticateUserCommandHandler.Handle(authenticateUserCommand, CancellationToken.None);

            // Assert
            result.UserId.Should().Be(user.Id);
            result.AliasName.Should().Be(user.UserAlias);
            result.AvatarName.Should().Be(user.AvatarName);
            result.FirstName.Should().Be(user.FirstName);
            result.LastName.Should().Be(user.LastName);
            result.ShortBio.Should().Be(user.ShortBio);
            result.Registered.Should().Be(user.Registered);
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

            var user = new Users
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
                CryptedPassword = cryptedPassword
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = emailAddress,
                Password = plainTextPassword
            };

            var mockedCipheringService = new Mock<ICipheringService>();
            var mockedJwtUtilityService = new Mock<IJwtUtilityService>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
            var mockedIdentityServer = new Mock<IdentityServer>();

            // Act
            var authenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                databaseContext,
                mockedCipheringService.Object,
                mockedJwtUtilityService.Object,
                mockedDateTimeService.Object,
                mockedUserServiceProvider.Object,
                mockedIdentityServer.Object);

            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => authenticateUserCommandHandler.Handle(authenticateUserCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_CREDENTIALS));
        }
        
        [Fact]
        public async Task GivenInvalidPassword_WhenAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var emailAddress = DataUtilityService.GetRandomEmail();
            var plainTextPassword = DataUtilityService.GetRandomString(10);
            var cryptedPassword = DataUtilityService.GetRandomString(60);

            var user = new Users
            {
                Id = Guid.NewGuid(),
                EmailAddress = emailAddress,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = cryptedPassword
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = emailAddress,
                Password = plainTextPassword
            };

            var mockedCipheringService = new Mock<ICipheringService>();
            mockedCipheringService
                .Setup(service => service
                    .VerifyPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            var mockedJwtUtilityService = new Mock<IJwtUtilityService>();
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
            var mockedIdentityServer = new Mock<IdentityServer>();

            // Act
            var authenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                databaseContext,
                mockedCipheringService.Object,
                mockedJwtUtilityService.Object,
                mockedDateTimeService.Object,
                mockedUserServiceProvider.Object,
                mockedIdentityServer.Object);

            // Assert
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => authenticateUserCommandHandler.Handle(authenticateUserCommand, CancellationToken.None));
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
            
            var user = new Users
            {
                Id = Guid.NewGuid(),
                EmailAddress = emailAddress,
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = false,
                Registered = DateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = cryptedPassword
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(user);
            await databaseContext.SaveChangesAsync();

            var authenticateUserCommand = new AuthenticateUserCommand
            {
                EmailAddress = emailAddress,
                Password = plainTextPassword
            };

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

            var mockedJwtUtilityService = new Mock<IJwtUtilityService>();
            mockedJwtUtilityService
                .Setup(service => service
                    .GenerateRefreshToken(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(generatedRefreshToken);
            
            var mockedDateTimeService = new Mock<IDateTimeService>();
            var randomDateTime = DataUtilityService.GetRandomDateTime();
            mockedDateTimeService
                .SetupGet(service => service.Now)
                .Returns(randomDateTime);
            
            var mockedUserServiceProvider = new Mock<IUserServiceProvider>();
            mockedUserServiceProvider
                .Setup(service => service.GenerateUserToken(
                    It.IsAny<Users>(), 
                    It.IsAny<DateTime>(), 
                    CancellationToken.None))
                .ReturnsAsync(generatedUserToken);
            
            mockedUserServiceProvider
                .Setup(service => service.GetRequestIpAddress())
                .Returns(ipAddress);

            mockedUserServiceProvider
                .Setup(service => service
                    .SetRefreshTokenCookie(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<string>()));

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
            
            // Act
            var authenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                databaseContext, 
                mockedCipheringService.Object, 
                mockedJwtUtilityService.Object, 
                mockedDateTimeService.Object, 
                mockedUserServiceProvider.Object, 
                identityServer);
            
            var result = await Assert.ThrowsAsync<BusinessException>(() 
                => authenticateUserCommandHandler.Handle(authenticateUserCommand, CancellationToken.None));
            result.ErrorCode.Should().Be(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE));
        }
    }
}