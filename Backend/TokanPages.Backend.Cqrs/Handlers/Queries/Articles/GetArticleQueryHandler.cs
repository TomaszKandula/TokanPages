using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetArticleQueryHandler : TemplateHandler<GetArticleQuery, Domain.Entities.Articles>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetArticleQueryHandler(DatabaseContext ADatabaseContext)
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<Domain.Entities.Articles> Handle(GetArticleQuery ARequest, CancellationToken ACancellationToken)
        {

            var LCurrentArticle = await FDatabaseContext.Articles
                .AsNoTracking()
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LCurrentArticle.Any()) 
            {
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            }

            return LCurrentArticle.Single();

        }

    }

}
