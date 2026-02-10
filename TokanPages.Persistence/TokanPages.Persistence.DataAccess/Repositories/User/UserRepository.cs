using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User.Models;
using Users = TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Repositories.User;

public class UserRepository : RepositoryBase, IUserRepository
{
    private readonly IDateTimeService _dateTimeService;

    public UserRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;

    public async Task<Users.User?> GetUserById(Guid userId)
    {
        var filterBy = new { Id = userId };
        var data = await DbOperations.Retrieve<Users.User>(filterBy);
        return data.SingleOrDefault();
    }

    public async Task<Users.UserInfo?> GetUserInformationById(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = await DbOperations.Retrieve<Users.UserInfo>(filterBy);
        return data.SingleOrDefault();
    }

    public async Task<GetUserDetailsDto?> GetUserDetails(Guid userId)
    {
        const string query = @"
            SELECT
                operation.Users.[Id] AS UserId,
                operation.Users.[EmailAddress],
                operation.Users.[UserAlias],
                operation.UserInformation.[FirstName],
                operation.UserInformation.[LastName],
                operation.UserInformation.[UserAboutText],
                operation.UserInformation.[UserImageName],
                operation.UserInformation.[UserVideoName],
                operation.Users.[CryptedPassword],
                operation.Users.[ResetId],
                operation.Users.[ResetIdEnds],
                operation.Users.[ActivationId],
                operation.Users.[ActivationIdEnds],
                operation.Users.[HasBusinessLock],
                operation.Users.[IsActivated],
                operation.Users.[IsDeleted],
                operation.Users.[IsVerified],
                operation.Users.CreatedAt AS Registered
            FROM
                operation.Users
            LEFT JOIN
                operation.UserInformation
            ON
                operation.Users.Id = operation.UserInformation.UserId
            WHERE
                operation.Users.Id = @UserId
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserId = userId };
        var data = await db.QueryAsync<GetUserDetailsDto>(query, parameters);
        return data.SingleOrDefault();
    }

    public async Task<List<GetUserRoleDto>> GetUserRoles(Guid userId)
    {
        const string query = @"
            SELECT 
                operation.UserRoles.UserId,
                operation.UserRoles.RoleId,
                operation.Roles.Name AS RoleName,
                operation.Roles.Description
            FROM 
                operation.UserRoles
            LEFT JOIN
                operation.Roles
            ON
                operation.UserRoles.RoleId = operation.Roles.Id
            WHERE 
                UserId = @UserId
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserId = userId };
        var data = await db.QueryAsync<GetUserRoleDto>(query, parameters);
        return data.ToList();
    }

    public async Task<List<GetUserPermissionDto>> GetUserPermissions(Guid userId)
    {
        const string query = @"
            SELECT 
                operation.UserPermissions.UserId,
                operation.UserPermissions.PermissionId,
                operation.Permissions.Name
            FROM 
                operation.UserPermissions
            LEFT JOIN
                operation.Permissions
            ON
                operation.UserPermissions.PermissionId = operation.Permissions.Id
            WHERE 
                UserId = @UserId
        ";

        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserId = userId };
        var data = await db.QueryAsync<GetUserPermissionDto>(query, parameters);
        return data.ToList();
    }

    public async Task<GetUserRefreshTokenDto?> GetUserRefreshToken(string token)
    {
        var filterBy = new { Token = token };
        var data = await DbOperations.Retrieve<Users.UserRefreshToken>(filterBy);
        var dto = data.SingleOrDefault();
        if (dto == null)
            return null;

        return new GetUserRefreshTokenDto
        {
            Id =  dto.Id,
            UserId = dto.UserId,
            Token = dto.Token,
            Expires = dto.Expires,
            Created = dto.Created,
            CreatedByIp = dto.CreatedByIp,
            Revoked = dto.Revoked,
            RevokedByIp = dto.RevokedByIp,
            ReplacedByToken = dto.ReplacedByToken,
            ReasonRevoked = dto.ReasonRevoked
        };
    }

    public async Task<List<GetUserRefreshTokenDto>> GetUserRefreshTokens(Guid userId)
    {
        var filterBy = new { Id = userId };
        var tokens = await DbOperations.Retrieve<Users.UserRefreshToken>(filterBy);

        var dto = new List<GetUserRefreshTokenDto>();
        foreach (var token in tokens)
        {
            var item = new GetUserRefreshTokenDto
            {
                Id = token.Id,
                UserId = token.UserId,
                Token = token.Token,
                Expires = token.Expires,
                Created = token.Created,
                CreatedByIp = token.CreatedByIp,
                Revoked = token.Revoked,
                RevokedByIp = token.RevokedByIp,
                ReplacedByToken = token.ReplacedByToken,
                ReasonRevoked = token.ReasonRevoked,
            };

            dto.Add(item);
        }

        return dto;
    }

    public async Task InsertUserRefreshToken(Guid userId, string token, DateTime expires, DateTime created, string? createdByIp)
    {
        var entity = new Users.UserRefreshToken
        {
            Id = Guid.NewGuid(),
            UserId =  userId,
            Token = token,
            Expires = expires,
            Created = created,
            CreatedByIp = createdByIp,
            Revoked = null,
            RevokedByIp = null,
            ReplacedByToken = null,
            ReasonRevoked = null,
        };

        await DbOperations.Insert(entity);
    }

    public async Task DeleteUserRefreshToken(Guid id)
    {
        var deleteBy = new { Id = id };
        await DbOperations.Delete<Users.UserRefreshToken>(deleteBy);
    }

    public async Task DeleteUserRefreshTokens(HashSet<Guid> ids)
    {
        var uids = new HashSet<object>();
        foreach (var id in ids)
        {
            uids.Add(id);
        }

        await DbOperations.Delete<Users.UserRefreshToken>(uids);
    }

    public async Task InsertHttpRequest(string ipAddress, string handlerName)
    {
        var entity = new HttpRequest
        {
            Id = Guid.NewGuid(),
            SourceAddress = ipAddress,
            RequestedAt = _dateTimeService.Now,
            RequestedHandlerName = handlerName
        };

        await DbOperations.Insert(entity);
    }
}