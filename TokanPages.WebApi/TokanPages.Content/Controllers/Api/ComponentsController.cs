using Microsoft.AspNetCore.Mvc;
using MediatR;
using TokanPages.Backend.Application.Content.Components.Commands;
using TokanPages.Backend.Application.Content.Components.Queries;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Content.Controllers.Mappers;
using TokanPages.Content.Dto.Components;
using TokanPages.Persistence.Caching.Abstractions;

namespace TokanPages.Content.Controllers.Api;

/// <summary>
/// API endpoints definitions for content.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/content/[controller]/[action]")]
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
    /// <param name="name">Content name.</param>
    /// <param name="language">Language code (eng, pol, etc.).</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [ETagFilter]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "name", "language", "noCache" })]
    [ProducesResponseType(typeof(GetContentQueryResult), StatusCodes.Status200OK)]
    public async Task<GetContentQueryResult> GetContent(
        [FromQuery] string? language,
        [FromQuery] string name = "",
        [FromQuery] bool noCache = false)
        => await _contentCache.GetContent(language, name, noCache);

    /// <summary>
    /// Returns component's content.
    /// </summary>
    /// <param name="request">List of requested component's content for given language.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(RequestPageDataCommandResult), StatusCodes.Status200OK)]
    public async Task<RequestPageDataCommandResult> RequestPageData([FromBody] RequestPageDataDto request, [FromQuery] bool noCache = false) 
        => await _contentCache.GetPageContent(ComponentsMapper.MapToRequestPageDataCommand(request), noCache);
}