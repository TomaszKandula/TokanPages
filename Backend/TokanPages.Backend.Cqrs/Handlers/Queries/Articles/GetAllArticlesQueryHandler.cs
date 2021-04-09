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
            => FDatabaseContext = ADatabaseContext;

        public override async Task<IEnumerable<GetAllArticlesQueryResult>> Handle(GetAllArticlesQuery ARequest, CancellationToken ACancellationToken) 
        {
            var LArticles = await FDatabaseContext.Articles
                .AsNoTracking()
                .Where(AArticles => AArticles.IsPublished == ARequest.IsPublished)
                .Select(AFields => new GetAllArticlesQueryResult 
                { 
                    Id = AFields.Id,
                    Title = AFields.Title,
                    Description = AFields.Description,
                    IsPublished = AFields.IsPublished,
                    ReadCount = AFields.ReadCount,
                    CreatedAt = AFields.CreatedAt,
                    UpdatedAt = AFields.UpdatedAt
                })
                .OrderByDescending(AArticles => AArticles.CreatedAt)
                .ToListAsync(ACancellationToken);

            return LArticles; 
        }
    }
}
