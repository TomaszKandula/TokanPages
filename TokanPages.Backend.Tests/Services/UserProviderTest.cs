using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using TokanPages.Backend.Shared.Services.DataProviderService;
using Roles = TokanPages.Backend.Identity.Authorization.Roles;
using Permissions = TokanPages.Backend.Identity.Authorization.Permissions;

namespace TokanPages.Backend.Tests.Services
{
    public class UserProviderTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public UserProviderTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserId_ShouldReturnLoggedUserId()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUsers = GetOneUserInList(LLoggedUserId);
            var LHttpContext = GetMockedHttpContext(LLoggedUserId);
            
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
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUserId_ShouldReturnNull()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUsers = GetOneUserInList(LLoggedUserId);
            var LHttpContext = GetMockedHttpContext(null);
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);

            // Assert
            var LResult = await LUserProvider.GetUserId();
            LResult.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUserId_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"));
            var LHttpContext = GetMockedHttpContext(Guid.NewGuid());
            
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
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUser_ShouldReturnJsonObject()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUsers = GetOneUserInList(LLoggedUserId).ToList();
            var LHttpContext = GetMockedHttpContext(LLoggedUserId);
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.GetUser();
            
            // Assert
            LResult.UserId.Should().Be(LUsers[0].Id);
            LResult.AliasName.Should().Be(LUsers[0].UserAlias);
            LResult.AvatarName.Should().Be(LUsers[0].AvatarName);
            LResult.FirstName.Should().Be(LUsers[0].FirstName);
            LResult.LastName.Should().Be(LUsers[0].LastName);
            LResult.ShortBio.Should().Be(LUsers[0].ShortBio);
            LResult.Registered.Should().Be(LUsers[0].Registered);
        }
        
        [Fact]
        public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUser_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"));
            var LHttpContext = GetMockedHttpContext(Guid.NewGuid());
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUser);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }

        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUser_ShouldReturnNull()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"));
            var LHttpContext = GetMockedHttpContext(null);
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.GetUser();

            // Assert
            LResult.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldReturnJsonObject()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetOneRoleInList().ToList();
            var LUserRoles = new UserRoles
            {
                UserId = LUsers[0].Id,
                RoleId = LRoles[0].Id
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUsers[0].Id);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.GetUserRoles();

            // Assert
            LResult.Should().HaveCount(1);
            LResult[0].Name.Should().Be(LRoles[0].Name);
            LResult[0].Description.Should().Be(LRoles[0].Description);
        }

        [Fact]
        public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetOneRoleInList().ToList();
            var LUserRoles = new UserRoles
            {
                UserId = LUsers[0].Id,
                RoleId = LRoles[0].Id
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(Guid.NewGuid());
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserRoles);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetOneRoleInList().ToList();
            var LUserRoles = new UserRoles
            {
                UserId = LUsers[0].Id,
                RoleId = LRoles[0].Id
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(null);
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserRoles);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldReturnJsonObject()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetOneUserInList(LUserId);
            var LPermissions = GetTwoPermissions().ToList();
            var LUserPermissions = new List<UserPermissions>
            {
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[0].Id
                },
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.UserPermissions.AddRangeAsync(LUserPermissions);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUserId);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.GetUserPermissions();

            // Assert
            LResult.Should().HaveCount(2);
            LResult[0].Name.Should().Be(LPermissions[0].Name);
            LResult[1].Name.Should().Be(LPermissions[1].Name);
        }

        [Fact]
        public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetOneUserInList(LUserId);
            var LPermissions = GetTwoPermissions().ToList();
            var LUserPermissions = new List<UserPermissions>
            {
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[0].Id
                },
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.UserPermissions.AddRangeAsync(LUserPermissions);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(Guid.NewGuid());

            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserPermissions);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetOneUserInList(LUserId);
            var LPermissions = GetTwoPermissions().ToList();
            var LUserPermissions = new List<UserPermissions>
            {
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[0].Id
                },
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.UserPermissions.AddRangeAsync(LUserPermissions);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(null);
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserPermissions);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeHasRoleAssigned_ShouldReturnTrue()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetOneRoleInList().ToList();
            var LUserRoles = new UserRoles
            {
                UserId = LUsers[0].Id,
                RoleId = LRoles[0].Id
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUsers[0].Id);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.HasRoleAssigned(Roles.EverydayUser);

            // Assert
            LResult.Should().BeTrue();
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContextAndInvalidRole_WhenInvokeHasRoleAssigned_ShouldReturnFalse()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetOneRoleInList().ToList();
            var LUserRoles = new UserRoles
            {
                UserId = LUsers[0].Id,
                RoleId = LRoles[0].Id
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUsers[0].Id);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.HasRoleAssigned(Roles.PhotoPublisher);

            // Assert
            LResult.Should().BeFalse();
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeHasRoleAssigned_ShouldReturnNull()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetOneRoleInList().ToList();
            var LUserRoles = new UserRoles
            {
                UserId = LUsers[0].Id,
                RoleId = LRoles[0].Id
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(null);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);

            // Assert
            var LResult = await LUserProvider.HasRoleAssigned(Roles.EverydayUser);
            LResult.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContextAndNoRole_WhenInvokeHasRoleAssigned_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetOneUserInList(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetOneRoleInList().ToList();
            var LUserRoles = new UserRoles
            {
                UserId = LUsers[0].Id,
                RoleId = LRoles[0].Id
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Roles.AddRangeAsync(LRoles);
            await LDatabaseContext.UserRoles.AddRangeAsync(LUserRoles);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUsers[0].Id);
            
            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LUserProvider.HasRoleAssigned(string.Empty));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION));
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeHasPermissionAssigned_ShouldReturnTrue()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetOneUserInList(LUserId).ToList();
            var LPermissions = GetTwoPermissions().ToList();
            var LUserPermissions = new List<UserPermissions>
            {
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[0].Id
                },
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.UserPermissions.AddRangeAsync(LUserPermissions);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUserId);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.HasPermissionAssigned(Permissions.CanSelectArticles);

            // Assert
            LResult.Should().BeTrue();
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContextAndInvalidPermission_WhenInvokeHasPermissionAssigned_ShouldReturnFalse()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetOneUserInList(LUserId).ToList();
            var LPermissions = GetTwoPermissions().ToList();
            var LUserPermissions = new List<UserPermissions>
            {
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[0].Id
                },
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.UserPermissions.AddRangeAsync(LUserPermissions);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUserId);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await LUserProvider.HasPermissionAssigned(Permissions.CanAddLikes);

            // Assert
            LResult.Should().BeFalse();
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeHasPermissionAssigned_ShouldReturnNull()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetOneUserInList(LUserId).ToList();
            var LPermissions = GetTwoPermissions().ToList();
            var LUserPermissions = new List<UserPermissions>
            {
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[0].Id
                },
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.UserPermissions.AddRangeAsync(LUserPermissions);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(null);
            
            // Act
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);

            // Assert
            var LResult = await LUserProvider.HasPermissionAssigned(Permissions.CanSelectArticles);
            LResult.Should().BeNull();
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContextAndNoPermission_WhenInvokeHasPermissionAssigned_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetOneUserInList(LUserId).ToList();
            var LPermissions = GetTwoPermissions().ToList();
            var LUserPermissions = new List<UserPermissions>
            {
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[0].Id
                },
                new ()
                {
                    UserId = LUserId,
                    PermissionId = LPermissions[1].Id
                }
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.Permissions.AddRangeAsync(LPermissions);
            await LDatabaseContext.UserPermissions.AddRangeAsync(LUserPermissions);
            await LDatabaseContext.SaveChangesAsync();

            var LHttpContext = GetMockedHttpContext(LUserId);

            // Act
            // Assert
            var LUserProvider = new UserProvider(LHttpContext.Object, LDatabaseContext);
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LUserProvider.HasPermissionAssigned(string.Empty));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION));
        }
        
        private  IEnumerable<Users> GetOneUserInList(Guid AUserId)
        {
            return new List<Users>
            {
                new ()
                {
                    Id = AUserId,
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
        }

        private static IEnumerable<Domain.Entities.Roles> GetOneRoleInList()
        {
            return new List<Domain.Entities.Roles> 
            {
                new ()
                {
                    Id = Guid.Parse("dbb74bc8-dd33-4c9f-9744-84ad4c37035b"),
                    Name = Roles.EverydayUser,
                    Description = "User"
                }
            };
        }

        private static IEnumerable<Domain.Entities.Permissions> GetTwoPermissions()
        {
            return new List<Domain.Entities.Permissions>
            {
                new ()
                {
                    Id = Guid.Parse("dbb74bc8-dd33-4c9f-9744-84ad4c37035b"),
                    Name = Permissions.CanSelectArticles
                },
                new ()
                {
                    Id = Guid.Parse("76fb3d47-f10d-467e-9e68-61d8a9fc5f6d"),
                    Name = Permissions.CanInsertArticles
                }
            };   
        }

        private static Mock<IHttpContextAccessor> GetMockedHttpContext(Guid? AUserId)
        {
            var LHttpContext = new Mock<IHttpContextAccessor>();
            var LClaims = new List<Claim>();
            
            if (AUserId != null && AUserId != Guid.Empty)
                LClaims.Add(new Claim(Claims.UserId, AUserId.ToString()));

            LHttpContext
                .Setup(AHttpContext => AHttpContext.HttpContext.User.Claims)
                .Returns(LClaims);

            return LHttpContext;
        }
    }
}