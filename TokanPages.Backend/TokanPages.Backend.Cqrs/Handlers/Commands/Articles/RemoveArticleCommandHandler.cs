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
        private readonly DatabaseContext _databaseContext;

        public RemoveArticleCommandHandler(DatabaseContext databaseContext) 
            => _databaseContext = databaseContext;

        public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
        {
            var currentArticle = await _databaseContext.Articles
                .Where(articles => articles.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!currentArticle.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            _databaseContext.Articles.Remove(currentArticle.Single());
            await _databaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}