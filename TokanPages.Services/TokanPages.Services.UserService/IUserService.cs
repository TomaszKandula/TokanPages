namespace TokanPages.Services.UserService;

using System;
using System.Threading;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
using Backend.Domain.Entities;
using Backend.Shared.Dto.Users;

public interface IUserService
{
    string GetRequestIpAddress();

    public int GetRequestUserTimezoneOffset();

    Task LogHttpRequest(string handlerName);

    Task<Guid?> GetUserId();

    Task<GetUserDto> GetUser();

    Task<List<GetUserRoleDto>> GetUserRoles(Guid? userId);

    Task<List<GetUserPermissionDto>> GetUserPermissions(Guid? userId);

    Task<bool?> HasRoleAssigned(string userRoleName);

    Task<bool> HasRoleAssigned(Guid roleId, Guid? userId);

    Task<bool?> HasPermissionAssigned(string userPermissionName);

    Task<bool> HasPermissionAssigned(Guid permissionId, Guid? userId);

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