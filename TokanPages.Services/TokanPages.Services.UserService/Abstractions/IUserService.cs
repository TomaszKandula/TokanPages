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

    Task<GetUserOutput?> GetUser(CancellationToken cancellationToken = default);//TODO: to be removed

    Task<User> GetActiveUser(Guid? userId = default, bool isTracking = false, CancellationToken cancellationToken = default);

    Task<bool?> HasRoleAssigned(string userRoleName, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<bool?> HasPermissionAssigned(string userPermissionName, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<string> GenerateUserToken(Guid userId, DateTime tokenExpires);

    bool IsRefreshTokenActive(DateTime expires);
}