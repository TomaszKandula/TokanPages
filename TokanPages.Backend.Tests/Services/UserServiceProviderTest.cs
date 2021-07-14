namespace TokanPages.Backend.Tests.Services
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Domain.Entities;
    using Core.Exceptions;
    using Shared.Resources;
    using Shared.Services.DateTimeService;
    using Cqrs.Services.UserServiceProvider;
    using Roles = Identity.Authorization.Roles;
    using Permissions = Identity.Authorization.Permissions;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class UserServiceProviderTest : TestBase
    {
        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserId_ShouldReturnLoggedUserId()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUsers = GetUser(LLoggedUserId);
            var LHttpContext = GetMockedHttpContext(LLoggedUserId);
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
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
            var LUsers = GetUser(LLoggedUserId);
            var LHttpContext = GetMockedHttpContext(null);
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);

            // Assert
            var LResult = await LUserProvider.GetUserId();
            LResult.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenInvalidClaimsInHttpContext_WhenInvokeGetUserId_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"));
            var LHttpContext = GetMockedHttpContext(Guid.NewGuid());
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            // Assert
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserId);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUser_ShouldReturnJsonObject()
        {
            // Arrange
            var LLoggedUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9");
            var LUsers = GetUser(LLoggedUserId).ToList();
            var LHttpContext = GetMockedHttpContext(LLoggedUserId);
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
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
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"));
            var LHttpContext = GetMockedHttpContext(Guid.NewGuid());
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            // Assert
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUser);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }

        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUser_ShouldReturnNull()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"));
            var LHttpContext = GetMockedHttpContext(null);
            
            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddRangeAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            // Act
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await LUserProvider.GetUser();

            // Assert
            LResult.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldReturnJsonObject()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetRole().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
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
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetRole().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserRoles);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUserRoles_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetRole().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserRoles);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldReturnJsonObject()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetUser(LUserId);
            var LPermissions = GetPermissions().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
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
            var LUsers = GetUser(LUserId);
            var LPermissions = GetPermissions().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserPermissions);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeGetUserPermissions_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetUser(LUserId);
            var LPermissions = GetPermissions().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(LUserProvider.GetUserPermissions);
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ACCESS_DENIED));
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeHasRoleAssigned_ShouldReturnTrue()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetRole().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await LUserProvider.HasRoleAssigned(nameof(Roles.EverydayUser));

            // Assert
            LResult.Should().BeTrue();
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContextAndInvalidRole_WhenInvokeHasRoleAssigned_ShouldReturnFalse()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetRole().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await LUserProvider.HasRoleAssigned(nameof(Roles.PhotoPublisher));

            // Assert
            LResult.Should().BeFalse();
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeHasRoleAssigned_ShouldReturnNull()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetRole().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);

            // Assert
            var LResult = await LUserProvider.HasRoleAssigned(nameof(Roles.EverydayUser));
            LResult.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenValidClaimsInHttpContextAndNoRole_WhenInvokeHasRoleAssigned_ShouldThrowError()
        {
            // Arrange
            var LUsers = GetUser(Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")).ToList();
            var LRoles = GetRole().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LUserProvider.HasRoleAssigned(string.Empty));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION));
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContext_WhenInvokeHasPermissionAssigned_ShouldReturnTrue()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetUser(LUserId).ToList();
            var LPermissions = GetPermissions().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await LUserProvider.HasPermissionAssigned(Permissions.CanSelectArticles.ToString());

            // Assert
            LResult.Should().BeTrue();
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContextAndInvalidPermission_WhenInvokeHasPermissionAssigned_ShouldReturnFalse()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetUser(LUserId).ToList();
            var LPermissions = GetPermissions().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await LUserProvider.HasPermissionAssigned(Permissions.CanAddLikes.ToString());

            // Assert
            LResult.Should().BeFalse();
        }
        
        [Fact]
        public async Task GivenNoUserClaimsInHttpContext_WhenInvokeHasPermissionAssigned_ShouldReturnNull()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetUser(LUserId).ToList();
            var LPermissions = GetPermissions().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);

            // Assert
            var LResult = await LUserProvider.HasPermissionAssigned(Permissions.CanSelectArticles.ToString());
            LResult.Should().BeNull();
        }

        [Fact]
        public async Task GivenValidClaimsInHttpContextAndNoPermission_WhenInvokeHasPermissionAssigned_ShouldThrowError()
        {
            // Arrange
            var LUserId = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"); 
            var LUsers = GetUser(LUserId).ToList();
            var LPermissions = GetPermissions().ToList();
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
            var LMockedDateTimeService = new Mock<IDateTimeService>();
            var LUserProvider = new UserServiceProvider(LHttpContext.Object, LDatabaseContext, LMockedDateTimeService.Object);
            var LResult = await Assert.ThrowsAsync<BusinessException>(() => LUserProvider.HasPermissionAssigned(string.Empty));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION));
        }
        
        private  IEnumerable<Users> GetUser(Guid AUserId)
        {
            return new List<Users>
            {
                new ()
                {
                    Id = AUserId,
                    EmailAddress = DataUtilityService.GetRandomEmail(),
                    UserAlias = DataUtilityService.GetRandomString(),
                    FirstName = DataUtilityService.GetRandomString(),
                    LastName = DataUtilityService.GetRandomString(),
                    IsActivated = true,
                    Registered = DataUtilityService.GetRandomDateTime(),
                    LastUpdated = null,
                    LastLogged = null,
                    CryptedPassword = DataUtilityService.GetRandomString()
                }
            };
        }

        private static IEnumerable<Domain.Entities.Roles> GetRole()
        {
            return new List<Domain.Entities.Roles> 
            {
                new ()
                {
                    Id = Guid.Parse("dbb74bc8-dd33-4c9f-9744-84ad4c37035b"),
                    Name = nameof(Roles.EverydayUser),
                    Description = "User"
                }
            };
        }

        private static IEnumerable<Domain.Entities.Permissions> GetPermissions()
        {
            return new List<Domain.Entities.Permissions>
            {
                new ()
                {
                    Id = Guid.Parse("dbb74bc8-dd33-4c9f-9744-84ad4c37035b"),
                    Name = Permissions.CanSelectArticles.ToString()
                },
                new ()
                {
                    Id = Guid.Parse("76fb3d47-f10d-467e-9e68-61d8a9fc5f6d"),
                    Name = Permissions.CanInsertArticles.ToString()
                }
            };   
        }

        private static Mock<IHttpContextAccessor> GetMockedHttpContext(Guid? AUserId)
        {
            var LHttpContext = new Mock<IHttpContextAccessor>();
            var LClaims = new List<Claim>();
            
            if (AUserId != null && AUserId != Guid.Empty)
                LClaims.Add(new Claim(ClaimTypes.NameIdentifier, AUserId.ToString()));

            LHttpContext
                .Setup(AHttpContext => AHttpContext.HttpContext.User.Claims)
                .Returns(LClaims);

            return LHttpContext;
        }
    }
}