using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RemoveArticleCommandHandler : RequestHandler<RemoveArticleCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveArticleCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var userId = _userService.GetLoggedUserId();
        var articleData = await DatabaseContext.Articles
            .Where(article => article.UserId == userId)
            .Where(article => article.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var articleLike = await DatabaseContext.ArticleLikes
            .Where(like => like.UserId == userId)
            .Where(like => like.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var articleCount = await DatabaseContext.ArticleCounts
            .Where(count => count.UserId == userId)
            .Where(count => count.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLike is not null)
        {
            DatabaseContext.ArticleLikes.Remove(articleLike);
            LoggerService.LogInformation($"Article likes has been removed for User (ID: {userId}) and Article (ID: {articleData.Id})");
        }

        if (articleCount is not null)
        {
            DatabaseContext.ArticleCounts.Remove(articleCount);
            LoggerService.LogInformation($"Article counts has been removed for User (ID: {userId}) and Article (ID: {articleData.Id})");
        }

        DatabaseContext.Articles.Remove(articleData);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}