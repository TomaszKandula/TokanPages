using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Services.UserService;

[SuppressMessage("Sonar Code Smell", "S3267:Loop should be simplified with LINQ expression", 
    Justification = "LINQ would actually just make things harder to understand")]
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

    private readonly IConfiguration _configuration;

    private List<GetUserPermissionsOutput>? _userPermissions;

    private List<GetUserRolesOutput>? _userRoles;

    private GetUserOutput? _user;

    public UserService(IHttpContextAccessor httpContextAccessor, DatabaseContext databaseContext, 
        IWebTokenUtility webTokenUtility, IDateTimeService dateTimeService, IConfiguration configuration)
    {
        _httpContextAccessor = httpContextAccessor;
        _databaseContext = databaseContext;
        _webTokenUtility = webTokenUtility;
        _dateTimeService = dateTimeService;
        _configuration = configuration;
    }

    public string GetRequestIpAddress() 
    {
        var remoteIpAddress = _httpContextAccessor.HttpContext?
            .Request.Headers[XForwardedFor].ToString();
            
        return string.IsNullOrEmpty(remoteIpAddress) 
            ? Localhost 
            : remoteIpAddress.Split(':')[0];
    }

    public bool GetCompactVideoFromHeader()
    {
        var value = _httpContextAccessor
            .HttpContext?
            .Request
            .Headers["compact-video"].ToString();

        if (string.IsNullOrWhiteSpace(value))
            return false;

        return Convert.ToBoolean(value);
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

    public async Task<GetUserOutput?> GetUser(CancellationToken cancellationToken = default)
    {
        await EnsureUserData(cancellationToken);
        return _user;
    }

    public async Task<Users> GetActiveUser(Guid? userId = default, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        var id = userId ?? UserIdFromClaim();
        var entity = isTracking ? _databaseContext.Users : _databaseContext.Users.AsNoTracking();
        var user = await entity
            .Where(users => !users.HasBusinessLock)
            .Where(users => users.IsActivated)
            .Where(users => !users.IsDeleted)
            .Where(users => users.Id == id)
            .SingleOrDefaultAsync(cancellationToken);

        if (user == null)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        if (!user.IsActivated)
            throw new AuthorizationException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

        if (user.IsDeleted)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        return user;
    }

    public async Task<List<GetUserRolesOutput>?> GetUserRoles(Guid? userId, CancellationToken cancellationToken = default)
    {
        await EnsureUserRoles(userId, cancellationToken);
        return _userRoles;
    }

    public async Task<List<GetUserPermissionsOutput>?> GetUserPermissions(Guid? userId, CancellationToken cancellationToken = default)
    {
        await EnsureUserPermissions(userId, cancellationToken);
        return _userPermissions;
    }

    public async Task<bool?> HasRoleAssigned(string userRoleName, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();

        var givenRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Where(roles => roles.UserId == userId && roles.Roles.Name == userRoleName)
            .ToListAsync(cancellationToken);

        return givenRoles.Any();
    }

    public async Task<bool> HasRoleAssigned(Guid roleId, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();
            
        var givenRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(userRoles => userRoles.Roles)
            .Where(userRoles => userRoles.UserId == userId && userRoles.Roles.Id == roleId)
            .ToListAsync(cancellationToken);

        return givenRoles.Any();
    }

    public async Task<bool?> HasPermissionAssigned(string userPermissionName, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();

        var givenPermissions = await _databaseContext.UserPermissions
            .AsNoTracking()
            .Include(permissions => permissions.Permissions)
            .Where(permissions => permissions.UserId == userId && permissions.Permissions.Name == userPermissionName)
            .ToListAsync(cancellationToken);

        return givenPermissions.Any();
    }

    public async Task<bool> HasPermissionAssigned(Guid permissionId, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();

        var givenPermissions = await _databaseContext.UserPermissions
            .AsNoTracking()
            .Include(userPermissions => userPermissions.Permissions)
            .Where(userPermissions => userPermissions.UserId == userId && userPermissions.Permissions.Id == permissionId)
            .ToListAsync(cancellationToken);

        return givenPermissions.Any();
    }

    public async Task<ClaimsIdentity> MakeClaimsIdentity(Users users, CancellationToken cancellationToken = default)
    {
        var userRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(roles => roles.Users)
            .Include(roles => roles.Roles)
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
            .Select(roles => new Claim(ClaimTypes.Role, roles.Roles.Name)));

        return claimsIdentity;
    }

    public async Task<string> GenerateUserToken(Users users, DateTime tokenExpires, CancellationToken cancellationToken = default)
    {
        var claimsIdentity = await MakeClaimsIdentity(users, cancellationToken);

        return _webTokenUtility.GenerateJwt(
            tokenExpires, 
            claimsIdentity, 
            _configuration.GetValue<string>("Ids_WebSecret"), 
            _configuration.GetValue<string>("Ids_Issuer"), 
            _configuration.GetValue<string>("Ids_Audience"));
    }

    public async Task DeleteOutdatedRefreshTokens(Guid userId, bool saveImmediately = false, CancellationToken cancellationToken = default)
    {
        var tokenMaturity = _configuration.GetValue<int>("Ids_RefreshToken_Maturity");
        var refreshTokens = await _databaseContext.UserRefreshTokens
            .Where(tokens => tokens.UserId == userId 
                             && tokens.Expires <= _dateTimeService.Now 
                             && tokens.Created.AddMinutes(tokenMaturity) <= _dateTimeService.Now
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
            _configuration.GetValue<int>("Ids_RefreshToken_Maturity"));

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
        if (input.UserRefreshTokens is null) 
            return;

        if (input.SavedUserRefreshTokens is null) 
            return;

        if (string.IsNullOrEmpty(input.SavedUserRefreshTokens.ReplacedByToken)) 
            return;

        var userRefreshTokensList = input.UserRefreshTokens.ToList();
        var childToken = userRefreshTokensList.SingleOrDefault(tokens => tokens.Token == input.SavedUserRefreshTokens.ReplacedByToken);

        if (childToken is null) 
            return;

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
        if (input.UserRefreshTokens is null) 
            return;

        input.UserRefreshTokens.Revoked = _dateTimeService.Now;
        input.UserRefreshTokens.RevokedByIp = input.RequesterIpAddress;
        input.UserRefreshTokens.ReasonRevoked = input.Reason;
        input.UserRefreshTokens.ReplacedByToken = input.ReplacedByToken;

        _databaseContext.UserRefreshTokens.Update(input.UserRefreshTokens);

        if (input.SaveImmediately)
            await _databaseContext.SaveChangesAsync(cancellationToken);
    }

    public bool IsRefreshTokenExpired(UserRefreshTokens userRefreshTokens)
    {
        return userRefreshTokens.Expires <= _dateTimeService.Now;
    }

    public bool IsRefreshTokenRevoked(UserRefreshTokens userRefreshTokens)
    {
        return userRefreshTokens.Revoked != null;
    }

    public bool IsRefreshTokenActive(UserRefreshTokens userRefreshTokens)
    {
        return!IsRefreshTokenRevoked(userRefreshTokens) && !IsRefreshTokenExpired(userRefreshTokens);
    }

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
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
            
        return Guid.Parse(userIds.First().Value);
    }

    private async Task EnsureUserRoles(Guid? userId, CancellationToken cancellationToken = default)
    {
        if (_userRoles != null)
            return;
            
        var getUserId = userId ?? UserIdFromClaim();
        var userRoles = await _databaseContext.UserRoles
            .AsNoTracking()
            .Include(roles => roles.Roles)
            .Where(roles => roles.UserId == getUserId)
            .ToListAsync(cancellationToken);

        if (!userRoles.Any())
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        _userRoles = new List<GetUserRolesOutput>();
        foreach (var userRole in userRoles)
        {
            _userRoles.Add(new GetUserRolesOutput
            {
                Name = userRole.Roles.Name,
                Description = userRole.Roles.Description
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
            .Include(permissions => permissions.Permissions)
            .Where(permissions => permissions.UserId == getUserId)
            .ToListAsync(cancellationToken);

        if (!userPermissions.Any())
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        _userPermissions = new List<GetUserPermissionsOutput>();
        foreach (var userPermission in userPermissions)
        {
            _userPermissions.Add(new GetUserPermissionsOutput
            {
                Name = userPermission.Permissions.Name
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

        _user = new GetUserOutput
        {
            UserId = user.Id,
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