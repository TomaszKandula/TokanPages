using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{

    public class GetAllArticlesQueryHandler : TemplateHandler<GetAllArticlesQuery, IEnumerable<Domain.Entities.Articles>>
    {

        private readonly DatabaseContext FDatabaseContext;

        public GetAllArticlesQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<IEnumerable<Domain.Entities.Articles>> Handle(GetAllArticlesQuery ARequest, CancellationToken ACancellationToken) 
        {
            var LArticles = await FDatabaseContext.Articles.AsNoTracking().ToListAsync(ACancellationToken);
            return LArticles;
        }

    }

}
