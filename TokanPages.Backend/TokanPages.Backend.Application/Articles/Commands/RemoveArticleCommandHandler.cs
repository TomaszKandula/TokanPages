using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RemoveArticleCommandHandler : RequestHandler<RemoveArticleCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveArticleCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService) : base(operationDbContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var userId = _userService.GetLoggedUserId();
        var articleData = await OperationDbContext.Articles
            .Where(article => article.UserId == userId)
            .Where(article => article.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var articleLike = await OperationDbContext.ArticleLikes
            .Where(like => like.UserId == userId)
            .Where(like => like.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        var articleCount = await OperationDbContext.ArticleCounts
            .Where(count => count.UserId == userId)
            .Where(count => count.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLike is not null)
        {
            OperationDbContext.ArticleLikes.Remove(articleLike);
            LoggerService.LogInformation($"Article likes has been removed for User (ID: {userId}) and Article (ID: {articleData.Id})");
        }

        if (articleCount is not null)
        {
            OperationDbContext.ArticleCounts.Remove(articleCount);
            LoggerService.LogInformation($"Article counts has been removed for User (ID: {userId}) and Article (ID: {articleData.Id})");
        }

        OperationDbContext.Articles.Remove(articleData);
        await OperationDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}