using TokanPages.Backend.Application.Content.Components.Commands;
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
    /// <param name="name">Content name</param>
    /// <param name="language">Content language (pol, eng, esp, etc.)</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    Task<GetContentQueryResult> GetContent(string? language, string name = "", bool noCache = false);

    /// <summary>
    /// Returns list of component's content
    /// </summary>
    /// <param name="request">Language and list of requested components.</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Component's content</returns>
    Task<RequestPageDataCommandResult> GetPageContent(RequestPageDataCommand request, bool noCache = false);
}