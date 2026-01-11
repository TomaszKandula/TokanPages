using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserCommandHandler : RequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveUserCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, 
        IUserService userService) : base(operationsDbContext, loggerService) => _userService = userService;

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

        await OperationsDbContext.SaveChangesAsync(cancellationToken);
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
        var albums = await OperationsDbContext.Albums
            .Where(albums => albums.UserId == userId)
            .ToListAsync(cancellationToken);

        if (albums.Count > 0)
        {
            foreach (var item in albums) { item.UserId = null; item.UserPhotoId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(Album)}");
        }

        var articles = await OperationsDbContext.Articles
            .Where(articles => articles.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articles.Count > 0)
        {
            foreach (var item in articles) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(Articles)}");
        }

        var articleLikes = await OperationsDbContext.ArticleLikes
            .Where(articleLikes => articleLikes.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articleLikes.Count > 0)
        {
            foreach (var item in articleLikes) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(ArticleLike)}");
        }

        var articleCounts = await OperationsDbContext.ArticleCounts
            .Where(articleCounts => articleCounts.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articleCounts.Count > 0)
        {
            foreach (var item in articleCounts) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(ArticleCount)}");
        }
    }

    private async Task RemoveFromUser(Guid userId, CancellationToken cancellationToken = default)
    {
        var userNotes = await OperationsDbContext.UserNotes
            .Where(userNotes => userNotes.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userNotes.Count > 0)
        {
            OperationsDbContext.RemoveRange(userNotes);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(userNotes)}");
        }

        var userPhotos = await OperationsDbContext.UserPhotos
            .Where(userPhotos => userPhotos.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userPhotos.Count > 0)
        {
            OperationsDbContext.RemoveRange(userPhotos);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserPhoto)}");
        }

        var userInfo = await OperationsDbContext.UserInformation
            .Where(userInfo => userInfo.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userInfo.Count > 0)
        {
            OperationsDbContext.RemoveRange(userInfo);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserInfo)}");
        }

        var userTokens = await OperationsDbContext.UserTokens
            .Where(userTokens => userTokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userTokens.Count > 0)
        {
            OperationsDbContext.RemoveRange(userTokens);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserToken)}");
        }

        var userRefreshTokens = await OperationsDbContext.UserRefreshTokens
            .Where(userRefreshTokens => userRefreshTokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userRefreshTokens.Count > 0)
        {
            OperationsDbContext.RemoveRange(userRefreshTokens);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserRefreshToken)}");
        }

        var userRoles = await OperationsDbContext.UserRoles
            .Where(userRoles => userRoles.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userRoles.Count > 0)
        {
            OperationsDbContext.RemoveRange(userRoles);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserRole)}");
        }

        var userPermissions = await OperationsDbContext.UserPermissions
            .Where(userPermissions => userPermissions.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userPermissions.Count > 0)
        {
            OperationsDbContext.RemoveRange(userPermissions);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserPermission)}");
        }

        var users = await OperationsDbContext.Users
            .Where(users => users.Id == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (users is not null)
        {
            OperationsDbContext.Remove(users);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(Users)}");
        }
    }
}