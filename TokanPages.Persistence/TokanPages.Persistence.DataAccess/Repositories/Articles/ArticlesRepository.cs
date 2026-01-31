using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Persistence.DataAccess.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public class ArticlesRepository : IArticlesRepository
{
    private readonly IDbOperations _dbOperations;

    private readonly AppSettingsModel _appSettings;

    public ArticlesRepository(IOptions<AppSettingsModel> appSettings, IDbOperations dbOperations)
    {
        _appSettings = appSettings.Value;
        _dbOperations = dbOperations;
    }

    public async Task<Guid> GetArticleIdByTitle(string title)
    {
        var filterBy = new { Title = title.Replace("-", " ").ToLower() };
        var data = (await _dbOperations.Retrieve<Article>(filterBy)).SingleOrDefault();
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

        await using var db = new SqlConnection(_appSettings.DbDatabaseContext);
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

        await using var db = new SqlConnection(_appSettings.DbDatabaseContext);
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

        await using var db = new SqlConnection(_appSettings.DbDatabaseContext);
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

        await using var db = new SqlConnection(_appSettings.DbDatabaseContext);
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

        await using var db = new SqlConnection(_appSettings.DbDatabaseContext);
        var articleInfoList = (await db.QueryAsync<ArticleDataDto>(query, new
        {
            UserLanguage = userLanguage,
            ArticleIds = articleIds.ToArray()
        })).ToList();

        return articleInfoList;
    }

    public async Task<List<ArticleCount>> GetArticleCount(string ipAddress, Guid articleId)
    {
        var filterBy = new { ArticleId = articleId, IpAddress = ipAddress };
        return (await _dbOperations.Retrieve<ArticleCount>(filterBy)).ToList();
    }

    public async Task<ArticleLike?> GetArticleLikes(bool isAnonymousUser, Guid userId, Guid articleId, string ipAddress)
    {
        return isAnonymousUser 
            ? (await _dbOperations.Retrieve<ArticleLike>(new { ArticleId = articleId, IpAddress = ipAddress })).SingleOrDefault()
            : (await _dbOperations.Retrieve<ArticleLike>(new { ArticleId = articleId, UserId = userId })).SingleOrDefault();
    }

    public async Task<bool> CreateArticleLikes(Guid userId, Guid articleId, string ipAddress, int likes, DateTime createdAt, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new ArticleLike
            {
                Id =  Guid.NewGuid(),
                UserId = userId,
                ArticleId = articleId,
                IpAddress = ipAddress,
                LikeCount = likes,
                CreatedAt = createdAt,
                CreatedBy = userId,
                ModifiedAt = null,
                ModifiedBy = null
            };

            await _dbOperations.Insert(entity, cancellationToken);
        }
        catch
        {
            return false;    
        }

        return true;
    }

    public async Task<bool> CreateArticle(Guid userId, ArticleDataInputDto data, DateTime createdAt, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new Article
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Title = data.Title,
                Description = data.Description,
                IsPublished = false,
                CreatedBy = userId,
                CreatedAt = createdAt,
                LanguageIso = data.LanguageIso
            };

            await _dbOperations.Insert(entity, cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> RemoveArticle(Guid userId, Guid requestId, CancellationToken cancellationToken = default)
    {
        try
        {
            var articleLikes = new { ArticleId = requestId, UserId =  userId };
            var articleCounts = new { ArticleId = requestId, UserId =  userId };
            var articleTags = new { ArticleId = requestId };
            var articles = new { Id = requestId, UserId =  userId };

            await _dbOperations.Delete<ArticleLike>(articleLikes, cancellationToken);
            await _dbOperations.Delete<ArticleCount>(articleCounts, cancellationToken);
            await _dbOperations.Delete<ArticleTag>(articleTags, cancellationToken);
            await _dbOperations.Delete<Article>(articles, cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> CreateArticleCount(Guid userId, Guid articleId, DateTime updatedAt, string ipAddress, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = new ArticleCount
            {
                Id = Guid.NewGuid(),
                ArticleId = articleId,
                UserId = userId,
                IpAddress = ipAddress,
                ReadCount = 1,
                CreatedBy = userId,
                CreatedAt = updatedAt
            };

            await _dbOperations.Insert(entity, cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateArticleCount(Guid userId, Guid articleId, int count, DateTime updatedAt, string ipAddress, CancellationToken cancellationToken = default)
    {
        try 
        {
            var updateBy = new { ReadCount = count, ModifiedAt = updatedAt, ModifiedBy = userId };
            var filterBy = new { ArticleId = articleId, IpAddress = ipAddress };
            await _dbOperations.Update<ArticleCount>(updateBy, filterBy, cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateArticleVisibility(Guid userId, Guid articleId, DateTime updatedAt, bool isPublished, CancellationToken cancellationToken = default)
    {
        try
        {
            var updateBy = new
            {
                IsPublished = isPublished,
                ModifiedAt = updatedAt,
                ModifiedBy = userId,
            };

            var filterBy = new
            {
                ArticleId = articleId,
                UserId = userId
            };

            await _dbOperations.Update<Article>(updateBy, filterBy, cancellationToken);
        }
        catch
        {
            return false;
        }

        return true;
    }

    public async Task<bool> UpdateArticleContent(Guid userId, Guid articleId, DateTime updatedAt, string? title, string? description, string? languageIso,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var updateBy = new
            {
                Title = title,
                Description = description,
                LanguageIso = languageIso,
                UpdatedAt = updatedAt,
                ModifiedAt = updatedAt,
                ModifiedBy = userId
            };

            var filterBy = new
            {
                UserId = userId,
                ArticleId = articleId
            };

            await _dbOperations.Update<Article>(updateBy, filterBy, cancellationToken);
        }
        catch
        {
            return false;    
        }

        return true;
    }

    public async Task<bool> UpdateArticleLikes(Guid userId, Guid articleId, DateTime updatedAt, int addToLikes, bool isAnonymousUser, string ipAddress,
        CancellationToken cancellationToken = default)
    {
        var updateBy = new
        {
            LikeCount = addToLikes,
            UpdatedAt = updatedAt
        };

        try
        {
            if (isAnonymousUser)
            {
                await _dbOperations.Update<ArticleLike>(updateBy, new { ArticleId = articleId, IpAddress = ipAddress }, cancellationToken);
            }
            else
            {
                await _dbOperations.Update<ArticleLike>(updateBy, new { ArticleId = articleId, UserId = userId }, cancellationToken);    
            }
        }
        catch
        {
            return false;
        }

        return true;
    }
}