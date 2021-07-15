namespace TokanPages.Backend.Cqrs.Services.UserServiceProvider
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared.Dto.Users;

    public interface IUserServiceProvider
    {
        string GetRequestIpAddress();

        DateTimeOffset? SetRefreshTokenCookie(string ARefreshToken, int AExpiresIn, bool AIsHttpOnly = true);

        Task<Guid?> GetUserId();
        
        Task<GetUserDto> GetUser();

        Task<List<GetUserRoleDto>> GetUserRoles();

        Task<List<GetUserPermissionDto>> GetUserPermissions();

        Task<bool?> HasRoleAssigned(string AUserRoleName);

        Task<bool?> HasPermissionAssigned(string AUserPermissionName);
    }
}