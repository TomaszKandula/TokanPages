namespace TokanPages.Backend.Tests.Handlers.Users
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Shared.Models;
    using Domain.Entities;
    using Core.Exceptions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using Cqrs.Services.CipheringService;
    using Shared.Services.DateTimeService;
    using Cqrs.Services.UserServiceProvider;
    using Identity.Services.JwtUtilityService;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class AuthenticateUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenValidCredentials_WhenAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LPlainTextPassword = DataUtilityService.GetRandomString(10);
            var LCryptedPassword = DataUtilityService.GetRandomString(60);
            var LGeneratedJwt = DataUtilityService.GetRandomString(255);
            
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

            var LRoles = new List<Roles>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.EverydayUser),
                    Description = "User"
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.ArticlePublisher),
                    Description = "User can publish articles"
                }
            };

            var LUserRoles = new List<UserRoles>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[0].Id
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
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
            
            var LMockedJwtUtilityService = new Mock<IJwtUtilityService>();
            LMockedJwtUtilityService
                .Setup(AUtilityService => AUtilityService.GenerateJwt(
                    It.IsAny<DateTime>(),
                    It.IsAny<ClaimsIdentity>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(LGeneratedJwt);

            var LRefreshTokenExpires = DateTimeService.Now.AddDays(10);
            var LRefreshTokenCreated = DateTimeService.Now;
            var LGeneratedRefreshToken = new RefreshToken
            {
                Token = DataUtilityService.GetRandomString(),
                Expires = LRefreshTokenExpires,
                Created = LRefreshTokenCreated,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString()
            };
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
            var LMockedIdentityServer = new Mock<IdentityServer>();
            
            // Act
            var LAuthenticateUserCommandHandler = new AuthenticateUserCommandHandler(
                LDatabaseContext, 
                LMockedCipheringService.Object, 
                LMockedJwtUtilityService.Object, 
                LMockedDateTimeService.Object, 
                LMockedUserServiceProvider.Object, 
                LMockedIdentityServer.Object);
            
            var LResult = await LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None);

            // Assert
            LResult.UserId.Should().Be(LUser.Id);
            LResult.AliasName.Should().Be(LUser.UserAlias);
            LResult.AvatarName.Should().Be(LUser.AvatarName);
            LResult.FirstName.Should().Be(LUser.FirstName);
            LResult.LastName.Should().Be(LUser.LastName);
            LResult.ShortBio.Should().Be(LUser.ShortBio);
            LResult.Registered.Should().Be(LUser.Registered);
            LResult.Jwt.Should().NotBeNullOrEmpty();
            LResult.Jwt.Length.Should().BeGreaterThan(0);
            
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
        public async Task GivenOldRefreshTokens_WhenAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var LEmailAddress = DataUtilityService.GetRandomEmail();
            var LPlainTextPassword = DataUtilityService.GetRandomString(10);
            var LCryptedPassword = DataUtilityService.GetRandomString(60);
            var LGeneratedJwt = DataUtilityService.GetRandomString(255);
            
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

            var LRoles = new List<Roles>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.EverydayUser),
                    Description = "User"
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.ArticlePublisher),
                    Description = "User can publish articles"
                }
            };

            var LUserRoles = new List<UserRoles>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[0].Id
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[1].Id
                }
            };

            var LExistingRefreshTokens = new List<UserRefreshTokens>
            {
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    Token = DataUtilityService.GetRandomString(),
                    Expires = DateTimeService.Now.AddDays(-6),
                    Created = DateTimeService.Now.AddDays(-7),
                    CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString()
                },
                new ()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    Token = DataUtilityService.GetRandomString(),
                    Expires = DateTimeService.Now.AddDays(-6),
                    Created = DateTimeService.Now.AddDays(-5),
                    CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString()
                },
            };
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.UserRefreshTokens.AddRangeAsync(LExistingRefreshTokens);
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
            
            var LMockedJwtUtilityService = new Mock<IJwtUtilityService>();
            LMockedJwtUtilityService
                .Setup(AUtilityService => AUtilityService.GenerateJwt(
                    It.IsAny<DateTime>(),
                    It.IsAny<ClaimsIdentity>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(LGeneratedJwt);

            var LRefreshTokenExpires = DateTimeService.Now.AddDays(10);
            var LRefreshTokenCreated = DateTimeService.Now;
            var LGeneratedRefreshToken = new RefreshToken
            {
                Token = DataUtilityService.GetRandomString(),
                Expires = LRefreshTokenExpires,
                Created = LRefreshTokenCreated,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString()
            };
            LMockedJwtUtilityService
                .Setup(AUtilityService => AUtilityService
                    .GenerateRefreshToken(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(LGeneratedRefreshToken);
            
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LRandomDateTime = DateTimeService.Now.AddMinutes(10);
            LMockedDateTimeService
                .SetupGet(ADateTimeService => ADateTimeService.Now)
                .Returns(LRandomDateTime);
            
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
            
            var LResult = await LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None);

            // Assert
            LResult.UserId.Should().Be(LUser.Id);
            LResult.AliasName.Should().Be(LUser.UserAlias);
            LResult.AvatarName.Should().Be(LUser.AvatarName);
            LResult.FirstName.Should().Be(LUser.FirstName);
            LResult.LastName.Should().Be(LUser.LastName);
            LResult.ShortBio.Should().Be(LUser.ShortBio);
            LResult.Registered.Should().Be(LUser.Registered);
            LResult.Jwt.Should().NotBeNullOrEmpty();
            LResult.Jwt.Length.Should().BeGreaterThan(0);
            
            var LUserRefreshTokens = await LDatabaseContext.UserRefreshTokens
                .Where(AUserRefreshToken => AUserRefreshToken.UserId == LUser.Id)
                .ToListAsync();

            LUserRefreshTokens.Should().HaveCount(1);

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

            var LRoles = new List<Roles>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.EverydayUser),
                    Description = "User"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.ArticlePublisher),
                    Description = "User can publish articles"
                }
            };

            var LUserRoles = new List<UserRoles>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
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
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None));
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

            var LRoles = new List<Roles>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.EverydayUser),
                    Description = "User"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = nameof(Identity.Authorization.Roles.ArticlePublisher),
                    Description = "User can publish articles"
                }
            };

            var LUserRoles = new List<UserRoles>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[0].Id
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    UserId = LUser.Id,
                    RoleId = LRoles[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUser);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
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
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LAuthenticateUserCommandHandler.Handle(LAuthenticateUserCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.INVALID_CREDENTIALS));
        }
    }
}