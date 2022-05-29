namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Database;
using Domain.Entities;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using MediatR;

public class RemoveUserCommandHandler : Cqrs.RequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IEnumerable<string> _detachFromEntities = new List<string>
    {
        nameof(Albums),
        nameof(Articles),
        nameof(ArticleLikes),
        nameof(ArticleCounts)
    };    

    private readonly IEnumerable<string> _removeFromEntities = new List<string>
    {
        nameof(UserPhotos),
        nameof(UserInfo),
        nameof(UserTokens),
        nameof(UserRefreshTokens),
        nameof(UserRoles),
        nameof(UserPermissions),
        nameof(Users)
    };

    public RemoveUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var userId = request.Id ?? await _userService.GetUserId(cancellationToken) ?? Guid.Empty;
        if (userId == Guid.Empty)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var currentUser = await DatabaseContext.Users
            .Where(users => !users.IsDeleted)
            .Where(users => users.Id == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (currentUser is null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        if (request.IsSoftDelete)
        {
            LoggerService.LogInformation($"Removing user account (user ID: {userId}). You can undo this operation at any time.");
            currentUser.IsDeleted = true;
        }
        else
        {
            LoggerService.LogInformation($"Removing permanently user account (user ID: {userId}). You cannot undo this operation.");
            await PermanentRemoval(userId, _detachFromEntities, _removeFromEntities, cancellationToken);
            DatabaseContext.Users.Remove(currentUser);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation($"User account (user ID: {userId}) has been removed");
        return Unit.Value;
    }

    private async Task PermanentRemoval(Guid userId, IEnumerable<string> detachFrom, IEnumerable<string> removeFrom, CancellationToken cancellationToken = default)
    {
        foreach (var entityName in detachFrom)
        {
            const string detachUserSql = "UPDATE [@p0] SET [UserId] = null WHERE [UserId] = @p1";
            var detachUserParams = new object[]
            {
                new SqlParameter("@p0", entityName),
                new SqlParameter("@p1", userId)
            };

            var result = await DatabaseContext.Database.ExecuteSqlRawAsync(detachUserSql, detachUserParams, cancellationToken);
            LoggerService.LogInformation($"Update command executed for '{entityName}', rows affected: {result}");
        }

        foreach (var entityName in removeFrom)
        {
            const string removeUserSql = "DELETE FROM [@p0] WHERE [UserId] = @p1";
            var removeUserParams = new object[]
            {
                new SqlParameter("@p0", entityName),
                new SqlParameter("@p1", userId)
            };

            var result = await DatabaseContext.Database.ExecuteSqlRawAsync(removeUserSql, removeUserParams, cancellationToken);
            LoggerService.LogInformation($"Delete command executed for '{entityName}', rows affected: {result}");
        }
    }
}