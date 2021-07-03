using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Identity.Authorization;

namespace TokanPages.Backend.Cqrs.Services.UserProvider
{
    public class UserProvider : IUserProvider
    {
        private const string LOCALHOST = "127.0.0.1";
        
        private readonly IHttpContextAccessor FHttpContextAccessor;

        private readonly DatabaseContext FDatabaseContext;

        private List<UserPermissionDto> FUserPermissions;

        private List<UserRoleDto> FUserRoles;
        
        private GetUserDto FUsers;
        
        public UserProvider(IHttpContextAccessor AHttpContextAccessor, DatabaseContext ADatabaseContext)
        {
            FHttpContextAccessor = AHttpContextAccessor;
            FDatabaseContext = ADatabaseContext;
        }

        public UserProvider() { }

        public virtual string GetRequestIpAddress() 
        {
            var LRemoteIpAddress = FHttpContextAccessor.HttpContext?
                .Request.Headers["X-Forwarded-For"].ToString();
            
            return string.IsNullOrEmpty(LRemoteIpAddress) 
                ? LOCALHOST 
                : LRemoteIpAddress.Split(':')[0];
        }

        public virtual async Task<Guid?> GetUserId()
        {
            await EnsureUserData();
            return FUsers.UserId;
        }

        public virtual async Task<GetUserDto> GetUser()
        {
            await EnsureUserData();
            return FUsers;
        }

        public virtual async Task<List<UserRoleDto>> GetUserRoles()
        {
            await EnsureUserRoles();
            return FUserRoles;
        }

        public virtual async Task<List<UserPermissionDto>> GetUserPermissions()
        {
            await EnsureUserPermissions();
            return FUserPermissions;
        }

        public virtual async Task<bool> HasRoleAssigned(string AUserRoleName)
        {
            if (string.IsNullOrEmpty(AUserRoleName))
                throw ArgumentNullException;
            
            var LUserId = UserIdFromClaim();
            var LGivenRoles = await FDatabaseContext.UserRoles
                .AsNoTracking()
                .Include(AUserRoles => AUserRoles.Role)
                .Where(AUserRole => AUserRole.UserId == LUserId && AUserRole.Role.Name == AUserRoleName)
                .ToListAsync();

            return LGivenRoles.Any();
        }

        public virtual async Task<bool> HasPermissionAssigned(string AUserPermissionName)
        {
            if (string.IsNullOrEmpty(AUserPermissionName))
                throw ArgumentNullException;
            
            var LUserId = UserIdFromClaim();
            var LGivenPermissions = await FDatabaseContext.UserPermissions
                .AsNoTracking()
                .Include(AUserPermissions => AUserPermissions.Permission)
                .Where(AUserPermissions => AUserPermissions.UserId == LUserId && AUserPermissions.Permission.Name == AUserPermissionName)
                .ToListAsync();

            return LGivenPermissions.Any();
        }
        
        private static BusinessException AccessDeniedException 
            => new (nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        private static BusinessException ArgumentNullException
            => new (nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION), ErrorCodes.ARGUMENT_NULL_EXCEPTION);
        
        private Guid UserIdFromClaim()
        {
            var LUserClaims = FHttpContextAccessor.HttpContext?.User.Claims ?? throw AccessDeniedException;
            var LUserId = LUserClaims.First(AClaim => AClaim.Type.Contains(Claims.UserId)).Value;
            return Guid.Parse(LUserId);
        }

        private async Task EnsureUserRoles()
        {
            if (FUserRoles != null)
                return;
            
            var LUserRoles = await FDatabaseContext.UserRoles
                .AsNoTracking()
                .Include(AUserRoles => AUserRoles.Role)
                .Where(AUserRoles => AUserRoles.UserId == UserIdFromClaim())
                .ToListAsync();

            if (!LUserRoles.Any())
                throw AccessDeniedException;

            FUserRoles = new List<UserRoleDto>();
            foreach (var LItem in LUserRoles)
            {
                FUserRoles.Add(new UserRoleDto
                {
                    Name = LItem.Role.Name,
                    Description = LItem.Role.Description
                });
            }
        }

        private async Task EnsureUserPermissions()
        {
            if (FUserPermissions != null)
                return;
            
            var LUserPermissions = await FDatabaseContext.UserPermissions
                .AsNoTracking()
                .Include(AUserPermissions => AUserPermissions.Permission)
                .Where(AUserPermissions => AUserPermissions.UserId == UserIdFromClaim())
                .ToListAsync();

            if (!LUserPermissions.Any())
                throw AccessDeniedException;

            FUserPermissions = new List<UserPermissionDto>();
            foreach (var LItem in LUserPermissions)
            {
                FUserPermissions.Add(new UserPermissionDto
                {
                    Name = LItem.Permission.Name
                });
            }
        }

        private async Task EnsureUserData()
        {
            if (FUsers != null) 
                return;
            
            var LUsers = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.Id == UserIdFromClaim())
                .ToListAsync();

            if (!LUsers.Any())
                throw AccessDeniedException;

            FUsers = new GetUserDto
            {
                UserId = LUsers.First().Id,
                AliasName = LUsers.First().UserAlias,
                AvatarName = LUsers.First().AvatarName,
                FirstName = LUsers.First().FirstName,
                LastName = LUsers.First().LastName,
                ShortBio = LUsers.First().ShortBio,
                Registered = LUsers.First().Registered
            };
        }
    }
}
