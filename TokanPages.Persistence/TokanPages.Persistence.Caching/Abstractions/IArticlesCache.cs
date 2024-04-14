using TokanPages.Backend.Application.Articles.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Articles cache contract
/// </summary>
public interface IArticlesCache
{
    /// <summary>
    /// Returns articles
    /// </summary>
    /// <param name="isPublished">If true, returns only published</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<List<GetArticlesQueryResult>> GetArticles(bool isPublished = true, bool noCache = false);

    /// <summary>
    /// Returns single article
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetArticleQueryResult> GetArticle(Guid id, bool noCache = false);
}