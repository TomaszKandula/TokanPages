using System.Linq.Expressions;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public interface IArticlesRepository
{
    Task<Guid> GetArticleIdByTitle(string title, CancellationToken cancellationToken = default);

    Task<GetArticleOutputDto> GetArticle(Guid userId, Guid requestId, bool isAnonymousUser, string ipAddress, string userLanguage, CancellationToken cancellationToken = default);

    Task<List<ArticleDataDto>> GetArticleList(bool isPublished, string? searchTerm, Guid? categoryId, HashSet<Guid>? foundArticleIds, IDictionary<string, Expression<Func<ArticleDataDto, object>>> orderByExpressions, CancellationToken cancellationToken = default);

    Task<List<ArticleCategoryDto>> GetArticleCategories(string userLanguage, CancellationToken cancellationToken = default);
    
    Task<HashSet<Guid>?> GetSearchResult(string? searchTerm, CancellationToken cancellationToken = default);

    Task<List<ArticleDataDto>> RetrieveArticleInfo(string userLanguage, HashSet<Guid> articleIds, CancellationToken cancellationToken = default);

    Task AddNewArticle(Guid userId, ArticleDataInputDto articleData, DateTime createdAt, CancellationToken cancellationToken = default);

    Task<bool> RemoveArticle(Guid userId, Guid requestId, CancellationToken cancellationToken = default);

    Task<bool> UpdateArticleCount(Guid userId, Guid articleId, DateTime updatedAt, string ipAddress, CancellationToken cancellationToken = default);
}