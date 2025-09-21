using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQueryHandler : TableRequestHandler<GetArticlesQueryResult, GetArticlesQuery, GetAllArticlesQueryResult>
{
    public GetArticlesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override IDictionary<string, Expression<Func<GetArticlesQueryResult, object>>> GetOrderingExpressions() => GetSortingConfig();

    public override async Task<GetAllArticlesQueryResult> Handle(GetArticlesQuery request, CancellationToken cancellationToken) 
    {
        var query = DatabaseContext.Articles
            .AsNoTracking()
            .Include(articles => articles.ArticleCategory)
            .Where(articles => articles.IsPublished == request.IsPublished)
            .WhereIf(!string.IsNullOrWhiteSpace(request.SearchTerm), articles => articles.Title.Contains(request.SearchTerm!))
            .WhereIf(!string.IsNullOrWhiteSpace(request.CategoryName), articles => articles.ArticleCategory.CategoryName.Contains(request.CategoryName!))
            .Select(articles => new GetArticlesQueryResult 
            { 
                Id = articles.Id,
                CategoryName = articles.ArticleCategory.CategoryName,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                ReadCount = articles.ReadCount,
                TotalLikes = articles.TotalLikes, 
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt,
                LanguageIso = articles.LanguageIso
            });

        var totalSize = await query.CountAsync(cancellationToken);
        var result = await query
            .ApplyOrdering(request, GetOrderingExpressions())
            .ApplyPaging(request)
            .ToListAsync(cancellationToken);

        return new GetAllArticlesQueryResult
        {
            PagingInfo = request,
            TotalSize = totalSize,
            Results = result
        };
    }

    private static Dictionary<string, Expression<Func<GetArticlesQueryResult, object>>> GetSortingConfig()
    {
        return new Dictionary<string, Expression<Func<GetArticlesQueryResult, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            {nameof(GetArticlesQueryResult.Title), articlesQueryResult => articlesQueryResult.Title},
            {nameof(GetArticlesQueryResult.Description), articlesQueryResult => articlesQueryResult.Description},
            {nameof(GetArticlesQueryResult.CreatedAt), articlesQueryResult => articlesQueryResult.CreatedAt},
            {nameof(GetArticlesQueryResult.CategoryName), articlesQueryResult => articlesQueryResult.CategoryName}
        };
    }
}