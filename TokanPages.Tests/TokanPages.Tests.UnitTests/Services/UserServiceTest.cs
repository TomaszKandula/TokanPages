using System.Net;
using System.Security.Claims;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService;
using TokanPages.Services.UserService.Models;
using TokanPages.Services.WebTokenService.Abstractions;
using TokanPages.Services.WebTokenService.Models;
using Roles = TokanPages.Backend.Domain.Enums.Roles;
using Permissions = TokanPages.Backend.Domain.Enums.Permissions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class UserServiceTest : TestBase
{
    [Fact]
    public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUser_ShouldReturnJsonObject()
    {
        // Arrange
        var loggedUserId = Guid.NewGuid();
        var users = GetUser(loggedUserId).ToList();
        var userInfo = GetUserInfo(loggedUserId).ToList();
        var httpContext = GetMockedHttpContext(loggedUserId);

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserInfo.AddRangeAsync(userInfo);
        await databaseContext.SaveChangesAsync();

        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.GetUser();

        // Assert
        result.Should().NotBeNull();
        result?.UserId.Should().Be(users[0].Id);
        result?.AliasName.Should().Be(users[0].UserAlias);
        result?.AvatarName.Should().Be(userInfo[0].UserImageName);
        result?.FirstName.Should().Be(userInfo[0].FirstName);
        result?.LastName.Should().Be(userInfo[0].LastName);
        result?.ShortBio.Should().Be(userInfo[0].UserAboutText);
        result?.Registered.Should().Be(users[0].CreatedAt);
    }
        
    [Fact]
    public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUser_ShouldReturnNull()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid());
        var httpContext = GetMockedHttpContext(Guid.NewGuid());

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);
            
        // Act
        // Assert
        var result = await userProvider.GetUser();
        result.Should().BeNull();
    }

    [Fact]
    public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUser_ShouldReturnNull()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid());
        var httpContext = GetMockedHttpContext(null);

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.GetUser();

        // Assert
        result.Should().BeNull();
    }
        
    [Fact]
    public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldReturnJsonObject()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid()).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(users[0].Id);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.GetUserRoles(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result?[0].Name.Should().Be(roles[0].Name);
        result?[0].Description.Should().Be(roles[0].Description);
    }

    [Fact]
    public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldThrowError()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid()).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(Guid.NewGuid());
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => userProvider.GetUserRoles(null));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }

    [Fact]
    public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldThrowError()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid()).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(null);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => userProvider.GetUserRoles(null));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }
        
    [Fact]
    public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldReturnJsonObject()
    {
        // Arrange
        var userId = Guid.NewGuid(); 
        var users = GetUser(userId);
        var permissions = GetPermissions().ToList();
        var userPermissions = new List<UserPermissions>
        {
            new()
            {
                UserId = userId,
                PermissionId = permissions[0].Id
            },
            new()
            {
                UserId = userId,
                PermissionId = permissions[1].Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.UserPermissions.AddRangeAsync(userPermissions);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(userId);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.GetUserPermissions(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        var hasName1 = result?.Select(output => output.Name).Contains(permissions[0].Name);
        var hasName2 = result?.Select(output => output.Name).Contains(permissions[1].Name);
        hasName1.Should().BeTrue();
        hasName2.Should().BeTrue();
    }

    [Fact]
    public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid(); 
        var users = GetUser(userId);
        var permissions = GetPermissions().ToList();
        var userPermissions = new List<UserPermissions>
        {
            new()
            {
                UserId = userId,
                PermissionId = permissions[0].Id
            },
            new()
            {
                UserId = userId,
                PermissionId = permissions[1].Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.UserPermissions.AddRangeAsync(userPermissions);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(Guid.NewGuid());
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => userProvider.GetUserPermissions(null));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }

    [Fact]
    public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid(); 
        var users = GetUser(userId);
        var permissions = GetPermissions().ToList();
        var userPermissions = new List<UserPermissions>
        {
            new()
            {
                UserId = userId,
                PermissionId = permissions[0].Id
            },
            new()
            {
                UserId = userId,
                PermissionId = permissions[1].Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.UserPermissions.AddRangeAsync(userPermissions);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(null);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<AccessException>(() => userProvider.GetUserPermissions(null));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
    }

    [Fact]
    public async Task GivenValidClaimsInHttpContext_WhenInvokeHasRoleAssigned_ShouldReturnTrue()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid()).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(users[0].Id);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
            
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.HasRoleAssigned(nameof(Roles.EverydayUser));

        // Assert
        result.Should().BeTrue();
    }
        
    [Fact]
    public async Task GivenValidClaimsInHttpContextAndInvalidRole_WhenInvokeHasRoleAssigned_ShouldReturnFalse()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid()).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(users[0].Id);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.HasRoleAssigned(nameof(Roles.PhotoPublisher));

        // Assert
        result.Should().BeFalse();
    }
        
    [Fact]
    public async Task GivenNoUserClaimsInHttpContext_WhenInvokeHasRoleAssigned_ShouldReturnFalse()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid()).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(null);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await userProvider.HasRoleAssigned(nameof(Roles.EverydayUser));
        result.Should().BeFalse();
    }
        
    [Fact]
    public async Task GivenValidClaimsInHttpContextAndNoRole_WhenInvokeHasRoleAssigned_ShouldReturnFalse()
    {
        // Arrange
        var users = GetUser(Guid.NewGuid()).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(users[0].Id);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.HasRoleAssigned(string.Empty);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GivenValidClaimsInHttpContext_WhenInvokeHasPermissionAssigned_ShouldReturnTrue()
    {
        // Arrange
        var userId = Guid.NewGuid(); 
        var users = GetUser(userId).ToList();
        var permissions = GetPermissions().ToList();
        var userPermissions = new List<UserPermissions>
        {
            new()
            {
                UserId = userId,
                PermissionId = permissions[0].Id
            },
            new()
            {
                UserId = userId,
                PermissionId = permissions[1].Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.UserPermissions.AddRangeAsync(userPermissions);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(userId);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await userProvider.HasPermissionAssigned(Permissions.CanSelectArticles.ToString());

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidClaimsInHttpContextAndInvalidPermission_WhenInvokeHasPermissionAssigned_ShouldReturnFalse()
    {
        // Arrange
        var userId = Guid.NewGuid(); 
        var users = GetUser(userId).ToList();
        var permissions = GetPermissions().ToList();
        var userPermissions = new List<UserPermissions>
        {
            new()
            {
                UserId = userId,
                PermissionId = permissions[0].Id
            },
            new()
            {
                UserId = userId,
                PermissionId = permissions[1].Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.UserPermissions.AddRangeAsync(userPermissions);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(userId);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.HasPermissionAssigned(Permissions.CanAddLikes.ToString());

        // Assert
        result.Should().BeFalse();
    }
        
    [Fact]
    public async Task GivenNoUserClaimsInHttpContext_WhenInvokeHasPermissionAssigned_ShouldReturnFalse()
    {
        // Arrange
        var userId = Guid.NewGuid(); 
        var users = GetUser(userId).ToList();
        var permissions = GetPermissions().ToList();
        var userPermissions = new List<UserPermissions>
        {
            new()
            {
                UserId = userId,
                PermissionId = permissions[0].Id
            },
            new()
            {
                UserId = userId,
                PermissionId = permissions[1].Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.UserPermissions.AddRangeAsync(userPermissions);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(null);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        // Assert
        var result = await userProvider.HasPermissionAssigned(Permissions.CanSelectArticles.ToString());
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GivenValidClaimsInHttpContextAndNoPermission_WhenInvokeHasPermissionAssigned_ShouldReturnFalse()
    {
        // Arrange
        var userId = Guid.NewGuid(); 
        var users = GetUser(userId).ToList();
        var permissions = GetPermissions().ToList();
        var userPermissions = new List<UserPermissions>
        {
            new()
            {
                UserId = userId,
                PermissionId = permissions[0].Id
            },
            new()
            {
                UserId = userId,
                PermissionId = permissions[1].Id
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Permissions.AddRangeAsync(permissions);
        await databaseContext.UserPermissions.AddRangeAsync(userPermissions);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(userId);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userProvider.HasPermissionAssigned(string.Empty);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GivenValidClaimsInHttpContextWithNoIpAddress_WhenInvokeGetRequestIpAddress_ShouldSucceed()
    {
        // Arrange
        const string localhost = "127.0.0.1";
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.SaveChangesAsync();

        var httpContext = GetMockedHttpContext(userId);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = userProvider.GetRequestIpAddress();

        // Assert
        result.Should().Be(localhost);
    }

    [Fact]
    public async Task GivenValidClaimsInHttpContextWithIpAddress_WhenInvokeGetRequestIpAddress_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.SaveChangesAsync();

        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userProvider = new UserService(
            httpContext.Object, 
            databaseContext, 
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = userProvider.GetRequestIpAddress();

        // Assert
        result.Should().Be(ipAddress.ToString());
    }

    [Fact]
    public async Task GivenUser_WhenMakeClaimsIdentity_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();
        var userInfo = GetUserInfo(userId).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.UserInfo.AddRangeAsync(userInfo);
        await databaseContext.SaveChangesAsync();
            
        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userServiceProvider = new UserService(
            httpContext.Object, 
            databaseContext,
            mockedJwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userServiceProvider.MakeClaimsIdentity(users[0], CancellationToken.None);
            
        // Assert
        result.Claims.First(claim => claim.Type == ClaimTypes.Name).Value.Should().Be(users[0].UserAlias);
        result.Claims.First(claim => claim.Type == ClaimTypes.Role).Value.Should().Be(roles[0].Name);
        result.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value.Should().Be(users[0].Id.ToString());
        result.Claims.First(claim => claim.Type == ClaimTypes.GivenName).Value.Should().Be(userInfo[0].FirstName);
        result.Claims.First(claim => claim.Type == ClaimTypes.Surname).Value.Should().Be(userInfo[0].LastName);
        result.Claims.First(claim => claim.Type == ClaimTypes.Email).Value.Should().Be(users[0].EmailAddress);
    }

    [Fact]
    public async Task WhenGenerateUserToken_ShouldSucceed()
    {
        // Arrange
        var tokenExpires = DataUtilityService.GetRandomDateTime();
        var userToken = DataUtilityService.GetRandomString();
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();
        var roles = GetRole().ToList();
        var userRoles = new UserRoles
        {
            UserId = users[0].Id,
            RoleId = roles[0].Id
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.Roles.AddRangeAsync(roles);
        await databaseContext.UserRoles.AddRangeAsync(userRoles);
        await databaseContext.SaveChangesAsync();

        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var jwtUtilityService = new Mock<IWebTokenUtility>();
        jwtUtilityService
            .Setup(service => service
                .GenerateJwt(
                    It.IsAny<DateTime>(), 
                    It.IsAny<ClaimsIdentity>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    It.IsAny<string>()))
            .Returns(userToken);

        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfig = SetConfiguration();
        var userServiceProvider = new UserService(
            httpContext.Object, 
            databaseContext,
            jwtUtilityService.Object, 
            mockedDateTimeService.Object, 
            mockedConfig.Object);

        // Act
        var result = await userServiceProvider.GenerateUserToken(users[0], tokenExpires, CancellationToken.None);
            
        // Assert
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenManyRefreshTokens_WhenDeleteOutdatedRefreshTokens_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();
        var userRefreshTokens = new List<UserRefreshTokens>
        {
            new() // New token
            {
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(120),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
            new() // Old token
            {
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddDays(-6),
                Created = DateTimeService.Now.AddDays(-5),
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
            new() // Old token
            {
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(-360),
                Created = DateTimeService.Now.AddMinutes(-220),
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserRefreshTokens.AddRangeAsync(userRefreshTokens);
        await databaseContext.SaveChangesAsync();

        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedConfig = SetConfiguration();

        var userServiceProvider = new UserService(
            httpContext.Object, 
            databaseContext,
            mockedJwtUtilityService.Object, 
            DateTimeService, 
            mockedConfig.Object);

        // Act
        await userServiceProvider.DeleteOutdatedRefreshTokens(userId, true);

        // Assert
        var updatedUserRefreshTokens = databaseContext.UserRefreshTokens.ToList();
        updatedUserRefreshTokens.Count.Should().Be(1);
        updatedUserRefreshTokens[0].Token.Should().Be(userRefreshTokens[0].Token);
    }

    [Fact]
    public async Task GivenNewRefreshTokens_WhenDeleteOutdatedRefreshTokens_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();
        var userRefreshTokens = new List<UserRefreshTokens>
        {
            new()
            {
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(140),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
            new()
            {
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(110),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
            new()
            {
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(90),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserRefreshTokens.AddRangeAsync(userRefreshTokens);
        await databaseContext.SaveChangesAsync();

        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var mockedConfig = SetConfiguration();

        var userServiceProvider = new UserService(
            httpContext.Object, 
            databaseContext,
            mockedJwtUtilityService.Object, 
            DateTimeService, 
            mockedConfig.Object);

        // Act
        await userServiceProvider.DeleteOutdatedRefreshTokens(userId, true);

        // Assert
        var updatedUserRefreshTokens = databaseContext.UserRefreshTokens.ToList();
        updatedUserRefreshTokens.Count.Should().Be(3);
    }

    [Fact]
    public async Task GivenNewRefreshTokens_WhenReplaceRefreshToken_ShouldSucceed()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();
        var userRefreshTokens = new List<UserRefreshTokens>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(140),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(110),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(90),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserRefreshTokens.AddRangeAsync(userRefreshTokens);
        await databaseContext.SaveChangesAsync();

        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var refreshToken = new RefreshToken
        {
            Token = DataUtilityService.GetRandomString(),
            Expires = DateTimeService.Now.AddMinutes(120),
            Created = DateTimeService.Now,
            CreatedByIp = ipAddress.ToString()
        };

        mockedJwtUtilityService
            .Setup(service => service
                .GenerateRefreshToken(
                    It.IsAny<string>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
            .Returns(refreshToken);

        var mockedConfig = SetConfiguration();

        var userServiceProvider = new UserService(
            httpContext.Object, 
            databaseContext,
            mockedJwtUtilityService.Object, 
            DateTimeService, 
            mockedConfig.Object);

        var input = new ReplaceRefreshTokenInput
        {
            UserId = userId, 
            SavedUserRefreshTokens = userRefreshTokens[0], 
            RequesterIpAddress = ipAddress.ToString(), 
            SaveImmediately = true
        };

        // Act
        var result = await userServiceProvider.ReplaceRefreshToken(input);

        // Assert
        result.UserId.Should().Be(userId);
        result.Token.Should().Be(refreshToken.Token);
        result.Expires.Should().Be(refreshToken.Expires);
        result.Created.Should().Be(refreshToken.Created);
        result.CreatedByIp.Should().Be(refreshToken.CreatedByIp);
        result.Revoked.Should().BeNull();
        result.RevokedByIp.Should().BeNull();
        result.ReplacedByToken.Should().BeNull();
        result.ReasonRevoked.Should().BeNull();

        var savedUserRefreshToken = await databaseContext.UserRefreshTokens.FindAsync(userRefreshTokens[0].Id);
        savedUserRefreshToken.Revoked.Should().NotBeNull();
        savedUserRefreshToken.RevokedByIp.Should().NotBeNull();
        savedUserRefreshToken.ReplacedByToken.Should().NotBeNull();
        savedUserRefreshToken.ReasonRevoked.Should().NotBeNull();
    }

    [Fact]
    public async Task GivenCompromisedRefreshTokens_WhenRevokeDescendantRefreshTokens_ShouldSucceed()
    {
        // Arrange
        const string reasonRevoked = "Attempted reuse of revoked ancestor token";
        var userId = Guid.NewGuid();
        var users = GetUser(userId).ToList();
        var token = DataUtilityService.GetRandomString(255);
        var userRefreshTokens = new List<UserRefreshTokens>
        {
            new() // Already revoked
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(-310),
                Created = DateTimeService.Now.AddMinutes(-400),
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = DateTimeService.Now.AddMinutes(-120),
                RevokedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                ReplacedByToken = token,
                ReasonRevoked = "Replaced by new token"
            },
            new() // Outdated
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = token,
                Expires = DateTimeService.Now.AddMinutes(6),
                Created = DateTimeService.Now.AddMinutes(-5),
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
            new() // Active
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = DataUtilityService.GetRandomString(255),
                Expires = DateTimeService.Now.AddMinutes(120),
                Created = DateTimeService.Now,
                CreatedByIp = DataUtilityService.GetRandomIpAddress().ToString(),
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            },
        };
            
        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.UserRefreshTokens.AddRangeAsync(userRefreshTokens);
        await databaseContext.SaveChangesAsync();
            
        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();
        var refreshToken = new RefreshToken
        {
            Token = DataUtilityService.GetRandomString(),
            Expires = DateTimeService.Now.AddMinutes(120),
            Created = DateTimeService.Now,
            CreatedByIp = ipAddress.ToString()
        };

        mockedJwtUtilityService
            .Setup(service => service
                .GenerateRefreshToken(
                    It.IsAny<string>(), 
                    It.IsAny<int>(), 
                    It.IsAny<int>()))
            .Returns(refreshToken);

        var mockedConfig = SetConfiguration();

        var userServiceProvider = new UserService(
            httpContext.Object, 
            databaseContext,
            mockedJwtUtilityService.Object, 
            DateTimeService, 
            mockedConfig.Object);

        var input = new RevokeRefreshTokensInput
        {
            UserRefreshTokens = userRefreshTokens, 
            SavedUserRefreshTokens = userRefreshTokens[0], 
            RequesterIpAddress = ipAddress.ToString(), 
            Reason = reasonRevoked, 
            SaveImmediately = true
        };

        // Act
        await userServiceProvider.RevokeDescendantRefreshTokens(input, CancellationToken.None);

        // Assert
        var getRefreshTokens = databaseContext.UserRefreshTokens.FirstOrDefault(x => x.Token == token);
        getRefreshTokens!.ReasonRevoked.Should().Be(reasonRevoked);
    }

    [Fact]
    public async Task GivenValidRefreshToken_WhenRevokeUserRefreshToken_ShouldUpdateEntity()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var token = DataUtilityService.GetRandomString(100);
        const string reasonRevoked = "Revoked by Admin";

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

        var ipAddress = DataUtilityService.GetRandomIpAddress();
        var httpContext = GetMockedHttpContext(userId, ipAddress);
        var mockedJwtUtilityService = new Mock<IWebTokenUtility>();

        var mockedConfig = SetConfiguration();

        var userServiceProvider = new UserService(
            httpContext.Object, 
            databaseContext,
            mockedJwtUtilityService.Object, 
            DateTimeService, 
            mockedConfig.Object);

        var input = new RevokeRefreshTokenInput
        {
            UserRefreshTokens = userRefreshToken, 
            RequesterIpAddress = ipAddress.ToString(), 
            Reason = reasonRevoked, 
            ReplacedByToken = null, 
            SaveImmediately = true
        };

        // Act
        await userServiceProvider.RevokeRefreshToken(input, CancellationToken.None);

        // Assert
        var getRefreshTokens = databaseContext.UserRefreshTokens.ToList();
        getRefreshTokens[0].RevokedByIp.Should().Be(ipAddress.ToString());
        getRefreshTokens[0].ReasonRevoked.Should().Be(reasonRevoked);
        getRefreshTokens[0].ReplacedByToken.Should().BeNull();
    }

    private IEnumerable<Users> GetUser(Guid userId)
    {
        return new List<Users>
        {
            new()
            {
                Id = userId,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                CryptedPassword = DataUtilityService.GetRandomString()
            }
        };
    }

    private IEnumerable<UserInfo> GetUserInfo(Guid userId)
    {
        return new List<UserInfo>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                CreatedAt = DataUtilityService.GetRandomDateTime(),
            }
        };
    }

    private static IEnumerable<Backend.Domain.Entities.Roles> GetRole()
    {
        return new List<Backend.Domain.Entities.Roles> 
        {
            new()
            {
                Id = Guid.Parse("dbb74bc8-dd33-4c9f-9744-84ad4c37035b"),
                Name = nameof(Roles.EverydayUser),
                Description = "User"
            }
        };
    }

    private static IEnumerable<Backend.Domain.Entities.Permissions> GetPermissions()
    {
        return new List<Backend.Domain.Entities.Permissions>
        {
            new()
            {
                Id = Guid.Parse("dbb74bc8-dd33-4c9f-9744-84ad4c37035b"),
                Name = Permissions.CanSelectArticles.ToString()
            },
            new()
            {
                Id = Guid.Parse("76fb3d47-f10d-467e-9e68-61d8a9fc5f6d"),
                Name = Permissions.CanInsertArticles.ToString()
            }
        };   
    }

    private static Mock<IHttpContextAccessor> GetMockedHttpContext(Guid? userId, IPAddress? requesterIpAddress = default)
    {
        var ipAddress = requesterIpAddress == null
            ? StringValues.Empty 
            : new StringValues(requesterIpAddress.ToString());

        var claims = new List<Claim>();
        var headers = new HeaderDictionary { { "X-Forwarded-For", ipAddress } };
        var values = new StringValues(It.IsAny<string>());
        var mockedHttpContext = new Mock<IHttpContextAccessor>();
        var response = new Mock<HttpResponse>();

        if (userId != null && userId != Guid.Empty)
            claims.Add(new Claim(ClaimTypes.NameIdentifier, userId.ToString()!));

        response
            .Setup(httpResponse => httpResponse.Cookies.Append(It.IsAny<string>(), values));

        mockedHttpContext
            .SetupGet(context => context.HttpContext!.User.Claims)
            .Returns(claims);

        mockedHttpContext
            .SetupGet(context => context.HttpContext!.Request.Headers)
            .Returns(headers);

        mockedHttpContext
            .SetupGet(context => context.HttpContext!.Response)
            .Returns(response.Object);

        return mockedHttpContext;
    }
}