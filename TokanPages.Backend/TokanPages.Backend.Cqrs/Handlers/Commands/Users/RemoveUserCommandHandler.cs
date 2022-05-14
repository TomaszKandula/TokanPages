namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using MediatR;

public class RemoveUserCommandHandler : Cqrs.RequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserService _userService;
    
    public RemoveUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService)
    {
        _userService = userService;
    }

    public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var userId = request.Id ?? await _userService.GetUserId() ?? Guid.Empty;
        if (userId == Guid.Empty)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var currentUser = await DatabaseContext.Users
            .Where(users => users.Id == userId)
            .ToListAsync(cancellationToken);

        if (!currentUser.Any())
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var userRefreshTokens = await DatabaseContext.UserRefreshTokens
            .Where(tokens => tokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userRefreshTokens.Any())
        {
            LoggerService.LogInformation($"Removing user refresh tokens for user ID: {userId}");
            DatabaseContext.RemoveRange(userRefreshTokens);
        }

        var userTokens = await DatabaseContext.UserTokens
            .Where(tokens => tokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userTokens.Any())
        {
            LoggerService.LogInformation($"Removing user tokens for user ID: {userId}");
            DatabaseContext.RemoveRange(userTokens);
        }

        var userRoles = await DatabaseContext.UserRoles
            .Where(roles => roles.UserId == userId)
            .ToListAsync(cancellationToken);        
        
        if (userRoles.Any())
        {
            LoggerService.LogInformation($"Removing assigned user roles for user ID: {userId}");
            DatabaseContext.RemoveRange(userRoles);
        }

        var userPermissions = await DatabaseContext.UserPermissions
            .Where(permissions => permissions.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userPermissions.Any())
        {
            LoggerService.LogInformation($"Removing assigned user permissions for user ID: {userId}");
            DatabaseContext.RemoveRange(userPermissions);
        }

        var articles = await DatabaseContext.Articles
            .Where(articles => articles.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articles.Any())
        {
            LoggerService.LogInformation($"Detaching articles from user (ID: {userId})");
            foreach (var article in articles)
            {
                article.UserId = Guid.Empty;
                article.IsPublished = false;
            }
        }

        var albums = await DatabaseContext.Albums
            .Where(albums => albums.UserId == userId)
            .ToListAsync(cancellationToken);

        if (albums.Any())
        {
            LoggerService.LogInformation($"Detaching albums from user (ID: {userId})");
            foreach (var album in albums)
            {
                album.UserId = Guid.Empty;
            }
        }

        LoggerService.LogInformation($"Removing user account (user ID: {userId})");
        DatabaseContext.Users.Remove(currentUser.First());
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        LoggerService.LogInformation($"User account (user ID: {userId}) has been removed");
        return Unit.Value;
    }
}