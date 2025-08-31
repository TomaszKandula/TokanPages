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
    /// <param name="query">Options</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetAllArticlesQueryResult> GetArticles(GetArticlesQuery query, bool noCache = false);

    /// <summary>
    /// Returns information for given article ID.
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetArticleInfoQueryResult> GetArticleInfo(Guid id, bool noCache = false);

    /// <summary>
    /// Returns single article
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetArticleQueryResult> GetArticle(Guid id, bool noCache = false);

    /// <summary>
    /// Returns single article
    /// </summary>
    /// <param name="title">Normalized and queryable article title</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetArticleQueryResult> GetArticle(string title, bool noCache = false);
}