namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

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

public class RemoveArticleCommandHandler : Cqrs.RequestHandler<RemoveArticleCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveArticleCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var user = await _userService.GetActiveUser(cancellationToken);
        var article = await DatabaseContext.Articles
            .Where(articles => articles.UserId == user.UserId)
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var articleLikes = await DatabaseContext.ArticleLikes
            .Where(likes => likes.UserId == user.UserId)
            .Where(likes => likes.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var articleCounts = await DatabaseContext.ArticleCounts
            .Where(counts => counts.UserId == user.UserId)
            .Where(counts => counts.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLikes is not null)
        {
            LoggerService.LogInformation("Remove related article likes for given User ID and Article ID");
            DatabaseContext.ArticleLikes.Remove(articleLikes);
        }

        if (articleCounts is not null)
        {
            LoggerService.LogInformation("Remove related article counts for given User ID and Article ID");
            DatabaseContext.ArticleCounts.Remove(articleCounts);
        }

        DatabaseContext.Articles.Remove(article);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}