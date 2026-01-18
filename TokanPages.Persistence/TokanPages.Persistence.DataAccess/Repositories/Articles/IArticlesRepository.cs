using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles;

public interface IArticlesRepository
{
    Task<Guid> GetArticleIdByTitle(string title, CancellationToken cancellationToken = default);

    Task<GetArticleOutput> GetArticle(Guid userId, Guid requestId, bool isAnonymousUser, string ipAddress, string userLanguage, CancellationToken cancellationToken = default);
}