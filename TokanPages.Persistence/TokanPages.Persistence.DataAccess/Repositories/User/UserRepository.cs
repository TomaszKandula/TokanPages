using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
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

    public async Task<GetUserDetailsDto?> GetUserDetails(Guid userId)
    {
        var query = UserDetailsQueryTemplate + " WHERE operation.Users.Id = @UserId";
        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { UserId = userId };
        var data = await db.QueryAsync<GetUserDetailsDto>(query, parameters);
        return data.SingleOrDefault();
    }

    public async Task<GetUserDetailsDto?> GetUserDetails(string email)
    {
        var query = UserDetailsQueryTemplate + " WHERE operation.Users.EmailAddress = @Email";
        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { Email = email };
        var data = await db.QueryAsync<GetUserDetailsDto>(query, parameters);
        return data.SingleOrDefault();
    }

    public async Task<GetUserDetailsDto?> GetUserDetailsByActivationId(Guid activationId)
    {
        var query = UserDetailsQueryTemplate + " WHERE operation.Users.ActivationId = @ActivationId AND operation.Users.IsDeleted = 0";
        await using var db = new SqlConnection(AppSettings.DbDatabaseContext);
        var parameters = new { ActivationId = activationId };
        var data = await db.QueryAsync<GetUserDetailsDto>(query, parameters);
        return data.SingleOrDefault();
    }

    public async Task CreateUser(CreateUserDto data)
    {
        var entity = new Users.User
        {
            Id = data.UserId,
            UserAlias = data.UserAlias,
            EmailAddress = data.EmailAddress,
            CryptedPassword = data.CryptedPassword,
            CreatedBy = Guid.Empty,
            CreatedAt = _dateTimeService.Now,
            ActivationId = data.ActivationId,
            ActivationIdEnds = data.ActivationIdEnds,
            ResetId = null,
            IsActivated = false,
            IsVerified = false,
            IsDeleted = false,
            HasBusinessLock = false,
        };

        await DbOperations.Insert(entity);
    }

    public async Task CreateUserInformation(Guid userId, string firstName, string lastName, string avatarName)
    {
        var entity = new Users.UserInfo
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            FirstName = firstName,
            LastName = lastName,
            CreatedBy = Guid.Empty,
            CreatedAt = _dateTimeService.Now,
            UserImageName = avatarName,
        };

        await DbOperations.Insert(entity);
    }

    public async Task ModifyRegistrationDetails(ModifySignupDetailsDto data)
    {
        var updateBy = new
        {
            CryptedPassword = data.CryptedPassword,
            ActivationId = data.ActivationId,
            ActivationIdEnds = data.ActivationIdEnds
        };

        var filterBy = new
        {
            Id = data.UserId
        };

        await DbOperations.Update<Users.User>(updateBy, filterBy);
    }

    public async Task ActivateUser(Guid userId)
    {
        var updateBy = new ActivateUserDto
        {
            IsActivated = true,
            IsVerified = true,
            ActivationId = null,
            ActivationIdEnds = null,
            ModifiedAt = _dateTimeService.Now
        };

        var filterBy = new
        {
            Id = userId    
        };

        await DbOperations.Update<Users.User>(updateBy, filterBy);
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

    public async Task InsertUserToken(Guid userId, string token, DateTime expires, DateTime created, string createdByIp)
    {
        var entity = new Users.UserToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = token,
            Expires = expires,
            Created = created,
            CreatedByIp = createdByIp,
            Command = string.Empty,
            RevokedByIp = string.Empty,
            ReasonRevoked = string.Empty
        };

        await DbOperations.Insert(entity);
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

    public async Task DeleteUserRefreshToken(string token)
    {
        var deleteBy = new { Token = token };
        await DbOperations.Delete<Users.UserRefreshToken>(deleteBy);
    }

    public async Task DeleteUserRefreshTokens(HashSet<Guid> ids)
    {
        var uids = new HashSet<object>();
        foreach (var id in ids) { uids.Add(id); }
        await DbOperations.Delete<Users.UserRefreshToken>(uids);
    }

    public async Task<GetUserNoteDto?> GetUserNote(Guid userId, Guid userNoteId)
    {
        var filterBy = new
        {
            UserId = userId, 
            UserNoteId = userNoteId
        };

        var notes = await DbOperations.Retrieve<Users.UserNote>(filterBy);
        var userNote = notes.SingleOrDefault();
        if (userNote == null)
            return null;

        return new GetUserNoteDto
        {
            Id =  userNote.Id,
            Note = userNote.Note,
            CreatedAt = userNote.CreatedAt,
            CreatedBy = userNote.CreatedBy,
            ModifiedAt = userNote.ModifiedAt,
            ModifiedBy = userNote.ModifiedBy
        };
    }

    public async Task<List<GetUserNoteDto>> GetUserNotes(Guid userId)
    {
        var filterBy = new { UserId = userId };
        var notes = await DbOperations.Retrieve<Users.UserNote>(filterBy);
        var userNotes =  notes.ToList();

        var result = new List<GetUserNoteDto>();
        foreach (var userNote in userNotes)
        {
            var item = new GetUserNoteDto
            {
                Id = userNote.Id,
                Note = userNote.Note,
                CreatedAt = userNote.CreatedAt,
                CreatedBy = userNote.CreatedBy,
                ModifiedAt = userNote.ModifiedAt,
                ModifiedBy = userNote.ModifiedBy
            };

            result.Add(item);
        }

        return result;
    }

    private static string UserDetailsQueryTemplate => @"
        SELECT
            operation.Users.Id AS UserId,
            operation.Users.EmailAddress,
            operation.Users.UserAlias,
            operation.UserInformation.FirstName,
            operation.UserInformation.LastName,
            operation.UserInformation.UserAboutText,
            operation.UserInformation.UserImageName,
            operation.UserInformation.UserVideoName,
            operation.Users.CryptedPassword,
            operation.Users.ResetId,
            operation.Users.ResetIdEnds,
            operation.Users.ActivationId,
            operation.Users.ActivationIdEnds,
            operation.Users.HasBusinessLock,
            operation.Users.IsActivated,
            operation.Users.IsDeleted,
            operation.Users.IsVerified,
            operation.Users.CreatedAt AS Registered,
            operation.Users.ModifiedAt AS Modified,
        FROM
            operation.Users
        LEFT JOIN
            operation.UserInformation
        ON
            operation.Users.Id = operation.UserInformation.UserId
    ";
}