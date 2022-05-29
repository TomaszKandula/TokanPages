﻿namespace TokanPages.Services.UserService;

using System;
using System.Linq;
using System.Threading;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Models;
using WebTokenService;
using Backend.Database;
using Backend.Dto.Users;
using Backend.Core.Exceptions;
using Backend.Domain.Entities;
using Backend.Shared.Services;
using Backend.Shared.Resources;
using Backend.Core.Utilities.DateTimeService;

public sealed class UserService : IUserService
{
    private const string Localhost = "127.0.0.1";

    private const string XForwardedFor = "X-Forwarded-For";

    private const string UserTimezoneOffset = "UserTimezoneOffset";

    private const string NewRefreshTokenText = "Replaced by new token";

    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly DatabaseContext _databaseContext;

    private readonly IWebTokenUtility _webTokenUtility;

    private readonly IDateTimeService _dateTimeService;

    private readonly IApplicationSettings _applicationSettings;

    private List<GetUserPermissionDto> _userPermissions;

    private List<GetUserRoleDto> _userRoles;

    private GetUserDto _user;

    public UserService(IHttpContextAccessor httpContextAccessor, DatabaseContext databaseContext, 
        IWebTokenUtility webTokenUtility, IDateTimeService dateTimeService, IApplicationSettings applicationSettings)
    {
        _httpContextAccessor = httpContextAccessor;
        _databaseContext = databaseContext;
        _webTokenUtility = webTokenUtility;
        _dateTimeService = dateTimeService;
        _applicationSettings = applicationSettings;
    }

    public string GetRequestIpAddress() 
    {
        var remoteIpAddress = _httpContextAccessor.HttpContext?
            .Request.Headers[XForwardedFor].ToString();
            
        return string.IsNullOrEmpty(remoteIpAddress) 
            ? Localhost 
            : remoteIpAddress.Split(':')[0];
    }

    public int GetRequestUserTimezoneOffset()
    {
        var offset = _httpContextAccessor.HttpContext?
            .Request.Headers[UserTimezoneOffset].ToString();

        return string.IsNullOrEmpty(offset) 
            ? 0 
            : int.Parse(offset);
    }

    public async Task LogHttpRequest(string handlerName)
    {
        var ipAddress = GetRequestIpAddress();
        var httpRequest = new HttpRequests
        {
            SourceAddress = ipAddress,
            RequestedAt = _dateTimeService.Now,
            RequestedHandlerName = handlerName
        };

        await _databaseContext.HttpRequests.AddAsync(httpRequest);
        await _databaseContext.SaveChangesAsync();
    }

    public async Task<Guid?> GetUserId(CancellationToken cancellationToken = default)
    {
        await EnsureUserData(cancellationToken);
        return _user?.UserId;
    }

    public async Task<GetUserDto> GetUser(CancellationToken cancellationToken = default)
    {
        await EnsureUserData(cancellationToken);
        return _user;
    }

    public async Task<GetUserDto> GetActiveUser(CancellationToken cancellationToken = default)
    {
        await EnsureUserData(cancellationToken);

        if (_user == null)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        if (!_user.IsActivated)
            throw new AuthorizationException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

        if (_user.IsDeleted)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        return _user;
    }

    public async Task<List<GetUserRoleDto>> GetUserRoles(Guid? userId, CancellationToken cancellationToken = default)
    {
        await EnsureUserRoles(userId, cancellationToken);
        return _userRoles;
    }

    public async Task<List<GetUserPermissionDto>> GetUserPermissions(Guid? userId, CancellationToken cancellationToken = default)
    {
        await EnsureUserPermissions(userId, cancellationToken);
        return _userPermissions;
    }

    public async Task<bool?> HasRoleAssigned(string userRoleName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(userRoleName))
            throw ArgumentNullException;
            
        var userId = UserIdFromClaim();
        if (userId == null)
            return null;
            
        var givenRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(roles => roles.RoleNavigation)
            .Where(roles => roles.UserId == userId && roles.RoleNavigation.Name == userRoleName)
            .ToListAsync(cancellationToken);

