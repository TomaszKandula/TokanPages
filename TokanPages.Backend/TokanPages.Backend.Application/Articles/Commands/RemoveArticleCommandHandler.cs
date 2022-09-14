using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RemoveArticleCommandHandler : RequestHandler<RemoveArticleCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveArticleCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var user = await _userService.GetActiveUser(null, false, cancellationToken);
        var article = await DatabaseContext.Articles
            .Where(articles => articles.UserId == user.Id)
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var articleLikes = await DatabaseContext.ArticleLikes
            .Where(likes => likes.UserId == user.Id)
            .Where(likes => likes.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var articleCounts = await DatabaseContext.ArticleCounts
            .Where(counts => counts.UserId == user.Id)
            .Where(counts => counts.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLikes is not null)
        {
            DatabaseContext.ArticleLikes.Remove(articleLikes);
            LoggerService.LogInformation($"Article likes has been removed for User (ID: {user.Id}) and Article (ID: {article.Id})");
        }

        if (articleCounts is not null)
        {
            DatabaseContext.ArticleCounts.Remove(articleCounts);
            LoggerService.LogInformation($"Article counts has been removed for User (ID: {user.Id}) and Article (ID: {article.Id})");
        }

        DatabaseContext.Articles.Remove(article);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}