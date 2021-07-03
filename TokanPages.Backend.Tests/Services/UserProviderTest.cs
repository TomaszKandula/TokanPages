using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Services
{
    public class UserProviderTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public UserProviderTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenValidUserIdWithinHttpContextClaims_WhenInvokeGetUserId_ShouldReturnLoggedUserId()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUsers = new List<Domain.Entities.Users>
            {
                new ()
                {
                    Id = LLoggedUserId,
                    EmailAddress = FDataProviderService.GetRandomEmail(),
                    UserAlias = FDataProviderService.GetRandomString(),
                    FirstName = FDataProviderService.GetRandomString(),
                    LastName = FDataProviderService.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null,
                    CryptedPassword = FDataProviderService.GetRandomString()
                }
            };

            var LHttpContext = new Mock<IHttpContextAccessor>();
            LHttpContext
                .Setup(AHttpContext => AHttpContext.HttpContext.User.Claims)
                .Returns(new List<Claim>
                {
                    new (Claims.UserId, LLoggedUserId.ToString())
                });
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.GetUserId();

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().Be(LLoggedUserId);
        }
        
        [Fact]
        public async Task GivenNoUserIdWithinHttpContextClaims_WhenInvokeGetUserId_ShouldThrowError()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUsers = new List<Domain.Entities.Users>
            {
                new ()
                {
                    Id = LLoggedUserId,
                    EmailAddress = FDataProviderService.GetRandomEmail(),
                    UserAlias = FDataProviderService.GetRandomString(),
                    FirstName = FDataProviderService.GetRandomString(),
                    LastName = FDataProviderService.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null,
                    CryptedPassword = FDataProviderService.GetRandomString()
                }
            };

            var LHttpContext = new Mock<IHttpContextAccessor>();
            LHttpContext
                .Setup(AHttpContext => AHttpContext.HttpContext.User.Claims)
                .Returns(new List<Claim>());
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserId);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenInvalidUserIdWithinHttpContextClaims_WhenInvokeGetUserId_ShouldThrowError()
        {
            // Arrange
            var LUsers = new List<Domain.Entities.Users>
            {
                new ()
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    EmailAddress = FDataProviderService.GetRandomEmail(),
                    UserAlias = FDataProviderService.GetRandomString(),
                    FirstName = FDataProviderService.GetRandomString(),
                    LastName = FDataProviderService.GetRandomString(),
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastUpdated = null,
                    LastLogged = null,
                    CryptedPassword = FDataProviderService.GetRandomString()
                }
            };

            var LHttpContext = new Mock<IHttpContextAccessor>();
            LHttpContext
                .Setup(AHttpContext => AHttpContext.HttpContext.User.Claims)
                .Returns(new List<Claim>
                {
                    new (Claims.UserId, Guid.NewGuid().ToString())
                });
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserId);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }

        [Fact]
        public async Task GivenValidUserIdWithinHttpContextClaims_WhenInvokeGetUser_ShouldReturnJsonObject()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUser = new Domain.Entities.Users
            {
                Id = LLoggedUserId,
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LUsers = new List<Domain.Entities.Users> { LUser };
            var LHttpContext = new Mock<IHttpContextAccessor>();
            LHttpContext
                .Setup(AHttpContext => AHttpContext.HttpContext.User.Claims)
                .Returns(new List<Claim>
                {
                    new (Claims.UserId, LLoggedUserId.ToString())
                });
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.GetUser();
            
            // Assert
            LResult.UserId.Should().Be(LUser.Id);
            LResult.AliasName.Should().Be(LUser.UserAlias);
            LResult.AvatarName.Should().Be(LUser.AvatarName);
            LResult.FirstName.Should().Be(LUser.FirstName);
            LResult.LastName.Should().Be(LUser.LastName);
            LResult.ShortBio.Should().Be(LUser.ShortBio);
            LResult.Registered.Should().Be(LUser.Registered);
        }
        
        [Fact]
        public async Task GivenInvalidUserIdWithinHttpContextClaims_WhenInvokeGetUser_ShouldThrowError()
        {
            // Arrange
            var LUser = new Domain.Entities.Users
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LUsers = new List<Domain.Entities.Users> { LUser };

            var LHttpContext = new Mock<IHttpContextAccessor>();
            LHttpContext
                .Setup(AHttpContext => AHttpContext.HttpContext.User.Claims)
                .Returns(new List<Claim>
                {
                    new (Claims.UserId, Guid.NewGuid().ToString())
                });
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUser);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        
    }
}