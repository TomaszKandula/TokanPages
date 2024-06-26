using TokanPages.Backend.Application.Content.Components.Queries;

namespace TokanPages.Persistence.Caching.Abstractions;

/// <summary>
/// Content cache contract
/// </summary>
public interface IContentCache
{
    /// <summary>
    /// Returns current component content manifest
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetContentManifestQueryResult> GetContentManifest(bool noCache = false);

    /// <summary>
    /// Returns content
    /// </summary>
    /// <param name="type">Content type (document or component)</param>
    /// <param name="name">Content name</param>
    /// <param name="language">Content language (pol, eng, esp, etc.)</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetContentQueryResult> GetContent(string? language, string type = "", string name = "", bool noCache = false);
}