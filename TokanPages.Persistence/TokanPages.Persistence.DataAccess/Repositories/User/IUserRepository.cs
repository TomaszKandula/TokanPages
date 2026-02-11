using TokanPages.Persistence.DataAccess.Repositories.User.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public interface IUserRepository
{
    Task<GetUserDetailsDto?> GetUserDetails(Guid userId);

    Task<GetUserDetailsDto?> GetUserDetails(string email);

    Task<GetUserDetailsDto?> GetUserDetailsByActivationId(Guid activationId);

    Task CreateUser();

    Task CreateUserInformation();

    Task ModifyRegistrationDetails(ModifySignupDetailsDto data);

    Task ActivateUser(Guid userId);

    Task<List<GetUserRoleDto>> GetUserRoles(Guid userId);

    Task<List<GetUserPermissionDto>> GetUserPermissions(Guid userId);

    Task InsertUserToken(Guid userId, string token, DateTime expires, DateTime created, string createdByIp);

    Task<GetUserRefreshTokenDto?> GetUserRefreshToken(string token);

    Task<List<GetUserRefreshTokenDto>> GetUserRefreshTokens(Guid userId);

    Task InsertUserRefreshToken(Guid userId, string token, DateTime expires, DateTime created, string? createdByIp);

    Task DeleteUserRefreshToken(string token);

    Task DeleteUserRefreshTokens(HashSet<Guid> ids);

    Task<GetUserNoteDto?> GetUserNote(Guid userId, Guid userNoteId);

    Task<List<GetUserNoteDto>> GetUserNotes(Guid userId);
}