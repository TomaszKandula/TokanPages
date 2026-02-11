using TokanPages.Services.UserService.Models;

namespace TokanPages.Services.UserService.Abstractions;

public interface IUserService
{
    string GetRequestIpAddress();

    bool GetCompactVideoFromHeader();

    public int GetRequestUserTimezoneOffset();

    public string GetRequestUserLanguage();

    Guid GetLoggedUserId();

    Task<GetActiveUserDto> GetActiveUser(Guid? userId = null);

    Task<bool?> HasRoleAssigned(string userRoleName, Guid? userId = null);

    Task<bool?> HasPermissionAssigned(string userPermissionName, Guid? userId = null);

    Task<string> GenerateUserToken(Guid userId, DateTime tokenExpires);
}