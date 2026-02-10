using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Services.UserService;

internal sealed class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    private readonly IUserRepository _userRepository;

    private readonly IWebTokenUtility _webTokenUtility;

    private readonly IDateTimeService _dateTimeService;

    private readonly AppSettingsModel _appSettings;

    //TODO: refactor and remove below fields
    private List<GetUserPermissionsOutput>? _userPermissions;
    private List<GetUserRolesOutput>? _userRoles;
    private GetUserOutput? _user;

    public UserService(IHttpContextAccessor httpContextAccessor, IWebTokenUtility webTokenUtility, 
        IDateTimeService dateTimeService, IOptions<AppSettingsModel> configuration, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _webTokenUtility = webTokenUtility;
        _dateTimeService = dateTimeService;
        _userRepository = userRepository;
        _appSettings = configuration.Value;
    }

    public string GetRequestIpAddress() 
    {
        const string localhost = "127.0.0.1";
        var remoteIpAddress = _httpContextAccessor.HttpContext?
            .Request.Headers["X-Forwarded-For"].ToString();

        return string.IsNullOrEmpty(remoteIpAddress) 
            ? localhost 
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
            .Request.Headers["UserTimezoneOffset"].ToString();

        return string.IsNullOrEmpty(offset) 
            ? 0 
            : int.Parse(offset);
    }

    public string GetRequestUserLanguage()
    {
        var language = _httpContextAccessor.HttpContext?
            .Request.Headers["UserLanguage"].ToString();

        return string.IsNullOrWhiteSpace(language) 
            ? "en" 
            : language;
    }

    public async Task LogHttpRequest(string handlerName)
    {
        var ipAddress = GetRequestIpAddress();
        await _userRepository.InsertHttpRequest(ipAddress, handlerName);
    }

    public Guid GetLoggedUserId()
    {
        var userId = UserIdFromClaim() ?? Guid.Empty;
        return userId;
    }

    public async Task<GetUserOutput?> GetUser(CancellationToken cancellationToken = default)
    {
        await EnsureUserData();
        return _user;
    }

    public async Task<User> GetActiveUser(Guid? userId = default, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        var id = userId ?? UserIdFromClaim();
        if (id is null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var user = await _userRepository.GetUserById((Guid)id);
        if (user is null or { IsDeleted: true })
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        if (user is { HasBusinessLock: true })
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        if (user is { IsActivated: false })
            throw new AuthorizationException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

        return user;
    }

    public async Task<List<GetUserRolesOutput>?> GetUserRoles(Guid? userId, CancellationToken cancellationToken = default)
    {
        await EnsureUserRoles(userId);
        return _userRoles;
    }

    public async Task<List<GetUserPermissionsOutput>?> GetUserPermissions(Guid? userId, CancellationToken cancellationToken = default)
    {
        await EnsureUserPermissions(userId);
        return _userPermissions;
    }

    public async Task<bool?> HasRoleAssigned(string userRoleName, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();
        var userRolesById = await _userRepository.GetUserRoles(userId ?? Guid.Empty);
        var rolesByName = userRolesById.Where(role => role.RoleName == userRoleName);
        return rolesByName.Any();
    }

    public async Task<bool?> HasPermissionAssigned(string userPermissionName, Guid? userId = default, CancellationToken cancellationToken = default)
    {
        userId ??= UserIdFromClaim();
        var userPermissionsById = await _userRepository.GetUserPermissions(userId ?? Guid.Empty);
        var permissionsByName = userPermissionsById.Where(permission => permission.Name == userPermissionName);
        return permissionsByName.Any();
    }

    public async Task<string> GenerateUserToken(User user, DateTime tokenExpires)
    {
        var claimsIdentity = await MakeClaimsIdentity(user.Id);
        return _webTokenUtility.GenerateJwt(
            tokenExpires,
            claimsIdentity,
            _appSettings.IdsWebSecret,
            _appSettings.IdsIssuer,
            _appSettings.IdsAudience);
    }

    public async Task<UserRefreshToken> ReplaceRefreshToken(ReplaceRefreshTokenInput input, CancellationToken cancellationToken = default)
    {
        var newRefreshToken = _webTokenUtility.GenerateRefreshToken(input.RequesterIpAddress, 
            _appSettings.IdsRefreshTokenMaturity);

        var tokenInput = new RevokeRefreshTokenInput
        {
            UserRefreshTokens = input.SavedUserRefreshTokens, 
            RequesterIpAddress = input.RequesterIpAddress, 
            Reason = "Replaced by new token", 
            ReplacedByToken = newRefreshToken.Token, 
            SaveImmediately = input.SaveImmediately
        };

        await RevokeRefreshToken(tokenInput, cancellationToken);

        return new UserRefreshToken
        {
            UserId = input.UserId,
            Token = newRefreshToken.Token,
            Expires = newRefreshToken.Expires,
            Created = newRefreshToken.Created,
            CreatedByIp = newRefreshToken.CreatedByIp,
            Revoked = null,
            RevokedByIp = null,
            ReplacedByToken = null,
            ReasonRevoked = null,
            Id = Guid.NewGuid(),
        };
    }

    public async Task RevokeRefreshToken(RevokeRefreshTokenInput input, CancellationToken cancellationToken = default)
    {
        if (input.UserRefreshTokens is null) 
            return;

        input.UserRefreshTokens.Revoked = _dateTimeService.Now;
        input.UserRefreshTokens.RevokedByIp = input.RequesterIpAddress;
        input.UserRefreshTokens.ReasonRevoked = input.Reason;
        input.UserRefreshTokens.ReplacedByToken = input.ReplacedByToken;

        _operationDbContext.UserRefreshTokens.Update(input.UserRefreshTokens);
        await _operationDbContext.SaveChangesAsync(cancellationToken);
    }

    public bool IsRefreshTokenActive(UserRefreshToken userRefreshToken)
    {
        var isRefreshTokenRevoked = userRefreshToken.Revoked != null;
        var isRefreshTokenExpired = userRefreshToken.Expires <= _dateTimeService.Now;

        return !isRefreshTokenRevoked && !isRefreshTokenExpired;
    }

    private async Task<ClaimsIdentity> MakeClaimsIdentity(Guid userId)
    {
        var userDetails = await _userRepository.GetUserDetails(userId);
        if (userDetails is null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS));            

        var claimsIdentity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userDetails.UserAlias),
            new Claim(ClaimTypes.NameIdentifier, userDetails.UserId.ToString()),
            new Claim(ClaimTypes.GivenName,  userDetails.FirstName),
            new Claim(ClaimTypes.Surname, userDetails.LastName),
            new Claim(ClaimTypes.Email, userDetails.EmailAddress)
        });

        var userRoles = await _userRepository.GetUserRoles(userId);
        claimsIdentity.AddClaims(userRoles
            .Select(roles => new Claim(ClaimTypes.Role, roles.RoleName)));

        return claimsIdentity;
    }

    private Guid? UserIdFromClaim()
    {
        var userClaims = _httpContextAccessor.HttpContext?.User.Claims ?? Array.Empty<Claim>();
            
        var claimsArray = userClaims as Claim[] ?? userClaims.ToArray();
        if (claimsArray.Length == 0)
            return null;
            
        var userIds = claimsArray
            .Where(claim => claim.Type.Contains(ClaimTypes.NameIdentifier))
            .ToList();
            
        if (userIds.Count == 0)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
            
        return Guid.Parse(userIds.First().Value);
    }

    private async Task EnsureUserRoles(Guid? userId)
    {
        if (_userRoles != null)
            return;
            
        var getUserId = userId ?? UserIdFromClaim();
        var userRoles = await _userRepository.GetUserRoles(getUserId ?? Guid.Empty);
        if (userRoles.Count == 0)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        _userRoles = new List<GetUserRolesOutput>();
        foreach (var userRole in userRoles)
        {
            _userRoles.Add(new GetUserRolesOutput
            {
                Name = userRole.RoleName,
                Description = userRole.Description
            });
        }
    }

    private async Task EnsureUserPermissions(Guid? userId)
    {
        if (_userPermissions != null)
            return;

        var getUserId = userId ?? UserIdFromClaim();
        var userPermissions = await _userRepository.GetUserPermissions(getUserId ?? Guid.Empty);
        if (userPermissions.Count == 0)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        _userPermissions = new List<GetUserPermissionsOutput>();
        foreach (var userPermission in userPermissions)
        {
            _userPermissions.Add(new GetUserPermissionsOutput
            {
                Name = userPermission.Name
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

        var user = await _userRepository.GetUserById((Guid)userId);
        if (user is null)
        {
            _user = null;
            return;
        }

        var userInfo = await _userRepository.GetUserInformationById((Guid)userId);
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