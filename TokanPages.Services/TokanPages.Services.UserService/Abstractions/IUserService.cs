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

    Task<GetUserOutput?> GetUser();//TODO: to be removed

    Task<User> GetActiveUser(Guid? userId = null);

    Task<bool?> HasRoleAssigned(string userRoleName, Guid? userId = null);

    Task<bool?> HasPermissionAssigned(string userPermissionName, Guid? userId = null);

    Task<string> GenerateUserToken(Guid userId, DateTime tokenExpires);
}