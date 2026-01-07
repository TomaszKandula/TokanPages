using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Application.Articles.Models;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQueryHandler : TableRequestHandler<ArticleDataDto, GetArticlesQuery, GetArticlesQueryResult>
{
    private readonly IUserService _userService;

    public GetArticlesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService) 
        : base(databaseContext, loggerService) => _userService = userService;

    public override IDictionary<string, Expression<Func<ArticleDataDto, object>>> GetOrderingExpressions() => GetSortingConfig();

    public override async Task<GetArticlesQueryResult> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        var userLanguage = _userService.GetRequestUserLanguage();
        var foundArticleIds = await GetSearchResult(request.SearchTerm, cancellationToken);
        var hasIds = foundArticleIds != null && foundArticleIds.Count != 0;
        var hasCategoryId = request.CategoryId != null && request.CategoryId != Guid.Empty;

        var articles = DatabaseContext.Articles
            .AsNoTracking()
            .Include(articles => articles.ArticleCategory)
            .Where(articles => articles.IsPublished == request.IsPublished)
            .WhereIf(hasIds, articles => foundArticleIds!.Contains(articles.Id))
            .WhereIf(hasCategoryId, articles => articles.ArticleCategory.Id == request.CategoryId)
            .Select(articles => new ArticleDataDto
            { 
                Id = articles.Id,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                ReadCount = articles.ReadCount,
                TotalLikes = articles.TotalLikes, 
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt,
                LanguageIso = articles.LanguageIso
            });

        var totalSize = await articles.CountAsync(cancellationToken);
        var result = await articles
            .ApplyOrdering(request, GetOrderingExpressions())
            .ApplyPaging(request)
            .ToListAsync(cancellationToken);

        var categories = await (from articleCategory in DatabaseContext.ArticleCategory
            join categoryNames in DatabaseContext.CategoryNames
                on articleCategory.Id equals categoryNames.ArticleCategoryId into category 
                    from categoryNames in category.DefaultIfEmpty() 
            join languages in DatabaseContext.Languages
                on categoryNames.LanguageId equals languages.Id into language
                    from  languages in language.DefaultIfEmpty()
            where languages.LangId == userLanguage
            select new ArticleCategoryDto
            {
                Id = articleCategory.Id,
                CategoryName = categoryNames.Name
            }).ToListAsync(cancellationToken);

        return new GetArticlesQueryResult
        {
            PagingInfo = request,
            TotalSize = totalSize,
            ArticleCategories = categories,
            Results = result
        };
    }

    private async Task<HashSet<Guid>?> GetSearchResult(string? searchTerm, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return null;
        }

        var searchTitleAndDescription = await DatabaseContext.Articles
            .AsNoTracking()
            .Where(articles => articles.Title.Contains(searchTerm) || articles.Description.Contains(searchTerm))
            .Select(articles => articles.Id)
            .ToListAsync(cancellationToken);

        var searchTags = await DatabaseContext.ArticleTags
            .AsNoTracking()
            .Where(articleTags => articleTags.TagName.Contains(searchTerm))
            .Select(articleTags => articleTags.ArticleId)
            .ToListAsync(cancellationToken);

        var articleIds = searchTitleAndDescription.Union(searchTags);
        return new HashSet<Guid>(articleIds);
    }

    private static Dictionary<string, Expression<Func<ArticleDataDto, object>>> GetSortingConfig()
    {
        return new Dictionary<string, Expression<Func<ArticleDataDto, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            {nameof(ArticleDataDto.Title), articlesQueryResult => articlesQueryResult.Title},
            {nameof(ArticleDataDto.Description), articlesQueryResult => articlesQueryResult.Description},
            {nameof(ArticleDataDto.CreatedAt), articlesQueryResult => articlesQueryResult.CreatedAt}
        };
    }
}