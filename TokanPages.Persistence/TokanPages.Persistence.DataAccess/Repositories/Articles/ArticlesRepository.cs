using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public class ArticlesRepository : RepositoryBase, IArticlesRepository
{
    private readonly IDateTimeService _dateTimeService;

    public ArticlesRepository(IDbOperations dbOperations, IOptions<AppSettingsModel> appSettings, IDateTimeService dateTimeService) 
        : base(dbOperations, appSettings) => _dateTimeService = dateTimeService;

    public async Task<Guid> GetArticleIdByTitle(string title)
    {
        var filterBy = new
        {
            Title = title.Replace("-", " ").ToLower()
        };

        var result = await DbOperations.Retrieve<Article>(filterBy);
        var data = result.SingleOrDefault();

        return data?.Id ?? Guid.Empty;
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
                operation.Articles.LanguageIso,
                operation.ArticleCategoryNames.Name AS CategoryName,
                Counts.ReadCount
            FROM
                operation.Articles
            LEFT JOIN
                operation.ArticleCategories ON operation.Articles.CategoryId = operation.ArticleCategories.Id
            LEFT JOIN
                operation.ArticleCategoryNames ON operation.ArticleCategories.Id = operation.ArticleCategoryNames.ArticleCategoryId
            LEFT JOIN
                operation.Languages ON operation.ArticleCategoryNames.LanguageId = operation.Languages.Id
            LEFT JOIN (
                SELECT
                    operation.ArticleCounts.ArticleId,
                    SUM(ReadCount) AS ReadCount
                FROM
                    operation.ArticleCounts
                GROUP BY
                    operation.ArticleCounts.ArticleId
            ) AS Counts
            ON
                operation.Articles.Id = Counts.ArticleId
            WHERE
                operation.Languages.LangId = @LanguageId
            AND
                operation.Articles.Id = @RequestId
        ";

        var queryArticleDataParams = new
        {
            RequestId = requestId,
            LanguageId = userLanguage
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var articleData = await connection.QuerySingleOrDefaultAsync<ArticleBaseDto>(queryArticleData, queryArticleDataParams);
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

        var queryFilteredLikes = isAnonymousUser 
            ? $"{queryArticleLikes}{filterAnonymouse}"
            : $"{queryArticleLikes}{filterLoggedUser}";

        var queryFilteredParams = new
        {
            RequestId = requestId,
            IpAddress = ipAddress,
            UserId = userId
        };

        var queryArticleParams = new
        {
            RequestId = requestId
        };

        var userLikes = await connection.QuerySingleOrDefaultAsync<int>(queryFilteredLikes, queryFilteredParams);
        var totalLikes = await connection.QuerySingleOrDefaultAsync<int>(queryArticleLikes, queryArticleParams);

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

        var queryUserParams = new
        {
            UserId = userId
        };

        var userDto = await connection.QuerySingleOrDefaultAsync<GetUserDto>(queryUserData, queryUserParams);

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

        var queryTagParams = new
        {
            RequestId = requestId
        };

        var result = await connection.QueryAsync<string>(queryTags, queryTagParams);
        var tags = result.ToArray();

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
                operation.Articles.CreatedAt,
                operation.Articles.UpdatedAt,
                operation.Articles.LanguageIso,
                Likes.TotalLikes,
                Counts.ReadCount,
                COUNT(*) OVER() AS CountOver
            FROM 
                operation.Articles 
            LEFT JOIN 
                operation.ArticleCategories ON operation.Articles.CategoryId = operation.ArticleCategories.Id
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
            LEFT JOIN (
                SELECT
                    operation.ArticleCounts.ArticleId,
                    SUM(ReadCount) AS ReadCount
                FROM
                    operation.ArticleCounts
                GROUP BY
                    operation.ArticleCounts.ArticleId
            ) AS Counts
            ON
                operation.Articles.Id = Counts.ArticleId
            WHERE 
                operation.Articles.IsPublished = 1";

        if (categoryId != null && categoryId != Guid.Empty)
            query += $"\nAND operation.Articles.CategoryId = '{categoryId}'";

        if (filterById != null && filterById.Count != 0)
            query += $"\nAND operation.Articles.Id IN {filterById.ToArray()}";

        query += $"\nORDER BY {pageInfo.OrderByColumn} {pageInfo.OrderByAscending}";

        var skipCount = (pageInfo.PageNumber - 1) * pageInfo.PageSize;
        query += $"\nOFFSET {skipCount} ROWS FETCH NEXT {pageInfo.PageSize} ROWS ONLY";

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var data = await connection.QueryAsync<ArticleDataDto>(query);
        var result = data.ToList();

        return result;
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

        var parameters = new
        {
            UserLanguage = userLanguage
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var data = await connection.QueryAsync<ArticleCategoryDto>(query, parameters);
        var result = data.ToList();

        return result;
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

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var data = await connection.QueryAsync<Guid>(query, new { SearchTerm = searchTerm });
        var result = data.ToList();

        return new HashSet<Guid>(result);
    }

    public async Task<List<ArticleDataDto>> GetArticleInfo(string userLanguage, HashSet<Guid> articleIds)
    {
        const string query = @"
            SELECT
                operation.Articles.Id,
                operation.Articles.Title,
                operation.Articles.Description,
                operation.Articles.IsPublished,
                operation.Articles.CreatedAt,
                operation.Articles.UpdatedAt,
                operation.Articles.LanguageIso,
                Categories.Name,
                Likes.TotalLikes,
                Counts.ReadCount
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
            LEFT JOIN (
                SELECT
                    operation.ArticleCounts.ArticleId,
                    SUM(ReadCount) AS ReadCount
                FROM
                    operation.ArticleCounts
                GROUP BY
                    operation.ArticleCounts.ArticleId
            ) AS Counts
            ON
                operation.Articles.Id = Counts.ArticleId
            WHERE
                Categories.LangId = @UserLanguage
            AND
                operation.Articles.Id IN @ArticleIds
        ";

        var parameters = new
        {
            UserLanguage = userLanguage,
            ArticleIds = articleIds.ToArray()
        };

        await using var connection = new SqlConnection(AppSettings.DbDatabaseContext);
        var data = await connection.QueryAsync<ArticleDataDto>(query, parameters);
        var result = data.ToList();

        return result;
    }

    public async Task<List<ArticleCount>> GetArticleCount(string ipAddress, Guid articleId)
    {
        var filterBy = new
        {
            ArticleId = articleId, IpAddress = ipAddress
        };

        var data = await DbOperations.Retrieve<ArticleCount>(filterBy);
        var result = data.ToList();

        return result;
    }

    public async Task<ArticleLike?> GetArticleLikes(bool isAnonymousUser, Guid userId, Guid articleId, string ipAddress)
    {
        dynamic filterBy = isAnonymousUser
            ? new { ArticleId = articleId, IpAddress = ipAddress }
            : new { ArticleId = articleId, UserId = userId };

        var data = await DbOperations.Retrieve<ArticleLike>(filterBy) as IEnumerable<ArticleLike>;
        var result = data?.SingleOrDefault();

        return result;
    }

    public async Task CreateArticleLikes(Guid userId, Guid articleId, string ipAddress, int likes)
    {
        var entity = new ArticleLike
        {
            Id =  Guid.NewGuid(),
            UserId = userId,
            ArticleId = articleId,
            IpAddress = ipAddress,
            LikeCount = likes,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = userId,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await DbOperations.Insert(entity);
    }

    public async Task CreateArticle(Guid userId, ArticleDataInputDto data)
    {
        var entity = new Article
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Title = data.Title,
            Description = data.Description,
            IsPublished = false,
            CreatedBy = userId,
            CreatedAt = _dateTimeService.Now,
            LanguageIso = data.LanguageIso
        };

        await DbOperations.Insert(entity);
    }

    // TODO: optimize following method
    public async Task RemoveArticle(Guid userId, Guid requestId)
    {
        var articleLikes = new { ArticleId = requestId, UserId =  userId };
        var articleCounts = new { ArticleId = requestId, UserId =  userId };
        var articleTags = new { ArticleId = requestId };
        var articles = new { Id = requestId, UserId =  userId };

        await DbOperations.Delete<ArticleLike>(articleLikes);
        await DbOperations.Delete<ArticleCount>(articleCounts);
        await DbOperations.Delete<ArticleTag>(articleTags);
        await DbOperations.Delete<Article>(articles);
    }

    public async Task CreateArticleCount(Guid userId, Guid articleId, string ipAddress)
    {
        var entity = new ArticleCount
        {
            Id = Guid.NewGuid(),
            ArticleId = articleId,
            UserId = userId,
            IpAddress = ipAddress,
            ReadCount = 1,
            CreatedBy = userId,
            CreatedAt = _dateTimeService.Now
        };

        await DbOperations.Insert(entity);
    }

    public async Task UpdateArticleCount(Guid userId, Guid articleId, int count, string ipAddress)
    {
        var updateBy = new
        {
            ReadCount = count,
            ModifiedAt = _dateTimeService.Now,
            ModifiedBy = userId
        };
        
        var filterBy = new
        {
            ArticleId = articleId,
            IpAddress = ipAddress
        };

        await DbOperations.Update<ArticleCount>(updateBy, filterBy);
    }

    public async Task UpdateArticleVisibility(Guid userId, Guid articleId, bool isPublished)
    {
        var updateBy = new
        {
            IsPublished = isPublished,
            ModifiedAt = _dateTimeService.Now,
            ModifiedBy = userId,
        };

        var filterBy = new
        {
            ArticleId = articleId,
            UserId = userId
        };

        await DbOperations.Update<Article>(updateBy, filterBy);
    }

    public async Task UpdateArticleContent(Guid userId, Guid articleId, string? title, string? description, string? languageIso)
    {
        var timestamp = _dateTimeService.Now;
        var updateBy = new
        {
            Title = title,
            Description = description,
            LanguageIso = languageIso,
            UpdatedAt = timestamp,
            ModifiedAt = timestamp,
            ModifiedBy = userId
        };

        var filterBy = new
        {
            UserId = userId,
            ArticleId = articleId
        };

        await DbOperations.Update<Article>(updateBy, filterBy);
    }

    public async Task UpdateArticleLikes(Guid userId, Guid articleId, int addToLikes, bool isAnonymousUser, string ipAddress)
    {
        var updateBy = new
        {
            LikeCount = addToLikes,
            UpdatedAt = _dateTimeService.Now
        };

        dynamic filterBy = isAnonymousUser 
            ? new { ArticleId = articleId, IpAddress = ipAddress } 
            : new { ArticleId = articleId, UserId = userId };

        await DbOperations.Update<ArticleLike>(updateBy, filterBy);
    }
}