        return givenRoles.Any();
    }

    public async Task<bool> HasRoleAssigned(Guid roleId, Guid? userId, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();
            
        var givenRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(userRoles => userRoles.RoleNavigation)
            .Where(userRoles => userRoles.UserId == userId && userRoles.RoleNavigation.Id == roleId)
            .ToListAsync(cancellationToken);

        return givenRoles.Any();
    }

    public async Task<bool?> HasPermissionAssigned(string userPermissionName, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(userPermissionName))
            throw ArgumentNullException;
            
        var userId = UserIdFromClaim();
        if (userId == null)
            return null;

        var givenPermissions = await _databaseContext.UserPermissions
            .AsNoTracking()
            .Include(permissions => permissions.PermissionNavigation)
            .Where(permissions => permissions.UserId == userId && permissions.PermissionNavigation.Name == userPermissionName)
            .ToListAsync(cancellationToken);

        return givenPermissions.Any();
    }

    public async Task<bool> HasPermissionAssigned(Guid permissionId, Guid? userId, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();

        var givenPermissions = await _databaseContext.UserPermissions
            .AsNoTracking()
            .Include(userPermissions => userPermissions.PermissionNavigation)
            .Where(userPermissions => userPermissions.UserId == userId && userPermissions.PermissionNavigation.Id == permissionId)
            .ToListAsync(cancellationToken);

        return givenPermissions.Any();
    }

    public async Task<ClaimsIdentity> MakeClaimsIdentity(Users users, CancellationToken cancellationToken = default)
    {
        var userRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(roles => roles.UserNavigation)
            .Include(roles => roles.RoleNavigation)
            .Where(roles => roles.UserId == users.Id)
            .ToListAsync(cancellationToken);

        var userInfo = await _databaseContext.UserInfo
            .AsNoTracking()
            .Where(info => info.UserId == users.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, users.UserAlias),
            new Claim(ClaimTypes.NameIdentifier, users.Id.ToString()),
            new Claim(ClaimTypes.GivenName,  userInfo is null ? string.Empty : userInfo.FirstName),
            new Claim(ClaimTypes.Surname, userInfo is null ? string.Empty : userInfo.LastName),
            new Claim(ClaimTypes.Email, users.EmailAddress)
        });

        claimsIdentity.AddClaims(userRoles
            .Select(roles => new Claim(ClaimTypes.Role, roles.RoleNavigation.Name)));

        return claimsIdentity;
    }

    public async Task<string> GenerateUserToken(Users users, DateTime tokenExpires, CancellationToken cancellationToken = default)
    {
        var claimsIdentity = await MakeClaimsIdentity(users, cancellationToken);
            
        return _webTokenUtility.GenerateJwt(
            tokenExpires, 
            claimsIdentity, 
            _applicationSettings.IdentityServer.WebSecret, 
            _applicationSettings.IdentityServer.Issuer, 
            _applicationSettings.IdentityServer.Audience);
    }

    public async Task DeleteOutdatedRefreshTokens(Guid userId, bool saveImmediately = false, CancellationToken cancellationToken = default)
    {
        var refreshTokens = await _databaseContext.UserRefreshTokens
            .Where(tokens => tokens.UserId == userId 
                             && tokens.Expires <= _dateTimeService.Now 
                             && tokens.Created.AddMinutes(_applicationSettings.IdentityServer.RefreshTokenExpiresIn) <= _dateTimeService.Now
                             && tokens.Revoked == null)
            .ToListAsync(cancellationToken);

        if (refreshTokens.Any())
            _databaseContext.UserRefreshTokens.RemoveRange(refreshTokens);
            
        if (saveImmediately && refreshTokens.Any())
            await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserRefreshTokens> ReplaceRefreshToken(ReplaceRefreshTokenInput input, CancellationToken cancellationToken = default)
    {
        var newRefreshToken = _webTokenUtility.GenerateRefreshToken(input.RequesterIpAddress, 
            _applicationSettings.IdentityServer.RefreshTokenExpiresIn);

        var tokenInput = new RevokeRefreshTokenInput
        {
            UserRefreshTokens = input.SavedUserRefreshTokens, 
            RequesterIpAddress = input.RequesterIpAddress, 
            Reason = NewRefreshTokenText, 
            ReplacedByToken = newRefreshToken.Token, 
            SaveImmediately = input.SaveImmediately
        };

        await RevokeRefreshToken(tokenInput, cancellationToken);

        return new UserRefreshTokens
        {
            UserId = input.UserId,
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

    public async Task RevokeDescendantRefreshTokens(RevokeRefreshTokensInput input, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(input.SavedUserRefreshTokens.ReplacedByToken)) 
            return;

        var userRefreshTokensList = input.UserRefreshTokens.ToList();
        var childToken = userRefreshTokensList
            .SingleOrDefault(tokens => tokens.Token == input.SavedUserRefreshTokens.ReplacedByToken);

        if (IsRefreshTokenActive(childToken))
        {
            var tokenInput = new RevokeRefreshTokenInput
            {
                UserRefreshTokens = childToken, 
                RequesterIpAddress = input.RequesterIpAddress, 
                Reason = input.Reason, 
                ReplacedByToken = null, 
                SaveImmediately = input.SaveImmediately,
            };

            await RevokeRefreshToken(tokenInput, cancellationToken);
        }
        else
        {
            await RevokeDescendantRefreshTokens(input, cancellationToken);
        }
    }

    public async Task RevokeRefreshToken(RevokeRefreshTokenInput input, CancellationToken cancellationToken = default)
    {
        input.UserRefreshTokens.Revoked = _dateTimeService.Now;
        input.UserRefreshTokens.RevokedByIp = input.RequesterIpAddress;
        input.UserRefreshTokens.ReasonRevoked = input.Reason;
        input.UserRefreshTokens.ReplacedByToken = input.ReplacedByToken;

        _databaseContext.UserRefreshTokens.Update(input.UserRefreshTokens);

        if (input.SaveImmediately)
            await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public bool IsRefreshTokenExpired(UserRefreshTokens userRefreshTokens) 
        => userRefreshTokens.Expires <= _dateTimeService.Now;

    public bool IsRefreshTokenRevoked(UserRefreshTokens userRefreshTokens) 
        => userRefreshTokens.Revoked != null;

    public bool IsRefreshTokenActive(UserRefreshTokens userRefreshTokens) 
        => !IsRefreshTokenRevoked(userRefreshTokens) && !IsRefreshTokenExpired(userRefreshTokens);

    private static BusinessException ArgumentNullException
        => new (nameof(ErrorCodes.ARGUMENT_NULL_EXCEPTION), ErrorCodes.ARGUMENT_NULL_EXCEPTION);

    private static AccessException AccessDeniedException 
        => new (nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

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

    private async Task EnsureUserRoles(Guid? userId, CancellationToken cancellationToken = default)
    {
        if (_userRoles != null)
            return;
            
        var getUserId = userId ?? UserIdFromClaim();
        var userRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(roles => roles.RoleNavigation)
            .Where(roles => roles.UserId == getUserId)
            .ToListAsync(cancellationToken);

        if (!userRoles.Any())
            throw AccessDeniedException;

        _userRoles = new List<GetUserRoleDto>();
        foreach (var userRole in userRoles)
        {
            _userRoles.Add(new GetUserRoleDto
            {
                Name = userRole.RoleNavigation.Name,
                Description = userRole.RoleNavigation.Description
            });
        }
    }

    private async Task EnsureUserPermissions(Guid? userId, CancellationToken cancellationToken = default)
    {
        if (_userPermissions != null)
            return;
            
        var getUserId = userId ?? UserIdFromClaim();
        var userPermissions = await _databaseContext.UserPermissions
            .AsNoTracking()
            .Include(permissions => permissions.PermissionNavigation)
            .Where(permissions => permissions.UserId == getUserId)
            .ToListAsync(cancellationToken);

        if (!userPermissions.Any())
            throw AccessDeniedException;

        _userPermissions = new List<GetUserPermissionDto>();
        foreach (var userPermission in userPermissions)
        {
            _userPermissions.Add(new GetUserPermissionDto
            {
                Name = userPermission.PermissionNavigation.Name
            });
        }
    }

    private async Task EnsureUserData(CancellationToken cancellationToken = default)
    {
        if (_user != null) 
            return;

        var userId = UserIdFromClaim();
        if (userId == null)
        {
            _user = null;
            return;
        }

        var user = await _databaseContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(users => users.Id == userId, cancellationToken);

        if (user is null)
        {
            _user = null;
            return;
        }

        var userInfo = await _databaseContext.UserInfo
            .AsNoTracking()
            .SingleOrDefaultAsync(info => info.UserId == userId, cancellationToken);

        _user = new GetUserDto
        {
            UserId = user.Id,
            IsActivated = user.IsActivated,
            IsDeleted = user.IsDeleted,
            AliasName = user.UserAlias,
            Email = user.EmailAddress,
            Registered = user.CreatedAt,
            AvatarName = userInfo?.UserImageName,
            FirstName = userInfo?.FirstName,
            LastName = userInfo?.LastName,
            ShortBio = userInfo?.UserAboutText
        };
    }
}