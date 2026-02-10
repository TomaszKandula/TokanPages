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

    /// <inheritdoc/>
    public async Task<Users.User?> GetUserById(Guid userId)
    {
        var filterBy = new { Id = userId };
        var data = await DbOperations.Retrieve<Users.User>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
    public async Task<Users.UserInfo?> GetUserInformationById(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var data = await DbOperations.Retrieve<Users.UserInfo>(filterBy);
        return data.SingleOrDefault();
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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