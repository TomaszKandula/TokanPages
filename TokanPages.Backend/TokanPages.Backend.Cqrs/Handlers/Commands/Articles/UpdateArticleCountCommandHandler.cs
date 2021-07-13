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

    public class UpdateArticleCountCommandHandler : TemplateHandler<UpdateArticleCountCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        public UpdateArticleCountCommandHandler(DatabaseContext ADatabaseContext)
            => FDatabaseContext = ADatabaseContext;

        public override async Task<Unit> Handle(UpdateArticleCountCommand ARequest, CancellationToken ACancellationToken)
        {
            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var LCurrentArticle = LArticles.First();
            LCurrentArticle.ReadCount += 1;

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}