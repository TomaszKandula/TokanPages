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
        var userId = UserIdFromClaim();
        if (userId == null)
            return null;

        var userDetails = await _userRepository.GetUserDetails((Guid)userId);
        if (userDetails is null)
            return null;

        return new GetUserOutput
        {
            UserId = userDetails.UserId,
            AliasName = userDetails.UserAlias,
            Email = userDetails.EmailAddress,
            Registered = userDetails.Registered,
            AvatarName = userDetails.UserImageName,
            FirstName = userDetails.FirstName,
            LastName = userDetails.LastName,
            ShortBio = userDetails.UserAboutText
        };
    }

    public async Task<User> GetActiveUser(Guid? userId = default, bool isTracking = false, CancellationToken cancellationToken = default)
    {
        var id = userId ?? UserIdFromClaim();
        if (id is null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var user = await _userRepository.GetUserDetails((Guid)id);
        if (user is null or { IsDeleted: true })
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        if (user is { HasBusinessLock: true })
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        if (user is { IsActivated: false })
            throw new AuthorizationException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

        //TODO: change to DTO
        return new User
        {
            UserAlias = user.UserAlias,
            EmailAddress = user.EmailAddress,
            CryptedPassword = user.CryptedPassword,
            ResetId = user.ResetId,
            CreatedBy = Guid.Empty,
            CreatedAt = default,
            IsActivated = user.IsActivated,
            IsVerified = user.IsVerified,
            IsDeleted = user.IsDeleted,
            HasBusinessLock = user.HasBusinessLock,
            Id = user.UserId
        };
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

    public async Task<string> GenerateUserToken(Guid userId, DateTime tokenExpires)
    {
        var claimsIdentity = await MakeClaimsIdentity(userId);
        return _webTokenUtility.GenerateJwt(
            tokenExpires,
            claimsIdentity,
            _appSettings.IdsWebSecret,
            _appSettings.IdsIssuer,
            _appSettings.IdsAudience);
    }

    public bool IsRefreshTokenActive(DateTime expires) => !(expires <= _dateTimeService.Now);

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
}