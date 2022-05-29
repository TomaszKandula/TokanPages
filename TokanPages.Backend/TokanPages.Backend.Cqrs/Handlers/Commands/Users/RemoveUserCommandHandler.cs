namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
            await PermanentRemoval(userId, cancellationToken);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation($"User account (user ID: {userId}) has been removed");
        return Unit.Value;
    }

    private async Task PermanentRemoval(Guid userId, CancellationToken cancellationToken = default)
    {
        await DetachFromUser(userId, cancellationToken);
        await RemoveFromUser(userId, cancellationToken);
    }

    private async Task DetachFromUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var albums = await DatabaseContext.Albums
            .Where(albums => albums.UserId == userId)
            .ToListAsync(cancellationToken);

        if (albums.Any())
        {
            foreach (var item in albums) { item.UserId = null; item.UserPhotoId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(Albums)}");
        }

        var articles = await DatabaseContext.Articles
            .Where(articles => articles.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articles.Any())
        {
            foreach (var item in articles) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(Articles)}");
        }

        var articleLikes = await DatabaseContext.ArticleLikes
            .Where(articleLikes => articleLikes.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articleLikes.Any())
        {
            foreach (var item in articleLikes) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(ArticleLikes)}");
        }

        var articleCounts = await DatabaseContext.ArticleCounts
            .Where(articleCounts => articleCounts.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articleCounts.Any())
        {
            foreach (var item in articleCounts) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(ArticleCounts)}");
        }
    }

    private async Task RemoveFromUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var userPhotos = await DatabaseContext.UserPhotos
            .Where(userPhotos => userPhotos.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userPhotos.Any())
        {
            DatabaseContext.RemoveRange(userPhotos);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserPhotos)}");
        }

        var userInfo = await DatabaseContext.UserInfo
            .Where(userInfo => userInfo.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userInfo.Any())
        {
            DatabaseContext.RemoveRange(userInfo);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserInfo)}");
        }

        var userTokens = await DatabaseContext.UserTokens
            .Where(userTokens => userTokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userTokens.Any())
        {
            DatabaseContext.RemoveRange(userTokens);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserTokens)}");
        }

        var userRefreshTokens = await DatabaseContext.UserRefreshTokens
            .Where(userRefreshTokens => userRefreshTokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userRefreshTokens.Any())
        {
            DatabaseContext.RemoveRange(userRefreshTokens);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserRefreshTokens)}");
        }

        var userRoles = await DatabaseContext.UserRoles
            .Where(userRoles => userRoles.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userRoles.Any())
        {
            DatabaseContext.RemoveRange(userRoles);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserRoles)}");
        }

        var userPermissions = await DatabaseContext.UserPermissions
            .Where(userPermissions => userPermissions.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userPermissions.Any())
        {
            DatabaseContext.RemoveRange(userPermissions);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserPermissions)}");
        }

        var users = await DatabaseContext.Users
            .Where(users => users.Id == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (users is not null)
        {
            DatabaseContext.Remove(users);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(Users)}");
        }
    }
}