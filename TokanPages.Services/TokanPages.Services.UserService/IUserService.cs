namespace TokanPages.Services.UserService;

using System;
using System.Threading;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
using Backend.Dto.Users;
using Backend.Domain.Entities;

public interface IUserService
{
    string GetRequestIpAddress();

    public int GetRequestUserTimezoneOffset();

    Task LogHttpRequest(string handlerName);

    Task<GetUserDto> GetUser(CancellationToken cancellationToken = default);

    Task<Users> GetActiveUser(Guid? userId = default, bool isTracking = false, CancellationToken cancellationToken = default);

    Task<List<GetUserRoleDto>> GetUserRoles(Guid? userId, CancellationToken cancellationToken = default);

    Task<List<GetUserPermissionDto>> GetUserPermissions(Guid? userId, CancellationToken cancellationToken = default);

    Task<bool?> HasRoleAssigned(string userRoleName, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<bool> HasRoleAssigned(Guid roleId, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<bool?> HasPermissionAssigned(string userPermissionName, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<bool> HasPermissionAssigned(Guid permissionId, Guid? userId = default, CancellationToken cancellationToken = default);

    Task<ClaimsIdentity> MakeClaimsIdentity(Users users, CancellationToken cancellationToken = default);
        
    Task<string> GenerateUserToken(Users users, DateTime tokenExpires, CancellationToken cancellationToken = default);

    Task DeleteOutdatedRefreshTokens(Guid userId, bool saveImmediately = false, CancellationToken cancellationToken = default);

    Task<UserRefreshTokens> ReplaceRefreshToken(ReplaceRefreshTokenInput input, CancellationToken cancellationToken = default);

    Task RevokeDescendantRefreshTokens(RevokeRefreshTokensInput input, CancellationToken cancellationToken = default);

    Task RevokeRefreshToken(RevokeRefreshTokenInput input, CancellationToken cancellationToken = default);

    bool IsRefreshTokenExpired(UserRefreshTokens userRefreshTokens);

    bool IsRefreshTokenRevoked(UserRefreshTokens userRefreshTokens);

    bool IsRefreshTokenActive(UserRefreshTokens userRefreshTokens);
}