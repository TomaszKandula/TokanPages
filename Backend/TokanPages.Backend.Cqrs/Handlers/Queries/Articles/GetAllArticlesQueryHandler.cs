using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    public class GetAllArticlesQueryHandler : TemplateHandler<GetAllArticlesQuery, IEnumerable<GetAllArticlesQueryResult>>
    {
        private readonly DatabaseContext FDatabaseContext;

        public GetAllArticlesQueryHandler(DatabaseContext ADatabaseContext) 
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<IEnumerable<GetAllArticlesQueryResult>> Handle(GetAllArticlesQuery ARequest, CancellationToken ACancellationToken) 
        {
            var LArticles = await FDatabaseContext.Articles
                .AsNoTracking()
                .Where(Articles => Articles.IsPublished == ARequest.IsPublished)
                .Select(Fields => new GetAllArticlesQueryResult 
                { 
                    Id = Fields.Id,
                    Title = Fields.Title,
                    Description = Fields.Description,
                    IsPublished = Fields.IsPublished,
                    ReadCount = Fields.ReadCount,
                    CreatedAt = Fields.CreatedAt,
                    UpdatedAt = Fields.UpdatedAt
                })
                .ToListAsync(ACancellationToken);

            return LArticles; 
        }
    }
}
