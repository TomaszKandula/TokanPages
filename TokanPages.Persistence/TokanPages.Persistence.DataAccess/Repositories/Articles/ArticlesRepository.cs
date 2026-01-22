using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public class ArticlesRepository : IArticlesRepository
{
    private readonly OperationDbContext _operationDbContext;

    //TODO: replace IConfiguration with IOption
    private readonly IConfiguration _configuration;
    private int MaxLikesForAnonymousUser => _configuration.GetValue<int>("Limit_Likes_Anonymous");
    private int MaxLikesForLoggedUser => _configuration.GetValue<int>("Limit_Likes_User");
    private string ConnectionString => _configuration.GetValue<string>("Db_DatabaseContext") ?? "";

    public ArticlesRepository(OperationDbContext operationDbContext, IConfiguration configuration)
    {
        _operationDbContext = operationDbContext;
        _configuration = configuration;
    }

    public async Task<Guid> GetArticleIdByTitle(string title)
    {
        const string query = "SELECT operation.Articles.Id FROM Operations.Articles WHERE Operations.Articles.Title = @Title";

        await using var db = new SqlConnection(ConnectionString);
        return await db.QuerySingleOrDefaultAsync<Guid>(query, new { Title = title.Replace("-", " ").ToLower() });
    }

    public async Task<GetArticleOutputDto?> GetArticle(Guid userId, Guid requestId, bool isAnonymousUser, string ipAddress, string userLanguage)
    {
        const string queryArticleData = @"
            SELECT
                operation.Articles.Id,
                operation.Articles.UserId,
                operation.Articles.Title,
                operation.Articles.Description,
                operation.Articles.IsPublished,
                operation.Articles.CreatedAt,
                operation.Articles.UpdatedAt,
                operation.Articles.ReadCount,
                operation.Articles.LanguageIso,
                operation.ArticleCategoryNames.Name AS CategoryName
            FROM
                operation.Articles
            LEFT JOIN
                operation.ArticleCategories ON operation.Articles.CategoryId = operation.ArticleCategories.Id
            LEFT JOIN
                operation.ArticleCategoryNames ON operation.ArticleCategories.Id = operation.ArticleCategoryNames.ArticleCategoryId
            LEFT JOIN
                operation.Languages ON operation.ArticleCategoryNames.LanguageId = operation.Languages.Id
            WHERE
                operation.Languages.LangId = @LanguageId
            AND
                operation.Articles.Id = @RequestId
        ";

        await using var db = new SqlConnection(ConnectionString);
        var queryArticleDataParams = new { RequestId = requestId, LanguageId = userLanguage };
        var articleData = await db.QuerySingleOrDefaultAsync<ArticleBaseDto>(queryArticleData, queryArticleDataParams);
        if (articleData is null)
            return null;

        const string queryArticleLikes = @"
            SELECT
                CASE WHEN 
                    SUM(operation.ArticleLikes.LikeCount) IS NULL 
                THEN 0 ELSE 
                    SUM(operation.ArticleLikes.LikeCount) 
                END
            FROM
                operation.ArticleLikes
            WHERE
                operation.ArticleLikes.ArticleId = @RequestId
        ";

        const string filterAnonymouse = "\nAND operation.ArticleLikes.IpAddress = @IpAddress AND operation.ArticleLikes.UserId IS NULL";
        const string filterLoggedUser = "\nAND operation.ArticleLikes.UserId = @UserId";

        var queryFilteredLikes = isAnonymousUser ? $"{queryArticleLikes}{filterAnonymouse}" : $"{queryArticleLikes}{filterLoggedUser}";
        var queryFilteredParams = new { RequestId = requestId, IpAddress = ipAddress, UserId = userId };
        var queryArticleParams = new { RequestId = requestId };

        var userLikes = await db.QuerySingleOrDefaultAsync<int>(queryFilteredLikes, queryFilteredParams);
        var totalLikes = await db.QuerySingleOrDefaultAsync<int>(queryArticleLikes, queryArticleParams);

        const string queryUserData = @"
            SELECT
                operation.Users.Id AS UserId,
                operation.Users.UserAlias,
                operation.UserInformation.UserImageName AS AvatarName,
                operation.UserInformation.FirstName,
                operation.UserInformation.LastName,
                operation.UserInformation.UserAboutText AS ShortBio,
                operation.UserInformation.CreatedAt AS Registered
            FROM
                operation.Users
            LEFT JOIN
                operation.UserInformation ON operation.Users.Id = operation.UserInformation.UserId
            WHERE
                operation.Users.Id = @UserId
        ";

        var queryUserParams = new { UserId = userId };
        var userDto = await db.QuerySingleOrDefaultAsync<GetUserDto>(queryUserData, queryUserParams);

        const string queryTags = @"
            SELECT
                operation.ArticleTags.TagName
            FROM
                operation.ArticleTags
            LEFT JOIN
                operation.Articles ON operation.ArticleTags.ArticleId = operation.Articles.Id
            WHERE
                operation.Articles.Id = @RequestId
        ";

        var queryTagParams = new { RequestId = requestId };
        var tags = (await db.QueryAsync<string>(queryTags, queryTagParams)).ToArray();

        return new GetArticleOutputDto
        {
            Id = articleData.Id,
            Title = articleData.Title,
            CategoryName = articleData.CategoryName,
            Description = articleData.Description,
            IsPublished = articleData.IsPublished,
            CreatedAt = articleData.CreatedAt,
            UpdatedAt = articleData.UpdatedAt,
            ReadCount = articleData.ReadCount,
            LanguageIso = articleData.LanguageIso,
            TotalLikes = totalLikes,
            UserLikes = userLikes,
            Author = userDto,
            Tags = tags
        };
    }

    public async Task<List<ArticleDataDto>> GetArticleList(bool isPublished, string? searchTerm, Guid? categoryId, HashSet<Guid>? filterById, ArticlePageInfoDto pageInfo)
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
            query += $"\nAND operation.Articles.Id IN {filterById.ToArray()}";

        query += $"\nORDER BY {pageInfo.OrderByColumn} {pageInfo.OrderByAscending}";

        var skipCount = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
        query += $"\nOFFSET {skipCount} ROWS FETCH NEXT {pageInfo.PageSize} ROWS ONLY";

        await using var db = new SqlConnection(ConnectionString);
        var articles = (await db.QueryAsync<ArticleDataDto>(query)).ToList();

        return articles;
    }

    public async Task<List<ArticleCategoryDto>> GetArticleCategories(string userLanguage)
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

    public async Task<HashSet<Guid>?> GetSearchResult(string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return null;

        const string query = @"
            SELECT
                operation.Articles.Id
            FROM
                operation.Articles
            WHERE
                operation.Articles.Title LIKE CONCAT('%', @SearchTerm, '%')
            OR
                operation.Articles.Description LIKE CONCAT('%', @SearchTerm, '%')
            UNION ALL
            SELECT
                operation.ArticleTags.ArticleId
            FROM
                operation.ArticleTags
            WHERE
                operation.ArticleTags.TagName LIKE CONCAT('%', @SearchTerm, '%')
        ";

        await using var db = new SqlConnection(ConnectionString);
        var result = (await db.QueryAsync<Guid>(query, new { SearchTerm = searchTerm })).ToList();
        return new HashSet<Guid>(result);
    }

    public async Task<List<ArticleDataDto>> RetrieveArticleInfo(string userLanguage, HashSet<Guid> articleIds)
    {
        const string query = @"
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
                Categories.Name,
                Likes.TotalLikes
            FROM
                operation.Articles
            LEFT JOIN (
                SELECT
                    operation.ArticleCategoryNames.ArticleCategoryId,
                    operation.ArticleCategoryNames.Name,
                    operation.Languages.LangId
                FROM
                    operation.ArticleCategories
                LEFT JOIN
                    operation.ArticleCategoryNames ON operation.ArticleCategories.Id = operation.ArticleCategoryNames.ArticleCategoryId
                LEFT JOIN
                    operation.Languages ON operation.ArticleCategoryNames.LanguageId = operation.Languages.Id
            ) AS Categories
            ON 
                operation.Articles.CategoryId = Categories.ArticleCategoryId
            LEFT JOIN (
                SELECT
                    operation.ArticleLikes.ArticleId,
                    SUM(LikeCount) AS TotalLikes
                FROM
                    operation.ArticleLikes
                GROUP BY
                    operation.ArticleLikes.ArticleId
            ) AS Likes
            ON
                operation.Articles.Id = Likes.ArticleId
            WHERE
                Categories.LangId = @UserLanguage
            AND
                operation.Articles.Id IN @ArticleIds
        ";

        await using var db = new SqlConnection(ConnectionString);
        var articleInfoList = (await db.QueryAsync<ArticleDataDto>(query, new
        {
            UserLanguage = userLanguage,
            ArticleIds = articleIds.ToArray()
        })).ToList();

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