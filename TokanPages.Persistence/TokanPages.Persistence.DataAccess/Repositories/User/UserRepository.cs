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