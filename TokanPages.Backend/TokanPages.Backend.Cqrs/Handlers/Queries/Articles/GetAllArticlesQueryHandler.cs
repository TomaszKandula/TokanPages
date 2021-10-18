namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Logger;

    public class GetAllArticlesQueryHandler : TemplateHandler<GetAllArticlesQuery, IEnumerable<GetAllArticlesQueryResult>>
    {
        public GetAllArticlesQueryHandler(DatabaseContext databaseContext, ILogger logger) : base(databaseContext, logger) { }

        public override async Task<IEnumerable<GetAllArticlesQueryResult>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken) 
        {
            var articles = await DatabaseContext.Articles
                .AsNoTracking()
                .Where(articles => articles.IsPublished == request.IsPublished)
                .Select(articles => new GetAllArticlesQueryResult 
                { 
                    Id = articles.Id,
                    Title = articles.Title,
                    Description = articles.Description,
                    IsPublished = articles.IsPublished,
                    ReadCount = articles.ReadCount,
                    CreatedAt = articles.CreatedAt,
                    UpdatedAt = articles.UpdatedAt
                })
                .OrderByDescending(articles => articles.CreatedAt)
                .ToListAsync(cancellationToken);

            return articles; 
        }
    }
}