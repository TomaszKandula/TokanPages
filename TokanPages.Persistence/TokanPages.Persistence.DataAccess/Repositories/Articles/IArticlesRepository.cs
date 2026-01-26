using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public interface IArticlesRepository
{
    Task<Guid> GetArticleIdByTitle(string title);

    Task<GetArticleOutputDto?> GetArticle(Guid userId, Guid requestId, bool isAnonymousUser, string ipAddress, string userLanguage);

    Task<List<ArticleDataDto>> GetArticleList(bool isPublished, string? searchTerm, Guid? categoryId, HashSet<Guid>? filterById, ArticlePageInfoDto pageInfo);

    Task<List<ArticleCategoryDto>> GetArticleCategories(string userLanguage);

    Task<HashSet<Guid>?> GetSearchResult(string? searchTerm);

    Task<List<ArticleDataDto>> RetrieveArticleInfo(string userLanguage, HashSet<Guid> articleIds);

    Task<List<ArticleCount>> GetArticleCount(string ipAddress, Guid articleId);

    Task CreateArticle(Guid userId, ArticleDataInputDto data, DateTime createdAt, CancellationToken cancellationToken = default);

    Task<bool> RemoveArticle(Guid userId, Guid requestId, CancellationToken cancellationToken = default);

    Task<bool> CreateArticleCount(Guid userId, Guid articleId, DateTime updatedAt, string ipAddress, CancellationToken cancellationToken = default);
    
    Task<bool> UpdateArticleCount(Guid userId, Guid articleId, int count, DateTime updatedAt, string ipAddress, CancellationToken cancellationToken = default);

    Task<bool> UpdateArticleVisibility(Guid userId, Guid articleId, DateTime updatedAt, bool isPublished, CancellationToken cancellationToken = default);

    Task<bool> UpdateArticleContent(Guid userId, Guid articleId, DateTime updatedAt, string? title, string? description, string? languageIso, CancellationToken cancellationToken = default);

    Task<bool> UpdateArticleLikes(Guid userId, Guid articleId, DateTime updatedAt, int addToLikes, bool isAnonymousUser, string ipAddress, CancellationToken cancellationToken = default);
}