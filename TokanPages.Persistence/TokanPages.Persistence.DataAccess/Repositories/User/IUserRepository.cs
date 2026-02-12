using TokanPages.Persistence.DataAccess.Repositories.User.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public interface IUserRepository
{
    Task<GetUserDetailsDto?> GetUserDetails(Guid userId);

    Task<GetUserDetailsDto?> GetUserDetails(string email);

    Task<GetUserDetailsDto?> GetUserDetailsByActivationId(Guid activationId);

    Task CreateUser(CreateUserDto data);

    Task CreateUserInformation(Guid userId, string firstName, string lastName, string avatarName);

    Task UpdateSignupDetails(UpdateSignupDetailsDto data);

    Task ResetUserPassword(UpdateUserPasswordDto data);
    
    Task ActivateUser(Guid userId);

    Task UserSoftDelete(Guid userId);

    Task UserHardDelete(Guid userId);

    Task<List<GetDefaultPermissionDto>> GetDefaultPermissions(string userRoleName);

    Task<List<GetUserRoleDto>> GetUserRoles(Guid userId);

    Task CreateUserRole(CreateUserRoleDto data);

    Task<List<GetUserPermissionDto>> GetUserPermissions(Guid userId);

    Task CreateUserPermissions(List<CreateUserPermissionDto> data);

    Task CreateUserToken(Guid userId, string token, DateTime expires, DateTime created, string createdByIp);

    Task<bool> DoesUserTokenExist(Guid userId, string token);
    
    Task DeleteUserToken(Guid userId, string token);

    Task<GetUserRefreshTokenDto?> GetUserRefreshToken(string token);

    Task<List<GetUserRefreshTokenDto>> GetUserRefreshTokens(Guid userId);

    Task CreateUserRefreshToken(Guid userId, string token, DateTime expires, DateTime created, string? createdByIp);

    Task DeleteUserRefreshToken(string token);

    Task DeleteUserRefreshTokens(HashSet<Guid> ids);

    Task<GetUserNoteDto?> GetUserNote(Guid userId, Guid userNoteId);

    Task<List<GetUserNoteDto>> GetUserNotes(Guid userId);

    Task RemoveUserNote(Guid userId, Guid userNoteId);

    Task ClearUserMedia(Guid userId);
}