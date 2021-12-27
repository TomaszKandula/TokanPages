namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using MediatR;

public class RemoveArticleCommandHandler : Cqrs.RequestHandler<RemoveArticleCommand, Unit>
{
    public RemoveArticleCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var currentArticle = await DatabaseContext.Articles
            .Where(articles => articles.Id == request.Id)
            .ToListAsync(cancellationToken);

        if (!currentArticle.Any())
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        DatabaseContext.Articles.Remove(currentArticle.Single());
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}