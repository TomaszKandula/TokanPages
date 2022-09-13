using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetAllArticlesQueryHandler : RequestHandler<GetAllArticlesQuery, List<GetAllArticlesQueryResult>>
{
    public GetAllArticlesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetAllArticlesQueryResult>> Handle(GetAllArticlesQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Articles
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
    }
}