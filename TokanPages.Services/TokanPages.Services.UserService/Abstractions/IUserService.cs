﻿using System.Security.Claims;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Services.UserService.Models;

namespace TokanPages.Services.UserService.Abstractions;

public interface IUserService
{
    string GetRequestIpAddress();

    bool GetCompactVideoFromHeader();

    public int GetRequestUserTimezoneOffset();

    Task LogHttpRequest(string handlerName);

    Guid GetLoggedUserId();

    Task<GetUserOutput?> GetUser(CancellationToken cancellationToken = default);

    Task<Users> GetActiveUser(Guid? userId = default, bool isTracking = false, CancellationToken cancellationToken = default);

    Task<List<GetUserRolesOutput>?> GetUserRoles(Guid? userId, CancellationToken cancellationToken = default);

    Task<List<GetUserPermissionsOutput>?> GetUserPermissions(Guid? userId, CancellationToken cancellationToken = default);

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