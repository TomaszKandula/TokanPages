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