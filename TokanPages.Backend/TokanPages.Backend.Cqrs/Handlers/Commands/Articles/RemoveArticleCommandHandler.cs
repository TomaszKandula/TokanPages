namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using MediatR;

    public class RemoveArticleCommandHandler : TemplateHandler<RemoveArticleCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        public RemoveArticleCommandHandler(DatabaseContext ADatabaseContext) 
            => FDatabaseContext = ADatabaseContext;

        public override async Task<Unit> Handle(RemoveArticleCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LCurrentArticle = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LCurrentArticle.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            FDatabaseContext.Articles.Remove(LCurrentArticle.Single());
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}