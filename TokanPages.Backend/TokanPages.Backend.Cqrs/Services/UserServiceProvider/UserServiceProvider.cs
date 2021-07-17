namespace TokanPages.Backend.Cqrs.Services.UserServiceProvider
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using Shared.Models;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Shared.Dto.Users;
    using System.Threading;
    using Shared.Services.DateTimeService;
    using Identity.Services.JwtUtilityService;

    public class UserServiceProvider : IUserServiceProvider
    {
        private const string LOCALHOST = "127.0.0.1";

        private const string NEW_REFRESH_TOKEN_TEXT = "Replaced by new token";
        
        private readonly IHttpContextAccessor FHttpContextAccessor;

        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IJwtUtilityService FJwtUtilityService;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly IdentityServer FIdentityServer;

        private List<GetUserPermissionDto> FUserPermissions;

        private List<GetUserRoleDto> FUserRoles;
        
        private GetUserDto FUsers;
        
        public UserServiceProvider(IHttpContextAccessor AHttpContextAccessor, DatabaseContext ADatabaseContext, 
            IJwtUtilityService AJwtUtilityService, IDateTimeService ADateTimeService, IdentityServer AIdentityServer)
        {
            FHttpContextAccessor = AHttpContextAccessor;
            FDatabaseContext = ADatabaseContext;
            FJwtUtilityService = AJwtUtilityService;
            FDateTimeService = ADateTimeService;
            FIdentityServer = AIdentityServer;
        }

        public virtual string GetRequestIpAddress() 
        {
            var LRemoteIpAddress = FHttpContextAccessor.HttpContext?
                .Request.Headers["X-Forwarded-For"].ToString();
            
            return string.IsNullOrEmpty(LRemoteIpAddress) 
                ? LOCALHOST 
                : LRemoteIpAddress.Split(':')[0];
        }

        public string GetRefreshTokenCookie(string ACookieName)
        {
            if (string.IsNullOrEmpty(ACookieName))
                throw ArgumentNullException;

            return FHttpContextAccessor.HttpContext?.Request.Cookies[ACookieName];
        }

        public virtual void SetRefreshTokenCookie(string ARefreshToken, int AExpiresIn, bool AIsHttpOnly = true, 
            bool ASecure = true, string ACookieName = Constants.CookieNames.REFRESH_TOKEN)
        {
            if (string.IsNullOrEmpty(ARefreshToken))
                throw ArgumentNullException;

            if (AExpiresIn == 0)
                throw ArgumentZeroException;
            
            var LDateTimeOffset = new DateTimeOffset();
            var LExpires = LDateTimeOffset.UtcDateTime.AddMinutes(AExpiresIn);
            var LCookieOptions = new CookieOptions
            {
                HttpOnly = AIsHttpOnly,
                Expires = LExpires,
                Secure = ASecure
            };
            
            FHttpContextAccessor.HttpContext?.Response.Cookies
                .Append(ACookieName, ARefreshToken, LCookieOptions);
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

        public async Task<ClaimsIdentity> MakeClaimsIdentity(Users AUsers, CancellationToken ACancellationToken = default)
        {
            var LUserRoles = await FDatabaseContext.UserRoles
                .AsNoTracking()
                .Include(AUserRole => AUserRole.User)
                .Include(AUserRole => AUserRole.Role)
                .Where(AUserRole => AUserRole.UserId == AUsers.Id)
                .ToListAsync(ACancellationToken);
            
            var LClaimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, AUsers.UserAlias),
                new Claim(ClaimTypes.NameIdentifier, AUsers.Id.ToString()),
                new Claim(ClaimTypes.GivenName, AUsers.FirstName),
                new Claim(ClaimTypes.Surname, AUsers.LastName),
                new Claim(ClaimTypes.Email, AUsers.EmailAddress)
            });

            LClaimsIdentity.AddClaims(LUserRoles
                .Select(AUserRole => new Claim(ClaimTypes.Role, AUserRole.Role.Name)));

            return LClaimsIdentity;
        }

        public async Task<string> GenerateUserToken(Users AUsers, DateTime ATokenExpires, CancellationToken ACancellationToken = default)
        {
            var LClaimsIdentity = await MakeClaimsIdentity(AUsers, ACancellationToken);
            
            return FJwtUtilityService.GenerateJwt(
                ATokenExpires, 
                LClaimsIdentity, 
                FIdentityServer.WebSecret, 
                FIdentityServer.Issuer, 
                FIdentityServer.Audience);
        }

        public async Task DeleteOutdatedRefreshTokens(Guid AUserId, bool ASaveImmediately = false, CancellationToken ACancellationToken = default)
        {
            var LRefreshTokens = await FDatabaseContext.UserRefreshTokens
                .Where(ATokens => ATokens.UserId == AUserId 
                    && ATokens.Expires <= FDateTimeService.Now 
                    && ATokens.Created.AddMinutes(FIdentityServer.RefreshTokenExpiresIn) <= FDateTimeService.Now
                    && ATokens.Revoked == null)
                .ToListAsync(ACancellationToken);

            if (LRefreshTokens.Any())
                FDatabaseContext.UserRefreshTokens.RemoveRange(LRefreshTokens);
            
            if (ASaveImmediately && LRefreshTokens.Any())
                await FDatabaseContext.SaveChangesAsync(ACancellationToken);
        }        
        
        public async Task<UserRefreshTokens> ReplaceRefreshToken(Guid AUserId, UserRefreshTokens ASavedUserRefreshTokens, string ARequesterIpAddress, 
            bool ASaveImmediately = false, CancellationToken ACancellationToken = default)
        {
            var LNewRefreshToken = FJwtUtilityService.GenerateRefreshToken(ARequesterIpAddress, FIdentityServer.RefreshTokenExpiresIn);
            
            await RevokeRefreshToken(ASavedUserRefreshTokens, ARequesterIpAddress, NEW_REFRESH_TOKEN_TEXT, 
                LNewRefreshToken.Token, ASaveImmediately, ACancellationToken);

            return new UserRefreshTokens
            {
                UserId = AUserId,
                Token = LNewRefreshToken.Token,
                Expires = LNewRefreshToken.Expires,
                Created = LNewRefreshToken.Created,
                CreatedByIp = LNewRefreshToken.CreatedByIp,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };
        }
        
        public async Task RevokeDescendantRefreshTokens(IEnumerable<UserRefreshTokens> AUserRefreshTokens,  UserRefreshTokens ASavedUserRefreshTokens, 
            string ARequesterIpAddress, string AReason, bool ASaveImmediately = false, CancellationToken ACancellationToken = default)
        {
            if (string.IsNullOrEmpty(ASavedUserRefreshTokens.ReplacedByToken)) 
                return;

            var LUserRefreshTokens = AUserRefreshTokens.ToList();
            var LChildToken = LUserRefreshTokens.SingleOrDefault(ARefreshTokens => ARefreshTokens.Token == ASavedUserRefreshTokens.ReplacedByToken);
            if (IsRefreshTokenActive(LChildToken))
            {
                await RevokeRefreshToken(LChildToken, ARequesterIpAddress, AReason, null, ASaveImmediately, ACancellationToken);
            }
            else
            {
                await RevokeDescendantRefreshTokens(LUserRefreshTokens, ASavedUserRefreshTokens, ARequesterIpAddress, AReason, ASaveImmediately, ACancellationToken);
            }
        }

        public bool IsRefreshTokenExpired(UserRefreshTokens AUserRefreshTokens) 
            => AUserRefreshTokens.Expires <= FDateTimeService.Now;

        public bool IsRefreshTokenRevoked(UserRefreshTokens AUserRefreshTokens) 
            => AUserRefreshTokens.Revoked != null;

        public bool IsRefreshTokenActive(UserRefreshTokens AUserRefreshTokens) 
            => !IsRefreshTokenRevoked(AUserRefreshTokens) && !IsRefreshTokenExpired(AUserRefreshTokens);
        
        private static BusinessException AccessDeniedException 
            => new (nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        private static BusinessException ArgumentNullException
            => new (nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION), ErrorCodes.ARGUMENT_NULL_EXCEPTION);
        
        private static BusinessException ArgumentZeroException 
            => new (nameof(ErrorCodes.ARGUMENT_ZERO_EXCEPTION), ErrorCodes.ARGUMENT_ZERO_EXCEPTION);
        
        private async Task RevokeRefreshToken(UserRefreshTokens AUserRefreshTokens, string ARequesterIpAddress, string AReason = null, 
            string AReplacedByToken = null, bool ASaveImmediately = false, CancellationToken ACancellationToken = default)
        {
            AUserRefreshTokens.Revoked = FDateTimeService.Now;
            AUserRefreshTokens.RevokedByIp = ARequesterIpAddress;
            AUserRefreshTokens.ReasonRevoked = AReason;
            AUserRefreshTokens.ReplacedByToken = AReplacedByToken;

            FDatabaseContext.UserRefreshTokens.Update(AUserRefreshTokens);

            if (ASaveImmediately)
                await FDatabaseContext.SaveChangesAsync(ACancellationToken);
        }
        
        private Guid? UserIdFromClaim()
        {
            var LUserClaims = FHttpContextAccessor.HttpContext?.User.Claims ?? Array.Empty<Claim>();
            
            var LClaimsArray = LUserClaims as Claim[] ?? LUserClaims.ToArray();
            if (!LClaimsArray.Any())
                return null;
            
            var LUserIds = LClaimsArray
                .Where(AClaim => AClaim.Type.Contains(ClaimTypes.NameIdentifier))
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