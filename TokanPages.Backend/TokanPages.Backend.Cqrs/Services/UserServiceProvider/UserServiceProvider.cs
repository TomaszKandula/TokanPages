namespace TokanPages.Backend.Cqrs.Services.UserServiceProvider
{
    using System;
    using System.Linq;
    using System.Threading;
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
    using Core.Utilities.DateTimeService;
    using Identity.Services.JwtUtilityService;

    public class UserServiceProvider : IUserServiceProvider
    {
        private const string Localhost = "127.0.0.1";

        private const string NewRefreshTokenText = "Replaced by new token";
        
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly DatabaseContext _databaseContext;
        
        private readonly IJwtUtilityService _jwtUtilityService;
        
        private readonly IDateTimeService _dateTimeService;
        
        private readonly IdentityServer _identityServer;

        private List<GetUserPermissionDto> _userPermissions;

        private List<GetUserRoleDto> _userRoles;
        
        private GetUserDto _user;
        
        public UserServiceProvider(IHttpContextAccessor httpContextAccessor, DatabaseContext databaseContext, 
            IJwtUtilityService jwtUtilityService, IDateTimeService dateTimeService, IdentityServer identityServer)
        {
            _httpContextAccessor = httpContextAccessor;
            _databaseContext = databaseContext;
            _jwtUtilityService = jwtUtilityService;
            _dateTimeService = dateTimeService;
            _identityServer = identityServer;
        }

        public virtual string GetRequestIpAddress() 
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext?
                .Request.Headers["X-Forwarded-For"].ToString();
            
            return string.IsNullOrEmpty(remoteIpAddress) 
                ? Localhost 
                : remoteIpAddress.Split(':')[0];
        }

        public virtual string GetRefreshTokenCookie(string cookieName)
        {
            if (string.IsNullOrEmpty(cookieName))
                throw ArgumentNullException;

            return _httpContextAccessor.HttpContext?.Request.Cookies[cookieName];
        }

        public virtual void SetRefreshTokenCookie(string refreshToken, int expiresIn, bool isHttpOnly = true, 
            bool secure = true, string cookieName = Constants.CookieNames.RefreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
                throw ArgumentNullException;

            if (expiresIn == 0)
                throw ArgumentZeroException;

            var expires = (DateTimeOffset)_dateTimeService.Now.AddMinutes(Math.Abs(expiresIn));
            var cookieOptions = new CookieOptions
            {
                HttpOnly = isHttpOnly,
                Expires = expires,
                Secure = secure
            };
            
            _httpContextAccessor.HttpContext?.Response.Cookies
                .Append(cookieName, refreshToken, cookieOptions);
        }

        public virtual async Task<Guid?> GetUserId()
        {
            await EnsureUserData();
            return _user?.UserId;
        }

        public virtual async Task<GetUserDto> GetUser()
        {
            await EnsureUserData();
            return _user;
        }

        public virtual async Task<List<GetUserRoleDto>> GetUserRoles(Guid? userId)
        {
            await EnsureUserRoles(userId);
            return _userRoles;
        }

        public virtual async Task<List<GetUserPermissionDto>> GetUserPermissions(Guid? userId)
        {
            await EnsureUserPermissions(userId);
            return _userPermissions;
        }

        public virtual async Task<bool?> HasRoleAssigned(string userRoleName)
        {
            if (string.IsNullOrEmpty(userRoleName))
                throw ArgumentNullException;
            
            var userId = UserIdFromClaim();
            if (userId == null)
                return null;
            
            var givenRoles = await _databaseContext.UserRoles
                .AsNoTracking()
                .Include(roles => roles.Role)
                .Where(roles => roles.UserId == userId && roles.Role.Name == userRoleName)
                .ToListAsync();

            return givenRoles.Any();
        }

        public virtual async Task<bool?> HasPermissionAssigned(string userPermissionName)
        {
            if (string.IsNullOrEmpty(userPermissionName))
                throw ArgumentNullException;
            
            var userId = UserIdFromClaim();
            if (userId == null)
                return null;

            var givenPermissions = await _databaseContext.UserPermissions
                .AsNoTracking()
                .Include(permissions => permissions.Permission)
                .Where(permissions => permissions.UserId == userId && permissions.Permission.Name == userPermissionName)
                .ToListAsync();

            return givenPermissions.Any();
        }

        public virtual async Task<ClaimsIdentity> MakeClaimsIdentity(Users users, CancellationToken cancellationToken = default)
        {
            var userRoles = await _databaseContext.UserRoles
                .AsNoTracking()
                .Include(roles => roles.User)
                .Include(roles => roles.Role)
                .Where(roles => roles.UserId == users.Id)
                .ToListAsync(cancellationToken);
            
            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, users.UserAlias),
                new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
                new Claim(ClaimTypes.GivenName, users.FirstName),
                new Claim(ClaimTypes.Surname, users.LastName),
                new Claim(ClaimTypes.Email, users.EmailAddress)
            });

            claimsIdentity.AddClaims(userRoles
                .Select(roles => new Claim(ClaimTypes.Role, roles.Role.Name)));

            return claimsIdentity;
        }

        public virtual async Task<string> GenerateUserToken(Users users, DateTime tokenExpires, CancellationToken cancellationToken = default)
        {
            var claimsIdentity = await MakeClaimsIdentity(users, cancellationToken);
            
            return _jwtUtilityService.GenerateJwt(
                tokenExpires, 
                claimsIdentity, 
                _identityServer.WebSecret, 
                _identityServer.Issuer, 
                _identityServer.Audience);
        }

        public virtual async Task DeleteOutdatedRefreshTokens(Guid userId, bool saveImmediately = false, CancellationToken cancellationToken = default)
        {
            var refreshTokens = await _databaseContext.UserRefreshTokens
                .Where(tokens => tokens.UserId == userId 
                    && tokens.Expires <= _dateTimeService.Now 
                    && tokens.Created.AddMinutes(_identityServer.RefreshTokenExpiresIn) <= _dateTimeService.Now
                    && tokens.Revoked == null)
                .ToListAsync(cancellationToken);

            if (refreshTokens.Any())
                _databaseContext.UserRefreshTokens.RemoveRange(refreshTokens);
            
            if (saveImmediately && refreshTokens.Any())
                await _databaseContext.SaveChangesAsync(cancellationToken);
        }        
        
        public virtual async Task<UserRefreshTokens> ReplaceRefreshToken(Guid userId, UserRefreshTokens savedUserRefreshTokens, string requesterIpAddress, 
            bool saveImmediately = false, CancellationToken cancellationToken = default)
        {
            var newRefreshToken = _jwtUtilityService.GenerateRefreshToken(requesterIpAddress, _identityServer.RefreshTokenExpiresIn);
            
            await RevokeRefreshToken(savedUserRefreshTokens, requesterIpAddress, NewRefreshTokenText, 
                newRefreshToken.Token, saveImmediately, cancellationToken);

            return new UserRefreshTokens
            {
                UserId = userId,
                Token = newRefreshToken.Token,
                Expires = newRefreshToken.Expires,
                Created = newRefreshToken.Created,
                CreatedByIp = newRefreshToken.CreatedByIp,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };
        }
        
        public virtual async Task RevokeDescendantRefreshTokens(IEnumerable<UserRefreshTokens> userRefreshTokens,  UserRefreshTokens savedUserRefreshTokens, 
            string requesterIpAddress, string reason, bool saveImmediately = false, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(savedUserRefreshTokens.ReplacedByToken)) 
                return;

            var userRefreshTokensList = userRefreshTokens.ToList();
            var childToken = userRefreshTokensList.SingleOrDefault(tokens => tokens.Token == savedUserRefreshTokens.ReplacedByToken);
            if (IsRefreshTokenActive(childToken))
            {
                await RevokeRefreshToken(childToken, requesterIpAddress, reason, null, saveImmediately, cancellationToken);
            }
            else
            {
                await RevokeDescendantRefreshTokens(userRefreshTokensList, savedUserRefreshTokens, requesterIpAddress, reason, saveImmediately, cancellationToken);
            }
        }

        public virtual async Task RevokeRefreshToken(UserRefreshTokens userRefreshTokens, string requesterIpAddress, string reason = null, 
            string replacedByToken = null, bool saveImmediately = false, CancellationToken cancellationToken = default)
        {
            userRefreshTokens.Revoked = _dateTimeService.Now;
            userRefreshTokens.RevokedByIp = requesterIpAddress;
            userRefreshTokens.ReasonRevoked = reason;
            userRefreshTokens.ReplacedByToken = replacedByToken;

            _databaseContext.UserRefreshTokens.Update(userRefreshTokens);

            if (saveImmediately)
                await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        public virtual bool IsRefreshTokenExpired(UserRefreshTokens userRefreshTokens) 
            => userRefreshTokens.Expires <= _dateTimeService.Now;

        public virtual bool IsRefreshTokenRevoked(UserRefreshTokens userRefreshTokens) 
            => userRefreshTokens.Revoked != null;

        public virtual bool IsRefreshTokenActive(UserRefreshTokens userRefreshTokens) 
            => !IsRefreshTokenRevoked(userRefreshTokens) && !IsRefreshTokenExpired(userRefreshTokens);
        
        private static BusinessException AccessDeniedException 
            => new (nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        private static BusinessException ArgumentNullException
            => new (nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION), ErrorCodes.ARGUMENT_NULL_EXCEPTION);
        
        private static BusinessException ArgumentZeroException 
            => new (nameof(ErrorCodes.ARGUMENT_ZERO_EXCEPTION), ErrorCodes.ARGUMENT_ZERO_EXCEPTION);
        
        private Guid? UserIdFromClaim()
        {
            var userClaims = _httpContextAccessor.HttpContext?.User.Claims ?? Array.Empty<Claim>();
            
            var claimsArray = userClaims as Claim[] ?? userClaims.ToArray();
            if (!claimsArray.Any())
                return null;
            
            var userIds = claimsArray
                .Where(claim => claim.Type.Contains(ClaimTypes.NameIdentifier))
                .ToList();
            
            if (!userIds.Any())
                throw AccessDeniedException;
            
            return Guid.Parse(userIds.First().Value);
        }

        private async Task EnsureUserRoles(Guid? userId)
        {
            if (_userRoles != null)
                return;
            
            var getUserId = userId ?? UserIdFromClaim();
            var userRoles = await _databaseContext.UserRoles
                .AsNoTracking()
                .Include(roles => roles.Role)
                .Where(roles => roles.UserId == getUserId)
                .ToListAsync();

            if (!userRoles.Any())
                throw AccessDeniedException;

            _userRoles = new List<GetUserRoleDto>();
            foreach (var userRole in userRoles)
            {
                _userRoles.Add(new GetUserRoleDto
                {
                    Name = userRole.Role.Name,
                    Description = userRole.Role.Description
                });
            }
        }

        private async Task EnsureUserPermissions(Guid? userId)
        {
            if (_userPermissions != null)
                return;
            
            var getUserId = userId ?? UserIdFromClaim();
            var userPermissions = await _databaseContext.UserPermissions
                .AsNoTracking()
                .Include(permissions => permissions.Permission)
                .Where(permissions => permissions.UserId == getUserId)
                .ToListAsync();

            if (!userPermissions.Any())
                throw AccessDeniedException;

            _userPermissions = new List<GetUserPermissionDto>();
            foreach (var userPermission in userPermissions)
            {
                _userPermissions.Add(new GetUserPermissionDto
                {
                    Name = userPermission.Permission.Name
                });
            }
        }

        private async Task EnsureUserData()
        {
            if (_user != null) 
                return;
            
            var userId = UserIdFromClaim();
            if (userId == null)
            {
                _user = null;
                return;
            }

            var users = await _databaseContext.Users
                .AsNoTracking()
                .Where(users => users.Id == userId)
                .ToListAsync();

            if (!users.Any())
                throw AccessDeniedException;

            _user = new GetUserDto
            {
                UserId = users.First().Id,
                AliasName = users.First().UserAlias,
                AvatarName = users.First().AvatarName,
                FirstName = users.First().FirstName,
                LastName = users.First().LastName,
                ShortBio = users.First().ShortBio,
                Registered = users.First().Registered
            };
        }
    }
}