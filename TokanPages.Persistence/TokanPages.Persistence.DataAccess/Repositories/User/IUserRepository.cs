using TokanPages.Persistence.DataAccess.Repositories.User.Models;
using Users = TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public interface IUserRepository
{
    Task<Users.User?> GetUserById(Guid userId);//TODO: use DTO model

    Task<Users.UserInfo?> GetUserInformationById(Guid userId);//TODO: use DTO model

    Task<GetUserDetailsDto?> GetUserDetails(Guid userId);

    Task<List<GetUserRoleDto>> GetUserRoles(Guid userId);

    Task<List<GetUserPermissionDto>> GetUserPermissions(Guid userId);

    Task<GetUserRefreshTokenDto?> GetUserRefreshToken(string token);

    Task<List<GetUserRefreshTokenDto>> GetUserRefreshTokens(Guid userId);

    Task InsertUserRefreshToken(Guid userId, string token, DateTime expires, DateTime created, string? createdByIp);

    Task DeleteUserRefreshToken(Guid id);

    Task DeleteUserRefreshTokens(HashSet<Guid> ids);

    Task InsertHttpRequest(string ipAddress, string handlerName);
}