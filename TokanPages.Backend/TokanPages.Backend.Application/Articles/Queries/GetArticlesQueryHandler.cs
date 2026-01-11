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

        var articleList = DatabaseContext.Articles
            .AsNoTracking()
            .Include(article => article.ArticleCategory)
            .Where(article => article.IsPublished == request.IsPublished)
            .WhereIf(hasIds, article => foundArticleIds!.Contains(article.Id))
            .WhereIf(hasCategoryId, article => article.ArticleCategory.Id == request.CategoryId)
            .Select(article => new ArticleDataDto
            { 
                Id = article.Id,
                Title = article.Title,
                Description = article.Description,
                IsPublished = article.IsPublished,
                ReadCount = article.ReadCount,
                TotalLikes = article.TotalLikes, 
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                LanguageIso = article.LanguageIso
            });

        var totalSize = await articleList.CountAsync(cancellationToken);
        var result = await articleList
            .ApplyOrdering(request, GetOrderingExpressions())
            .ApplyPaging(request)
            .ToListAsync(cancellationToken);

        var categories = await (from articleCategory in DatabaseContext.ArticleCategories
            join categoryName in DatabaseContext.CategoryNames
                on articleCategory.Id equals categoryName.ArticleCategoryId into category 
                    from categoryName in category.DefaultIfEmpty() 
            join language in DatabaseContext.Languages
                on categoryName.LanguageId equals language.Id into languageTable
                    from  language in languageTable.DefaultIfEmpty()
            where language.LangId == userLanguage
            select new ArticleCategoryDto
            {
                Id = articleCategory.Id,
                CategoryName = categoryName.Name
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