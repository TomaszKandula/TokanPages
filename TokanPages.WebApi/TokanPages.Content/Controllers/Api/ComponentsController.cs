using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Content.Components.Queries;
using TokanPages.Persistence.Caching.Abstractions;

namespace TokanPages.Content.Controllers.Api;

/// <summary>
/// API endpoints definitions for content.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class ComponentsController : ApiBaseController
{
    private readonly IContentCache _contentCache;

    /// <summary>
    /// Content controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="contentCache">REDIS cache instance.</param>
    public ComponentsController(IMediator mediator, IContentCache contentCache) 
        : base(mediator) => _contentCache = contentCache;

    /// <summary>
    /// Returns component content manifest.
    /// </summary>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetContentManifestQueryResult), StatusCodes.Status200OK)]
    public async Task<GetContentManifestQueryResult> GetManifest([FromQuery] bool noCache = false) 
        => await _contentCache.GetContentManifest(noCache);

    /// <summary>
    /// Returns component/document content.
    /// </summary>
    /// <param name="type">Content type (component, document).</param>
    /// <param name="name">Content name.</param>
    /// <param name="language">Language code (eng, pol, etc.).</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetContentQueryResult), StatusCodes.Status200OK)]
    public async Task<GetContentQueryResult> GetContent(
        [FromQuery] string? language,
        [FromQuery] string type = "",
        [FromQuery] string name = "",
        [FromQuery] bool noCache = false)
        => await _contentCache.GetContent(language, type, name, noCache);
}