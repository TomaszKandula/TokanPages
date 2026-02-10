using TokanPages.Persistence.DataAccess.Repositories.User.Models;
using Users = TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public interface IUserRepository
{
    Task<Users.User?> GetUserById(Guid userId);

    Task<Users.UserInfo?> GetUserInformationById(Guid userId);

    Task<GetUserDetailsDto?> GetUserDetails(Guid userId);

    Task<List<GetUserRoleDto>> GetUserRoles(Guid userId);

    Task<List<GetUserPermissionDto>> GetUserPermissions(Guid userId);

    Task<List<GetUserRefreshTokenDto>> GetUserRefreshTokens(Guid userId);

    Task UpdateUserRefreshToken(string oldToken, string newToken, string reason, string ipAddress);
    
    Task DeleteUserRefreshTokens(Guid userId);

    Task InsertHttpRequest(string ipAddress, string handlerName);
}