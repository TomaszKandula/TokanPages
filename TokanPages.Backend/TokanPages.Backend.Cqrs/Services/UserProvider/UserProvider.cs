﻿using System;
using System.Linq;
using System.Security.Claims;
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

        private List<GetUserPermissionDto> FUserPermissions;

        private List<GetUserRoleDto> FUserRoles;
        
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
            return FUsers?.UserId;
        }

        public virtual async Task<GetUserDto> GetUser()
        {
            await EnsureUserData();
            return FUsers;
        }

        public virtual async Task<List<GetUserRoleDto>> GetUserRoles()
        {
            await EnsureUserRoles();
            return FUserRoles;
        }

        public virtual async Task<List<GetUserPermissionDto>> GetUserPermissions()
        {
            await EnsureUserPermissions();
            return FUserPermissions;
        }

        public virtual async Task<bool?> HasRoleAssigned(string AUserRoleName)
        {
            if (string.IsNullOrEmpty(AUserRoleName))
                throw ArgumentNullException;
            
            var LUserId = UserIdFromClaim();
            if (LUserId == null)
                return null;
            
            var LGivenRoles = await FDatabaseContext.UserRoles
                .AsNoTracking()
                .Include(AUserRoles => AUserRoles.Role)
                .Where(AUserRole => AUserRole.UserId == LUserId && AUserRole.Role.Name == AUserRoleName)
                .ToListAsync();

            return LGivenRoles.Any();
        }

        public virtual async Task<bool?> HasPermissionAssigned(string AUserPermissionName)
        {
            if (string.IsNullOrEmpty(AUserPermissionName))
                throw ArgumentNullException;
            
            var LUserId = UserIdFromClaim();
            if (LUserId == null)
                return null;

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
        
        private Guid? UserIdFromClaim()
        {
            var LUserClaims = FHttpContextAccessor.HttpContext?.User.Claims ?? Array.Empty<Claim>();
            
            var LClaimsArray = LUserClaims as Claim[] ?? LUserClaims.ToArray();
            if (!LClaimsArray.Any())
                return null;
            
            var LUserIds = LClaimsArray
                .Where(AClaim => AClaim.Type.Contains(Claims.UserId))
                .ToList();
            
            if (!LUserIds.Any())
                throw AccessDeniedException;
            
            return Guid.Parse(LUserIds.First().Value);
        }

        private async Task EnsureUserRoles()
        {
            if (FUserRoles != null)
                return;
            
            var LUserId = UserIdFromClaim();
            var LUserRoles = await FDatabaseContext.UserRoles
                .AsNoTracking()
                .Include(AUserRoles => AUserRoles.Role)
                .Where(AUserRoles => AUserRoles.UserId == LUserId)
                .ToListAsync();

            if (!LUserRoles.Any())
                throw AccessDeniedException;

            FUserRoles = new List<GetUserRoleDto>();
            foreach (var LItem in LUserRoles)
            {
                FUserRoles.Add(new GetUserRoleDto
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
            
            var LUserId = UserIdFromClaim();
            var LUserPermissions = await FDatabaseContext.UserPermissions
                .AsNoTracking()
                .Include(AUserPermissions => AUserPermissions.Permission)
                .Where(AUserPermissions => AUserPermissions.UserId == LUserId)
                .ToListAsync();

            if (!LUserPermissions.Any())
                throw AccessDeniedException;

            FUserPermissions = new List<GetUserPermissionDto>();
            foreach (var LItem in LUserPermissions)
            {
                FUserPermissions.Add(new GetUserPermissionDto
                {
                    Name = LItem.Permission.Name
                });
            }
        }

        private async Task EnsureUserData()
        {
            if (FUsers != null) 
                return;
            
            var LUserId = UserIdFromClaim();
            if (LUserId == null)
            {
                FUsers = null;
                return;
            }

            var LUsers = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.Id == LUserId)
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
