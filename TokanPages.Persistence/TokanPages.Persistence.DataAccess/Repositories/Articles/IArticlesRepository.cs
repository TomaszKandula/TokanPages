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

    Task<ArticleLike?> GetArticleLikes(bool isAnonymousUser, Guid userId, Guid articleId, string ipAddress);

    Task<bool> CreateArticleLikes(Guid userId, Guid articleId, string ipAddress, int likes);

    Task<bool> CreateArticle(Guid userId, ArticleDataInputDto data);

    Task<bool> RemoveArticle(Guid userId, Guid requestId);

    Task<bool> CreateArticleCount(Guid userId, Guid articleId, string ipAddress);
    
    Task<bool> UpdateArticleCount(Guid userId, Guid articleId, int count, string ipAddress);

    Task<bool> UpdateArticleVisibility(Guid userId, Guid articleId, bool isPublished);

    Task<bool> UpdateArticleContent(Guid userId, Guid articleId, string? title, string? description, string? languageIso);

    Task<bool> UpdateArticleLikes(Guid userId, Guid articleId, int addToLikes, bool isAnonymousUser, string ipAddress);
}