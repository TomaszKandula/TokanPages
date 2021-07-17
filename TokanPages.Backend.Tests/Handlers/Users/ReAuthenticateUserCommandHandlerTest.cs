namespace TokanPages.Backend.Tests.Handlers.Users
{
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
    using Shared.Services.DateTimeService;
    using Cqrs.Services.UserServiceProvider;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class ReAuthenticateUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidRefreshToken_WhenReAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LCryptedPassword = DataUtilityService.GetRandomString(60);
            var LIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            var LGenerateUserRefreshToken = DataUtilityService.GetRandomString(255);
            var LExpires = DateTimeService.Now.AddMinutes(300);
            var LCreated = DateTimeService.Now.AddDays(-5);
            
            var LReAuthenticateUserCommand = new ReAuthenticateUserCommand
            {
                Id = LUserId
            };

            var LUser = new Users
            {
                Id = LUserId,
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

            var LUserRefreshToken = new UserRefreshTokens
            {
                UserId = LUser.Id,
                Token = LGenerateUserRefreshToken,
                Expires = LExpires,
                Created = LCreated,
                CreatedByIp = LIpAddress,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.UserRefreshTokens.AddAsync(LUserRefreshToken);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GetRefreshTokenCookie(It.IsAny<string>()))
                .Returns(LGenerateUserRefreshToken);
            
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GetRequestIpAddress())
                .Returns(LIpAddress);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.IsRefreshTokenRevoked(It.IsAny<UserRefreshTokens>()))
                .Returns(false);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.IsRefreshTokenActive(It.IsAny<UserRefreshTokens>()))
                .Returns(true);
            
            var LNewRefreshToken = new UserRefreshTokens
            {
                UserId = LUser.Id,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(120),
                Created = DateTimeService.Now,
                CreatedByIp = LIpAddress,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .ReplaceRefreshToken(
                        It.IsAny<Guid>(), 
                        It.IsAny<UserRefreshTokens>(), 
                        It.IsAny<string>()))
                .Returns(LNewRefreshToken);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .DeleteOutdatedRefreshTokens(
                        It.IsAny<Guid>(), 
                        It.IsAny<bool>(),
                        It.IsAny<CancellationToken>()));

            var LNewUserToken = DataUtilityService.GetRandomString();
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GenerateUserToken(
                    It.IsAny<Users>(), 
                    It.IsAny<DateTime>(), 
                    CancellationToken.None))
                .ReturnsAsync(LNewUserToken);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .SetRefreshTokenCookie(
                        It.IsAny<string>(), 
                        It.IsAny<int>(), 
                        It.IsAny<bool>(), 
                        It.IsAny<string>()));
            
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
            var LReAuthenticateUserCommandHandler = new ReAuthenticateUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTimeService.Object, 
                LMockedUserServiceProvider.Object, 
                LIdentityServer);

            var LResult = await LReAuthenticateUserCommandHandler.Handle(LReAuthenticateUserCommand, CancellationToken.None);
            
            // Assert
            LResult.UserId.Should().Be(LUser.Id);
            LResult.AliasName.Should().Be(LUser.UserAlias);
            LResult.AvatarName.Should().Be(LUser.AvatarName);
            LResult.FirstName.Should().Be(LUser.FirstName);
            LResult.LastName.Should().Be(LUser.LastName);
            LResult.ShortBio.Should().Be(LUser.ShortBio);
            LResult.Registered.Should().Be(LUser.Registered);
            LResult.UserToken.Should().Be(LNewUserToken);
            
            var LUserRefreshTokens = await LDatabaseContext.UserRefreshTokens
                .Where(AUserRefreshToken => AUserRefreshToken.UserId == LUser.Id)
                .ToListAsync();

            LUserRefreshTokens.Should().HaveCount(2);
            
            LUserRefreshTokens[1].UserId.Should().Be(LUser.Id);
            LUserRefreshTokens[1].Token.Should().Be(LNewRefreshToken.Token);
            LUserRefreshTokens[1].Expires.Should().Be(LNewRefreshToken.Expires);
            LUserRefreshTokens[1].Created.Should().Be(LNewRefreshToken.Created);
            LUserRefreshTokens[1].CreatedByIp.Should().Be(LNewRefreshToken.CreatedByIp);
            LUserRefreshTokens[1].Revoked.Should().Be(LNewRefreshToken.Revoked);
            LUserRefreshTokens[1].RevokedByIp.Should().Be(LNewRefreshToken.RevokedByIp);
            LUserRefreshTokens[1].ReplacedByToken.Should().Be(LNewRefreshToken.ReplacedByToken);
            LUserRefreshTokens[1].ReasonRevoked.Should().Be(LNewRefreshToken.ReasonRevoked);
        }
        
        [Fact]
        public async Task GivenInactiveRefreshToken_WhenReAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.NewGuid();
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LCryptedPassword = DataUtilityService.GetRandomString(60);
            var LIpAddress = DataUtilityService.GetRandomIpAddress().ToString();
            var LGenerateUserRefreshToken = DataUtilityService.GetRandomString(255);
            var LExpires = DateTimeService.Now.AddMinutes(300);
            var LCreated = DateTimeService.Now.AddDays(-5);
            
            var LReAuthenticateUserCommand = new ReAuthenticateUserCommand
            {
                Id = LUserId
            };

            var LUser = new Users
            {
                Id = LUserId,
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

            var LUserRefreshToken = new UserRefreshTokens
            {
                UserId = LUser.Id,
                Token = LGenerateUserRefreshToken,
                Expires = LExpires,
                Created = LCreated,
                CreatedByIp = LIpAddress,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.UserRefreshTokens.AddAsync(LUserRefreshToken);
            await LDatabaseContext.SaveChangesAsync();
            
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GetRefreshTokenCookie(It.IsAny<string>()))
                .Returns(LGenerateUserRefreshToken);
            
            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.GetRequestIpAddress())
                .Returns(LIpAddress);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.IsRefreshTokenRevoked(It.IsAny<UserRefreshTokens>()))
                .Returns(false);

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService.IsRefreshTokenActive(It.IsAny<UserRefreshTokens>()))
                .Returns(false);
            
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
            // Assert
            var LReAuthenticateUserCommandHandler = new ReAuthenticateUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTimeService.Object, 
                LMockedUserServiceProvider.Object, 
                LIdentityServer);

            var LResult = await Assert.ThrowsAsync<BusinessException>(() => 
                LReAuthenticateUserCommandHandler.Handle(LReAuthenticateUserCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_REFRESH_TOKEN));
        }
        
        [Fact]
        public async Task GivenMissingRefreshToken_WhenReAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LReAuthenticateUserCommand = new ReAuthenticateUserCommand
            {
                Id = Guid.NewGuid()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LMockedUserServiceProvider = new Mock<IUserServiceProvider>();

            LMockedUserServiceProvider
                .Setup(AUserService => AUserService
                    .GetRefreshTokenCookie(It.IsAny<string>()))
                .Returns(string.Empty);
            
            var LIdentityServer = new IdentityServer();
            
            // Act
            // Assert
            var LReAuthenticateUserCommandHandler = new ReAuthenticateUserCommandHandler(
                LDatabaseContext, 
                LMockedDateTimeService.Object, 
                LMockedUserServiceProvider.Object, 
                LIdentityServer);

            var LResult = await Assert.ThrowsAsync<BusinessException>(() => 
                LReAuthenticateUserCommandHandler.Handle(LReAuthenticateUserCommand, CancellationToken.None));

            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.MISSING_REFRESH_TOKEN));
        }
    }
}