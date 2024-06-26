﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserCommandHandler : RequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.Id, true, cancellationToken);

        if (request.IsSoftDelete)
        {
            LoggerService.LogInformation($"Removing user account (user ID: {user.Id}). You can undo this operation at any time.");
            user.IsDeleted = true;
        }
        else
        {
            LoggerService.LogInformation($"Removing permanently user account (user ID: {user.Id}). You cannot undo this operation.");
            await PermanentRemoval(user.Id, cancellationToken);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        LoggerService.LogInformation($"User account (user ID: {user.Id}) has been removed");
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