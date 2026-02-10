using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Services.UserService.Models;

namespace TokanPages.Services.UserService.Abstractions;

public interface IUserService
{
    string GetRequestIpAddress();

    bool GetCompactVideoFromHeader();

    public int GetRequestUserTimezoneOffset();

    public string GetRequestUserLanguage();

    Task LogHttpRequest(string handlerName);//TODO: to be removed

    Guid GetLoggedUserId();

    Task<GetUserOutput?> GetUser(CancellationToken cancellationToken = default);

    Task<User> GetActiveUser(Guid? userId = default, bool isTracking = false, CancellationToken cancellationToken = default);

    Task<List<GetUserRolesOutput>?> GetUserRoles(Guid? userId, CancellationToken cancellationToken = default);

    Task<List<GetUserPermissionsOutput>?> GetUserPermissions(Guid? userId, CancellationToken cancellationToken = default);

    Task<bool?> HasRoleAssigned(string userRoleName, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<bool?> HasPermissionAssigned(string userPermissionName, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<string> GenerateUserToken(User user, DateTime tokenExpires, CancellationToken cancellationToken = default);

    Task DeleteOutdatedRefreshTokens(Guid userId, bool saveImmediately = false, CancellationToken cancellationToken = default);

    Task<UserRefreshToken> ReplaceRefreshToken(ReplaceRefreshTokenInput input, CancellationToken cancellationToken = default);

    Task RevokeDescendantRefreshTokens(RevokeRefreshTokensInput input, CancellationToken cancellationToken = default);

    Task RevokeRefreshToken(RevokeRefreshTokenInput input, CancellationToken cancellationToken = default);

    bool IsRefreshTokenExpired(UserRefreshToken userRefreshToken);//TODO: to be removed

    bool IsRefreshTokenRevoked(UserRefreshToken userRefreshToken);

    bool IsRefreshTokenActive(UserRefreshToken userRefreshToken);
}