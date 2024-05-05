using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQueryHandler : RequestHandler<GetArticlesQuery, List<GetArticlesQueryResult>>
{
    public GetArticlesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetArticlesQueryResult>> Handle(GetArticlesQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Articles
            .AsNoTracking()
            .Where(articles => articles.IsPublished == request.IsPublished)
            .Select(articles => new GetArticlesQueryResult 
            { 
                Id = articles.Id,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                ReadCount = articles.ReadCount,
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt,
                LanguageIso = articles.LanguageIso
            })
            .OrderByDescending(articles => articles.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}