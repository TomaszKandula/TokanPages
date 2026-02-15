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

    Task<List<ArticleDataDto>> GetArticleInfo(string userLanguage, HashSet<Guid> articleIds);

    Task<List<ArticleCount>> GetArticleCount(string ipAddress, Guid articleId);

    Task<ArticleLike?> GetArticleLikes(bool isAnonymousUser, Guid userId, Guid articleId, string ipAddress);

    Task CreateArticleLikes(Guid userId, Guid articleId, string ipAddress, int likes);

    Task CreateArticle(Guid userId, ArticleDataInputDto data);

    Task RemoveArticle(Guid userId, Guid requestId);

    Task CreateArticleCount(Guid userId, Guid articleId, string ipAddress);
    
    Task UpdateArticleCount(Guid userId, Guid articleId, int count, string ipAddress);

    Task UpdateArticleVisibility(Guid userId, Guid articleId, bool isPublished);

    Task UpdateArticleContent(Guid userId, Guid articleId, string? title, string? description, string? languageIso);

    Task UpdateArticleLikes(Guid userId, Guid articleId, int addToLikes, bool isAnonymousUser, string ipAddress);
}