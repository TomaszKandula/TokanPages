namespace TokanPages.WebApi.Controllers.Api;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Cqrs.Handlers.Queries.Content;
using Services.Caching.Content;
using MediatR;

/// <summary>
/// API endpoints definitions for content
/// </summary>
[ApiVersion("1.0")]
public class ContentController : ApiBaseController
{
    private readonly IContentCache _contentCache;

    /// <summary>
    /// Content controller
    /// </summary>
    /// <param name="mediator">Mediator instance</param>
    /// <param name="contentCache"></param>
    public ContentController(IMediator mediator, IContentCache contentCache) 
        : base(mediator) => _contentCache = contentCache;

    /// <summary>
    /// Returns component/document content
    /// </summary>
    /// <param name="type">Content type (component, document)</param>
    /// <param name="name">Content name</param>
    /// <param name="language">Language code (eng, pol, etc.)</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>Object</returns>
    [HttpGet]
    [ProducesResponseType(typeof(GetContentQueryResult), StatusCodes.Status200OK)]
    public async Task<GetContentQueryResult> GetContent([FromQuery] string type, string name, string? language, bool noCache = false)
        => await _contentCache.GetContent(type, name, language, noCache);
}