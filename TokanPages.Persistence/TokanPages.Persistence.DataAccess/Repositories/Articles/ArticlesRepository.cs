using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public class ArticlesRepository : IArticlesRepository
{
    private readonly OperationDbContext _operationDbContext;

    public ArticlesRepository(OperationDbContext operationDbContext) => _operationDbContext = operationDbContext;

    public async Task<Guid> GetArticleIdByTitle(string title, CancellationToken cancellationToken = default)
    {
        var comparableTitle = title.Replace("-", " ").ToLower();
        return await _operationDbContext.Articles
            .AsNoTracking()
            .Where(article => article.Title.ToLower() == comparableTitle)
            .Select(article => article.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<GetArticleOutput> GetArticle(Guid userId, Guid requestId, bool isAnonymousUser, string ipAddress, string userLanguage, CancellationToken cancellationToken)
    {
        var userLikes = await _operationDbContext.ArticleLikes
            .AsNoTracking()
            .Where(like => like.ArticleId == requestId)
            .WhereIfElse(isAnonymousUser, 
                like => like.IpAddress == ipAddress && like.UserId == null, 
                like => like.UserId == userId)
            .Select(like => like.LikeCount)
            .SumAsync(cancellationToken);

        var totalLikes = await _operationDbContext.ArticleLikes
            .AsNoTracking()
            .Where(like => like.ArticleId == requestId)
            .Select(like => like.LikeCount)
            .SumAsync(cancellationToken);

        var articleData = await (from article in _operationDbContext.Articles
            join articleCategory in _operationDbContext.ArticleCategories 
                on article.CategoryId equals articleCategory.Id
            join categoryName in _operationDbContext.ArticleCategoryNames
                on articleCategory.Id equals categoryName.ArticleCategoryId
            join language in _operationDbContext.Languages
                on categoryName.LanguageId equals language.Id
            where language.LangId == userLanguage
            where article.Id == requestId
            select new 
            {
                article.Id,
                article.UserId,
                article.Title,
                article.Description,
                article.IsPublished,
                article.CreatedAt,
                article.UpdatedAt,
                article.ReadCount,
                article.LanguageIso,
                categoryName.Name,
                TotalLikes = totalLikes,
                UserLikes = userLikes,
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var userDto = await (from user in _operationDbContext.Users
            join userInfo in _operationDbContext.UserInformation 
            on user.Id equals userInfo.UserId
            where user.Id == articleData.UserId
            select new GetUserDto
            {
                UserId = user.Id,
                AliasName = user.UserAlias,
                AvatarName = userInfo.UserImageName,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                ShortBio = userInfo.UserAboutText,
                Registered = userInfo.CreatedAt
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        var tags = await (from articleTags in _operationDbContext.ArticleTags
            join articles in _operationDbContext.Articles
            on articleTags.ArticleId equals articles.Id
            where articles.Id == requestId
            select articleTags.TagName)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return new GetArticleOutput
        {
            Id = articleData.Id,
            Title = articleData.Title,
            CategoryName = articleData.Name,
            Description = articleData.Description,
            IsPublished = articleData.IsPublished,
            CreatedAt = articleData.CreatedAt,
            UpdatedAt = articleData.UpdatedAt,
            ReadCount = articleData.ReadCount,
            LanguageIso = articleData.LanguageIso,
            TotalLikes = totalLikes,
            UserLikes = userLikes,
            Author = userDto,
            Tags = tags,
        };
    }

    public async Task<List<ArticleDataDto>> GetArticleList(bool isPublished, string? searchTerm, Guid? categoryId, HashSet<Guid>? foundArticleIds, IDictionary<string, Expression<Func<ArticleDataDto, object>>> orderByExpressions, CancellationToken cancellationToken = default)
    {
        var hasArticleIds = foundArticleIds != null && foundArticleIds.Count != 0;
        var hasCategoryId = categoryId != null && categoryId != Guid.Empty;

        var articles = _operationDbContext.Articles
            .AsNoTracking()
            .Include(article => article.ArticleCategory)
            .Where(article => article.IsPublished == isPublished)
            .WhereIf(hasArticleIds, article => foundArticleIds!.Contains(article.Id))
            .WhereIf(hasCategoryId, article => article.ArticleCategory.Id == categoryId)
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

        var request = new ArticleListRequest
        {
            SearchTerm = searchTerm,
            IsPublished = isPublished,
            CategoryId = categoryId,
        };

        return await articles
            .ApplyOrdering(request, orderByExpressions)
            .ApplyPaging(request)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<ArticleCategoryDto>> GetArticleCategories(string userLanguage, CancellationToken cancellationToken = default)
    {
        var categories = await (from articleCategory in _operationDbContext.ArticleCategories
            join categoryName in _operationDbContext.ArticleCategoryNames
                on articleCategory.Id equals categoryName.ArticleCategoryId into category 
            from categoryName in category.DefaultIfEmpty() 
            join language in _operationDbContext.Languages
                on categoryName.LanguageId equals language.Id into languageTable
            from  language in languageTable.DefaultIfEmpty()
            where language.LangId == userLanguage
            select new ArticleCategoryDto
            {
                Id = articleCategory.Id,
                CategoryName = categoryName.Name
            }).ToListAsync(cancellationToken);

        return categories;
    }

    public async Task<HashSet<Guid>?> GetSearchResult(string? searchTerm, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return null;

        var searchTitleAndDescription = await _operationDbContext.Articles
            .AsNoTracking()
            .Where(articles => articles.Title.Contains(searchTerm) || articles.Description.Contains(searchTerm))
            .Select(articles => articles.Id)
            .ToListAsync(cancellationToken);

        var searchTags = await _operationDbContext.ArticleTags
            .AsNoTracking()
            .Where(articleTags => articleTags.TagName.Contains(searchTerm))
            .Select(articleTags => articleTags.ArticleId)
            .ToListAsync(cancellationToken);

        var articleIds = searchTitleAndDescription.Union(searchTags);
        return new HashSet<Guid>(articleIds);
    }
}