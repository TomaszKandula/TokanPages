using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DataUtilityService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public class ArticlesRepository : IArticlesRepository
{
    private readonly OperationDbContext _operationDbContext;
    
    private readonly IDataUtilityService _dataUtilityService;

    //TODO: replace IConfiguration with IOption
    private readonly IConfiguration _configuration;
    private int MaxLikesForAnonymousUser => _configuration.GetValue<int>("Limit_Likes_Anonymous");
    private int MaxLikesForLoggedUser => _configuration.GetValue<int>("Limit_Likes_User");
    private string ConnectionString => _configuration.GetValue<string>("Db_DatabaseContext") ?? "";

    public ArticlesRepository(OperationDbContext operationDbContext, IConfiguration configuration, IDataUtilityService dataUtilityService)
    {
        _operationDbContext = operationDbContext;
        _configuration = configuration;
        _dataUtilityService = dataUtilityService;
    }

    public async Task<Guid> GetArticleIdByTitle(string title, CancellationToken cancellationToken = default)
    {
        const string query = "SELECT operation.Articles.Id FROM Operations.Articles WHERE Operations.Articles.Title = @Title";

        await using var db = new SqlConnection(ConnectionString);
        return await db.QuerySingleOrDefaultAsync<Guid>(query, new { Title = title.Replace("-", " ").ToLower() });
    }

    public async Task<GetArticleOutputDto?> GetArticle(Guid userId, Guid requestId, bool isAnonymousUser, string ipAddress, string userLanguage, CancellationToken cancellationToken)
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
            return null;

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

        return new GetArticleOutputDto
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

    public async Task<List<ArticleDataDto>> GetArticleList(bool isPublished, string? searchTerm, Guid? categoryId, HashSet<Guid>? filterById, ArticlePageInfoDto pageInfo, CancellationToken cancellationToken = default)
    {
        var query = @"
            SELECT 
                operation.Articles.Id,
                operation.Articles.Title,
                operation.Articles.Description,
                operation.Articles.IsPublished,
                operation.Articles.ReadCount,
                operation.Articles.TotalLikes,
                operation.Articles.CreatedAt,
                operation.Articles.UpdatedAt,
                operation.Articles.LanguageIso,
                COUNT(*) OVER() AS CountOver
            FROM 
                operation.Articles 
            LEFT JOIN 
                operation.ArticleCategories ON operation.Articles.CategoryId = operation.ArticleCategories.Id
            WHERE 
                operation.Articles.IsPublished = 1";

        if (categoryId != null && categoryId != Guid.Empty)
            query += $"\nAND operation.Articles.CategoryId = '{categoryId}'";

        if (filterById != null && filterById.Count != 0)
            query += $"\nAND operation.Articles.Id IN ({_dataUtilityService.GuidToSQLStrings(filterById)})";

        query += $"\nORDER BY {pageInfo.OrderByColumn} {pageInfo.OrderByAscending}";

        var skipCount = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
        query += $"\nOFFSET {skipCount} ROWS FETCH NEXT {pageInfo.PageSize} ROWS ONLY";

        await using var db = new SqlConnection(ConnectionString);
        var articles = (await db.QueryAsync<ArticleDataDto>(query)).ToList();

        return articles;
    }

    public async Task<List<ArticleCategoryDto>> GetArticleCategories(string userLanguage, CancellationToken cancellationToken = default)
    {
        const string query = @"
            SELECT 
                operation.ArticleCategories.Id,
                operation.ArticleCategoryNames.Name AS CategoryName
            FROM
                operation.ArticleCategories
            LEFT JOIN
                operation.ArticleCategoryNames ON operation.ArticleCategoryNames.ArticleCategoryId = operation.ArticleCategories.Id
            LEFT JOIN
                operation.Languages ON operation.Languages.Id = operation.ArticleCategoryNames.LanguageId
            WHERE
                operation.Languages.LangId = @UserLanguage";

        await using var db = new SqlConnection(ConnectionString);
        return (await db.QueryAsync<ArticleCategoryDto>(query, new { UserLanguage = userLanguage })).ToList();
    }

    public async Task<HashSet<Guid>?> GetSearchResult(string? searchTerm, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return null;

        const string query = @"
            SELECT
                operation.Articles.Id
            FROM
                operation.Articles
            WHERE
                operation.Articles.Title LIKE N'%@SearchTerm%'
            OR
                operation.Articles.Description LIKE N'%@SearchTerm%'
            UNION
            SELECT
                operation.ArticleTags.Id
            FROM
                operation.ArticleTags
            WHERE
                operation.ArticleTags.TagName LIKE N'%@SearchTerm%'
        ";

        await using var db = new SqlConnection(ConnectionString);
        var result = (await db.QueryAsync<Guid>(query, new { SearchTerm = searchTerm })).ToList();
        return new HashSet<Guid>(result);
    }

    public async Task<List<ArticleDataDto>> RetrieveArticleInfo(string userLanguage, HashSet<Guid> articleIds, CancellationToken cancellationToken = default)
    {
        var articleInfoList = await (
            from article in _operationDbContext.Articles
            join table in
                (from articleCategory in _operationDbContext.ArticleCategories
                join categoryName in _operationDbContext.ArticleCategoryNames
                    on articleCategory.Id equals categoryName.ArticleCategoryId
                join language in _operationDbContext.Languages
                    on categoryName.LanguageId equals language.Id
                select new
                {
                    categoryName.ArticleCategoryId,
                    categoryName.Name,
                    language.LangId
                }
                )
                on article.CategoryId equals table.ArticleCategoryId
            where table.LangId == userLanguage
            where articleIds.Contains(article.Id)
            select new ArticleDataDto
            {
                Id = article.Id,
                CategoryName = table.Name,
                Title = article.Title,
                Description = article.Description,
                IsPublished = article.IsPublished,
                ReadCount = article.ReadCount,
                TotalLikes = article.ArticleLikes
                    .Where(likes => likes.ArticleId == article.Id)
                    .Select(likes => likes.LikeCount)
                    .Sum(),
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                LanguageIso = article.LanguageIso
            }).ToListAsync(cancellationToken);

        return articleInfoList;
    }

    public async Task AddArticle(Guid userId, ArticleDataInputDto articleData, DateTime createdAt, CancellationToken cancellationToken = default)
    {
        var newArticle = new Article
        {
            Title = articleData.Title,
            Description = articleData.Description,
            IsPublished = false,
            ReadCount = 0,
            CreatedBy = userId,
            CreatedAt = createdAt,
            UserId = userId,
            LanguageIso = articleData.LanguageIso
        };

        await _operationDbContext.Articles.AddAsync(newArticle, cancellationToken);
        await _operationDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> RemoveArticle(Guid userId, Guid requestId, CancellationToken cancellationToken = default)
    {
        var articleData = await _operationDbContext.Articles
            .AsNoTracking()
            .Where(article => article.UserId == userId)
            .Where(article => article.Id == requestId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            return false;

        var articleLike = await _operationDbContext.ArticleLikes
            .AsNoTracking()
            .Where(like => like.UserId == userId)
            .Where(like => like.ArticleId == requestId)
            .SingleOrDefaultAsync(cancellationToken);

        var articleCount = await _operationDbContext.ArticleCounts
            .AsNoTracking()
            .Where(count => count.UserId == userId)
            .Where(count => count.ArticleId == requestId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLike is not null)
            _operationDbContext.ArticleLikes.Remove(articleLike);

        if (articleCount is not null)
            _operationDbContext.ArticleCounts.Remove(articleCount);

        _operationDbContext.Articles.Remove(articleData);
        await _operationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> UpdateArticleCount(Guid userId, Guid articleId, DateTime updatedAt, string ipAddress, CancellationToken cancellationToken = default)
    {
        var articleData = await _operationDbContext.Articles
            .Where(article => article.Id == articleId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            return false;

        var articleCount = await _operationDbContext.ArticleCounts
            .Where(count => count.ArticleId == articleId)
            .Where(count => count.IpAddress == ipAddress)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleCount is null)
        {
            var newArticleCount = new ArticleCount
            {
                UserId = articleData.UserId,
                ArticleId = articleData.Id,
                IpAddress = ipAddress,
                ReadCount = 1,
                CreatedAt = updatedAt,
                CreatedBy = userId
            };

            await _operationDbContext.ArticleCounts.AddAsync(newArticleCount, cancellationToken);
        }
        else
        {
            articleCount.ReadCount += 1;
            articleCount.ModifiedAt = updatedAt;
            articleCount.ModifiedBy = userId;

            _operationDbContext.ArticleCounts.Update(articleCount);
        }

        articleData.ReadCount += 1;
        articleData.ModifiedAt = updatedAt;
        articleData.ModifiedBy = userId;

        await _operationDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateArticleVisibility(Guid userId, Guid articleId, DateTime updatedAt, bool isPublished, CancellationToken cancellationToken = default)
    {
        var articleData = await _operationDbContext.Articles
            .Where(article => article.UserId == userId)
            .Where(article => article.Id == articleId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            return false;

        articleData.IsPublished = isPublished;
        articleData.ModifiedAt = updatedAt;
        articleData.ModifiedBy = userId;

        await _operationDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateArticleContent(Guid userId, Guid articleId, DateTime updatedAt, string? title, string? description, string? languageIso,
        CancellationToken cancellationToken = default)
    {
        var articleData = await _operationDbContext.Articles
            .Where(article => article.UserId == userId)
            .Where(article => article.Id == articleId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            return false;

        articleData.Title = title ?? articleData.Title;
        articleData.Description = description ?? articleData.Description;
        articleData.LanguageIso = languageIso ?? articleData.LanguageIso;
        articleData.UpdatedAt = updatedAt;
        articleData.ModifiedAt = updatedAt;
        articleData.ModifiedBy = userId;

        await _operationDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> UpdateArticleLikes(Guid userId, Guid articleId, DateTime updatedAt, int addToLikes, bool isAnonymousUser, string ipAddress,
        CancellationToken cancellationToken = default)
    {
        var articleData = await _operationDbContext.Articles
            .Where(article => article.Id == articleId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            return false;

        var articleLike = await _operationDbContext.ArticleLikes
            .Where(like => like.ArticleId == articleId)
            .WhereIfElse(isAnonymousUser,
                like => like.IpAddress == ipAddress,
                like => like.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLike is null)
        {
            await AddLikes(userId, articleData, updatedAt, addToLikes, ipAddress, cancellationToken);
        }
        else
        {
            UpdateLikes(userId, articleData, articleLike, addToLikes, updatedAt);
        }

        await _operationDbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    private async Task AddLikes(Guid userId, Article article, DateTime updatedAt, int addToLikes, string ipAddress, CancellationToken cancellationToken)
    {
        var likesLimit = userId == Guid.Empty ? MaxLikesForAnonymousUser : MaxLikesForLoggedUser;
        var likes = addToLikes > likesLimit ? likesLimit : addToLikes;

        var entity = new ArticleLike
        {
            ArticleId = article.Id,
            UserId = userId == Guid.Empty ? null : userId,
            IpAddress = ipAddress,
            LikeCount = likes,
            CreatedAt = updatedAt,
            CreatedBy = userId,
            ModifiedAt = null,
            ModifiedBy = null
        };

        article.TotalLikes += likes;
        article.ModifiedAt = updatedAt;
        article.ModifiedBy = userId == Guid.Empty ? null : userId;
        await _operationDbContext.ArticleLikes.AddAsync(entity, cancellationToken);
    }

    private void UpdateLikes(Guid? userId, Article article, ArticleLike articleLike, int likesToBeAdded, DateTime updatedAt)
    {
        var likesLimit = userId == Guid.Empty ? MaxLikesForAnonymousUser : MaxLikesForLoggedUser;
        var likes = likesToBeAdded > likesLimit ? likesLimit : likesToBeAdded;

        articleLike.LikeCount += likes;
        articleLike.ModifiedAt = updatedAt;
        articleLike.ModifiedBy = userId == Guid.Empty ? null : userId;

        article.TotalLikes += likes;
        article.ModifiedAt = updatedAt;
        article.ModifiedBy = userId == Guid.Empty ? null : userId;
        _operationDbContext.ArticleLikes.Update(articleLike);
    }
}