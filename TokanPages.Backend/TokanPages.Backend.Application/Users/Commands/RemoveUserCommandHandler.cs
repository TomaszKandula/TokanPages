using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Domain.Entities.Photography;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserCommandHandler : RequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveUserCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService) : base(operationDbContext, loggerService) => _userService = userService;

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

        await OperationDbContext.SaveChangesAsync(cancellationToken);
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
        var albums = await OperationDbContext.Albums
            .Where(albums => albums.UserId == userId)
            .ToListAsync(cancellationToken);

        if (albums.Count > 0)
        {
            foreach (var item in albums) { item.UserId = null; item.UserPhotoId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(Album)}");
        }

        var articles = await OperationDbContext.Articles
            .Where(articles => articles.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articles.Count > 0)
        {
            foreach (var item in articles) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(Articles)}");
        }

        var articleLikes = await OperationDbContext.ArticleLikes
            .Where(articleLikes => articleLikes.UserId == userId)
            .ToListAsync(cancellationToken);

        if (articleLikes.Count > 0)
        {
            foreach (var item in articleLikes) { item.UserId = null; }
            LoggerService.LogInformation($"User (ID: {userId}) detached from {nameof(ArticleLike)}");
        }

        var articleCounts = await OperationDbContext.ArticleCounts
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
        var userNotes = await OperationDbContext.UserNotes
            .Where(userNotes => userNotes.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userNotes.Count > 0)
        {
            OperationDbContext.RemoveRange(userNotes);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(userNotes)}");
        }

        var userPhotos = await OperationDbContext.Photos
            .Where(userPhotos => userPhotos.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userPhotos.Count > 0)
        {
            OperationDbContext.RemoveRange(userPhotos);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(Photo)}");
        }

        var userInfo = await OperationDbContext.UserInformation
            .Where(userInfo => userInfo.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userInfo.Count > 0)
        {
            OperationDbContext.RemoveRange(userInfo);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserInfo)}");
        }

        var userTokens = await OperationDbContext.UserTokens
            .Where(userTokens => userTokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userTokens.Count > 0)
        {
            OperationDbContext.RemoveRange(userTokens);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserToken)}");
        }

        var userRefreshTokens = await OperationDbContext.UserRefreshTokens
            .Where(userRefreshTokens => userRefreshTokens.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userRefreshTokens.Count > 0)
        {
            OperationDbContext.RemoveRange(userRefreshTokens);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserRefreshToken)}");
        }

        var userRoles = await OperationDbContext.UserRoles
            .Where(userRoles => userRoles.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userRoles.Count > 0)
        {
            OperationDbContext.RemoveRange(userRoles);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserRole)}");
        }

        var userPermissions = await OperationDbContext.UserPermissions
            .Where(userPermissions => userPermissions.UserId == userId)
            .ToListAsync(cancellationToken);

        if (userPermissions.Count > 0)
        {
            OperationDbContext.RemoveRange(userPermissions);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(UserPermission)}");
        }

        var users = await OperationDbContext.Users
            .Where(users => users.Id == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (users is not null)
        {
            OperationDbContext.Remove(users);
            LoggerService.LogInformation($"User (ID: {userId}) removed from {nameof(Users)}");
        }
    }
}