namespace TokanPages.Backend.Cqrs.Services.UserServiceProvider
{
    using System;
    using System.Threading;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared;
    using Domain.Entities;
    using Shared.Dto.Users;

    public interface IUserServiceProvider
    {
        string GetRequestIpAddress();

        string GetRefreshTokenCookie(string ACookieName);
        
        void SetRefreshTokenCookie(string ARefreshToken, int AExpiresIn, bool AIsHttpOnly = true, string ACookieName = Constants.CookieNames.REFRESH_TOKEN);

        Task<Guid?> GetUserId();
        
        Task<GetUserDto> GetUser();

        Task<List<GetUserRoleDto>> GetUserRoles();

        Task<List<GetUserPermissionDto>> GetUserPermissions();

        Task<bool?> HasRoleAssigned(string AUserRoleName);

        Task<bool?> HasPermissionAssigned(string AUserPermissionName);

        Task<ClaimsIdentity> MakeClaimsIdentity(Users AUsers, CancellationToken ACancellationToken = default);
        
        Task<string> GenerateUserToken(Users AUsers, DateTime ATokenExpires, CancellationToken ACancellationToken = default);

        Task DeleteOutdatedRefreshTokens(Guid AUserId, bool ASaveImmediately = false, CancellationToken ACancellationToken = default);

        UserRefreshTokens ReplaceRefreshToken(Guid AUserId, UserRefreshTokens ASavedUserRefreshTokens, string ARequesterIpAddress);

        void RevokeDescendantRefreshTokens(IEnumerable<UserRefreshTokens> AUserRefreshTokens, UserRefreshTokens ASavedUserRefreshTokens, string ARequesterIpAddress, string AReason);

        void RevokeRefreshToken(UserRefreshTokens AUserRefreshTokens, string ARequesterIpAddress, string AReason = null, string AReplacedByToken = null);

        bool IsRefreshTokenExpired(UserRefreshTokens AUserRefreshTokens);

        bool IsRefreshTokenRevoked(UserRefreshTokens AUserRefreshTokens);

        bool IsRefreshTokenActive(UserRefreshTokens AUserRefreshTokens);
    }
}