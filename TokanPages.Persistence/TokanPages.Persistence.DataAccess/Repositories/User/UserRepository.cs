using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Entities.Photography;
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
        var parameters = new
        {
            UserId = userId
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        return await connection.QuerySingleOrDefaultAsync<GetUserDetailsDto>(query, parameters);
    }

    public async Task<GetUserDetailsDto?> GetUserDetails(string email)
    {
        var query = UserDetailsQueryTemplate + " WHERE operation.Users.EmailAddress = @Email";
        var parameters = new
        {
            Email = email
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        return await connection.QuerySingleOrDefaultAsync<GetUserDetailsDto>(query, parameters);
    }

    public async Task<GetUserDetailsDto?> GetUserDetailsByActivationId(Guid activationId)
    {
        var query = UserDetailsQueryTemplate + " WHERE operation.Users.ActivationId = @ActivationId AND operation.Users.IsDeleted = 0";
        var parameters = new
        {
            ActivationId = activationId
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        return await connection.QuerySingleOrDefaultAsync<GetUserDetailsDto>(query, parameters);
    }

    public async Task<GetUserDetailsDto?> GetUserDetailsByResetId(Guid resetId)
    {
        var query = UserDetailsQueryTemplate + " WHERE operation.Users.ResetId = @ResetId AND operation.Users.IsDeleted = 0";
        var parameters = new
        {
            ResetId = resetId
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        return await connection.QuerySingleOrDefaultAsync<GetUserDetailsDto>(query, parameters);
    }

    public async Task<bool> IsEmailAddressAvailableForChange(Guid userId, string emailAddress)
    {
        const string query = @"
            SELECT 
                COUNT(*) AS Count 
            FROM 
                operation.Users 
            WHERE 
                operation.Users.EmailAddress = @EmailAddress 
            AND 
                operation.Users.Id <> @UserId
        ";

        var parameters = new
        {
            UserId = userId,
            EmailAddress = emailAddress
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var count = await connection.QuerySingleOrDefaultAsync<int>(query, parameters);
        return count == 0;
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

    public async Task UpdateUser(UpdateUserDto data)
    {
        var updateBy = new
        {
            UserAlias = data.UserAlias,
            EmailAddress = data.EmailAddress,
            IsActivated = data.IsActivated,
            IsVerified = data.IsVerified,
            ModifiedAt = _dateTimeService.Now
        };

        var filterBy = new
        {
            Id = data.UserId
        };

        await DbOperations.Update<Users.User>(updateBy, filterBy);
    }

    public async Task UpdateUserActivation(Guid userId, Guid activationId, DateTime expires)
    {
        var updateBy = new
        {
            ActivationId = activationId,
            ActivationIdEnds = expires,
            ModifiedAt = _dateTimeService.Now
        };

        var filterBy = new
        {
            Id = userId
        };

        await DbOperations.Update<Users.User>(updateBy, filterBy);
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

    public async Task UpdateUserInformation(UpdateUserInformationDto data)
    {
        var updateBy = new
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            UserAboutText = data.UserAboutText,
            UserImageName = data.UserImageName,
            UserVideoName = data.UserVideoName,
            ModifiedAt = _dateTimeService.Now
        };

        var filterBy = new
        {
            UserId = data.UserId
        };

        await DbOperations.Update<Users.UserInfo>(updateBy, filterBy);
    }

    public async Task UpdateSignupDetails(UpdateSignupDetailsDto data)
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

    public async Task UpdateUserPassword(Guid userId, string password)
    {
        var updateBy = new UpdateUserPasswordDto
        {
            CryptedPassword = password,
            ResetId = null,
            ResetIdEnds = null,
            ModifiedAt = _dateTimeService.Now
        };

        var filterBy = new
        {
            Id = userId
        };

        await DbOperations.Update<Users.User>(updateBy, filterBy);
    }

    public async Task ResetUserPassword(ResetUserPasswordDto data)
    {
        var updateBy = new
        {
            CryptedPassword = string.Empty,
            ResetId = data.ResetId,
            ResetIdEnds = _dateTimeService.Now.AddMinutes(data.ResetMaturity),
            ModifiedAt = _dateTimeService.Now
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

    public async Task UserSoftDelete(Guid userId)
    {
        var updateBy = new
        {
            IsDeleted = true
        };

        var filterBy = new
        {
            Id = userId
        };

        await DbOperations.Update<Users.User>(updateBy, filterBy);
    }

    // TODO: Optimize following method
    public async Task UserHardDelete(Guid userId)
    {
        await DbOperations.Delete<Users.UserToken>(new { UserId = userId });
        await DbOperations.Delete<Users.UserRefreshToken>(new { UserId = userId });
        await DbOperations.Delete<Users.UserRole>(new { UserId = userId });
        await DbOperations.Delete<Users.UserPermission>(new { UserId = userId });
        await DbOperations.Delete<Users.UserPaymentHistory>(new { UserId = userId });
        await DbOperations.Delete<Users.UserPayment>(new { UserId = userId });
        await DbOperations.Delete<Users.UserNote>(new { UserId = userId });
        await DbOperations.Delete<Users.UserInfo>(new { UserId = userId });
        await DbOperations.Delete<Users.UserCompany>(new { UserId = userId });
        await DbOperations.Delete<Users.UserBankAccount>(new { UserId = userId });
        await DbOperations.Delete<Photo>(new { UserId = userId });
        await DbOperations.Delete<IssuedInvoice>(new { UserId = userId });
        await DbOperations.Delete<BatchInvoice>(new { UserId = userId });
        await DbOperations.Delete<Article>(new { UserId = userId });
        await DbOperations.Delete<ArticleLike>(new { UserId = userId });
        await DbOperations.Delete<ArticleCount>(new { UserId = userId });
        await DbOperations.Delete<Album>(new { UserId = userId });
        await DbOperations.Delete<Users.User>(new { Id = userId });
    }

    public async Task<List<GetDefaultPermissionDto>> GetDefaultPermissions(string userRoleName)
    {
        const string query = @"
            SELECT 
                operation.DefaultPermissions.Id,
                operation.DefaultPermissions.RoleId,
                operation.Roles.Name AS RoleName,
                operation.DefaultPermissions.PermissionId,
                operation.Permissions.Name AS PermissionName
            FROM 
                operation.DefaultPermissions
            LEFT JOIN
                operation.Roles
            ON
                operation.DefaultPermissions.RoleId = operation.Roles.Id
            LEFT JOIN
                operation.Permissions
            ON
                operation.DefaultPermissions.PermissionId = operation.Permissions.Id
            WHERE
                operation.Roles.Name = @RoleName
        ";

        var parameters = new
        {
            RoleName = userRoleName
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var data = await connection.QueryAsync<GetDefaultPermissionDto>(query, parameters);
        var result = data.ToList();

        return result;
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

        var parameters = new
        {
            UserId = userId
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var data = await connection.QueryAsync<GetUserRoleDto>(query, parameters);
        var result = data.ToList();

        return result;
    }

    public async Task CreateUserRole(CreateUserRoleDto data)
    {
        var entity = new Users.UserRole
        {
            Id = Guid.NewGuid(),
            UserId = data.UserId,
            RoleId = data.RoleId,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await DbOperations.Insert(entity);        
    }

    public async Task<List<GetUserPermissionDto>> GetUserPermissions(Guid userId)
    {
        const string query = @"
            SELECT 
                operation.UserPermissions.UserId,
                operation.UserPermissions.PermissionId,
                operation.Permissions.Name as PermissionName
            FROM 
                operation.UserPermissions
            LEFT JOIN
                operation.Permissions
            ON
                operation.UserPermissions.PermissionId = operation.Permissions.Id
            WHERE 
                UserId = @UserId
        ";

        var parameters = new
        {
            UserId = userId
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var data = await connection.QueryAsync<GetUserPermissionDto>(query, parameters);
        var result = data.ToList();

        return result;
    }

    public async Task CreateUserPermissions(List<CreateUserPermissionDto> data)
    {
        var entities = new List<Users.UserPermission>();
        foreach (var item in data)
        {
            var entity = new Users.UserPermission
            {
                Id = Guid.NewGuid(),
                UserId = item.UserId,
                PermissionId = item.PermissionId,
                CreatedBy = Guid.Empty,
                CreatedAt = _dateTimeService.Now
            };
            
            entities.Add(entity);
        }

        await DbOperations.Insert(entities);
    }

    public async Task CreateUserToken(Guid userId, string token, DateTime expires, DateTime created, string createdByIp)
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

    public async Task<bool> DoesUserTokenExist(string token)
    {
        var filterBy = new
        {
            Token = token
        };

        var data = await DbOperations.Retrieve<Users.UserToken>(filterBy);
        var result = data.Any();

        return result;
    }

    public async Task<bool> DoesUserTokenExist(Guid userId, string token)
    {
        var filterBy = new
        {
            UserId = userId, Token = token
        };

        var data = await DbOperations.Retrieve<Users.UserToken>(filterBy);
        var result = data.Any();

        return result;
    }

    public async Task RemoveUserToken(Guid userId, string token)
    {
        var deleteBy =  new
        {
            UserId = userId,
            Token = token
        };

        await DbOperations.Delete<Users.UserToken>(deleteBy);
    }

    public async Task<GetUserRefreshTokenDto?> GetUserRefreshToken(string token)
    {
        var filterBy = new
        {
            Token = token
        };

        var result = await DbOperations.Retrieve<Users.UserRefreshToken>(filterBy);
        var data = result.SingleOrDefault();
        if (data == null)
            return null;

        return new GetUserRefreshTokenDto
        {
            Id =  data.Id,
            UserId = data.UserId,
            Token = data.Token,
            Expires = data.Expires,
            Created = data.Created,
            CreatedByIp = data.CreatedByIp,
            Revoked = data.Revoked,
            RevokedByIp = data.RevokedByIp,
            ReplacedByToken = data.ReplacedByToken,
            ReasonRevoked = data.ReasonRevoked
        };
    }

    public async Task<List<GetUserRefreshTokenDto>> GetUserRefreshTokens(Guid userId)
    {
        var filterBy = new
        {
            Id = userId
        };

        var tokens = await DbOperations.Retrieve<Users.UserRefreshToken>(filterBy);

        var result = new List<GetUserRefreshTokenDto>();
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

            result.Add(item);
        }

        return result;
    }

    public async Task CreateUserRefreshToken(Guid userId, string token, DateTime expires, DateTime created, string? createdByIp)
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

    public async Task RemoveUserRefreshToken(string token)
    {
        var deleteBy = new
        {
            Token = token
        };

        await DbOperations.Delete<Users.UserRefreshToken>(deleteBy);
    }

    public async Task RemoveUserRefreshTokens(HashSet<Guid> ids)
    {
        var uids = new HashSet<object>();
        foreach (var id in ids)
        {
            uids.Add(id);
        }

        await DbOperations.Delete<Users.UserRefreshToken>(uids);
    }

    public async Task<GetUserNoteDto?> GetUserNote(Guid userId, Guid userNoteId)
    {
        var filterBy = new
        {
            UserId = userId, 
            Id = userNoteId
        };

        var data = await DbOperations.Retrieve<Users.UserNote>(filterBy);
        var note = data.SingleOrDefault();
        if (note == null)
            return null;

        var result = new GetUserNoteDto
        {
            Id =  note.Id,
            Note = note.Note,
            CreatedAt = note.CreatedAt,
            CreatedBy = note.CreatedBy,
            ModifiedAt = note.ModifiedAt,
            ModifiedBy = note.ModifiedBy
        };

        return result;
    }

    public async Task<List<GetUserNoteDto>> GetUserNotes(Guid userId)
    {
        var filterBy = new
        {
            UserId = userId
        };

        var data = await DbOperations.Retrieve<Users.UserNote>(filterBy);
        var notes = data.ToList();

        var result = new List<GetUserNoteDto>();
        foreach (var note in notes)
        {
            var item = new GetUserNoteDto
            {
                Id = note.Id,
                Note = note.Note,
                CreatedAt = note.CreatedAt,
                CreatedBy = note.CreatedBy,
                ModifiedAt = note.ModifiedAt,
                ModifiedBy = note.ModifiedBy
            };

            result.Add(item);
        }

        return result;
    }

    public async Task CreateUserNote(Guid userId, string note, Guid? noteId = null, DateTime? createdAt = null)
    {
        var entity = new Users.UserNote
        {
            Id = noteId ?? Guid.NewGuid(),
            UserId = userId,
            Note = note,
            CreatedBy = userId,
            CreatedAt = createdAt ?? _dateTimeService.Now
        };

        await DbOperations.Insert(entity);
    }

    public async Task UpdateUserNote(Guid userId, string note)
    {
        var updateBy = new
        {
            Note = note,
            ModifiedAt = _dateTimeService.Now
        };

        var filterBy = new
        {
            UserId = userId
        };

        await DbOperations.Update<Users.UserNote>(updateBy, filterBy);
    }

    public async Task RemoveUserNote(Guid userId, Guid userNoteId)
    {
        var deleteBy = new
        {
            Id = userNoteId,
            UserId = userId
        };

        await DbOperations.Delete<Users.UserNote>(deleteBy);
    }

    public async Task ClearUserMedia(Guid userId)
    {
        var updateBy = new
        {
            UserImageName = string.Empty,
            UserVideoName = string.Empty
        };

        var filterBy = new
        {
            UserId = userId
        };

        await DbOperations.Update<Users.User>(updateBy, filterBy);
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
            operation.Users.ModifiedAt AS Modified
        FROM
            operation.Users
        LEFT JOIN
            operation.UserInformation
        ON
            operation.Users.Id = operation.UserInformation.UserId
    ";
}