namespace TokanPages.Backend.Cqrs.Services.UserProvider
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared.Dto.Users;

    public interface IUserProvider
    {
        string GetRequestIpAddress();

        Task<Guid?> GetUserId();
        
        Task<GetUserDto> GetUser();

        Task<List<GetUserRoleDto>> GetUserRoles();

        Task<List<GetUserPermissionDto>> GetUserPermissions();

        Task<bool?> HasRoleAssigned(string AUserRoleName);

        Task<bool?> HasPermissionAssigned(string AUserPermissionName);
    }
}