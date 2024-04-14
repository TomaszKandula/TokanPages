using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using MediatR;
using TokanPages.Backend.Application.Articles.Queries;
using TokanPages.Articles.Controllers.Mappers;
using TokanPages.Persistence.Caching.Abstractions;
using TokanPages.Articles.Dto.Articles;

namespace TokanPages.Articles.Controllers.Api;

/// <summary>
/// API endpoints definitions for articles.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ArticlesController : ApiBaseController
{
    private readonly IArticlesCache _articlesCache;

    /// <summary>
    /// Articles controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="articlesCache"></param>
    public ArticlesController(IMediator mediator, IArticlesCache articlesCache) 
        : base(mediator) => _articlesCache = articlesCache;

    /// <summary>
    /// Returns all written articles.
    /// </summary>
    /// <param name="isPublished">Use true to get only published articles.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object list.</returns>
    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(typeof(IEnumerable<GetAllArticlesQueryResult>), StatusCodes.Status200OK)]
    public async Task<IEnumerable<GetAllArticlesQueryResult>> GetAllArticles([FromQuery] bool isPublished = true, bool noCache = false)
        => await _articlesCache.GetArticles(isPublished, noCache);

    /// <summary>
    /// Returns single article.
    /// </summary>
    /// <param name="id">Article ID.</param>
    /// <param name="noCache">Enable/disable REDIS cache.</param>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [ProducesResponseType(typeof(GetArticleQueryResult), StatusCodes.Status200OK)]
    public async Task<GetArticleQueryResult> GetArticle([FromRoute] Guid id, [FromQuery] bool noCache = false)
        => await _articlesCache.GetArticle(id, noCache);

    /// <summary>
    /// Adds new article to the database.
    /// </summary>
    /// <param name="payload">Article data.</param>
    /// <returns>Guid.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<Guid> AddArticle([FromBody] AddArticleDto payload) 
        => await Mediator.Send(ArticlesMapper.MapToAddArticleCommand(payload));

    /// <summary>
    /// Updates existing article content.
    /// </summary>
    /// <param name="payload">Article data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateArticleContent([FromBody] UpdateArticleContentDto payload)
        => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payload));

    /// <summary>
    /// Updates existing article count value.
    /// </summary>
    /// <param name="payload">Article data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateArticleCount([FromBody] UpdateArticleCountDto payload)
        => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payload));

    /// <summary>
    /// Updates existing article likes.
    /// </summary>
    /// <param name="payload">Article data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateArticleLikes([FromBody] UpdateArticleLikesDto payload)
        => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payload));

    /// <summary>
    /// Update existing article visibility.
    /// </summary>
    /// <param name="payload">Article data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> UpdateArticleVisibility([FromBody] UpdateArticleVisibilityDto payload)
        => await Mediator.Send(ArticlesMapper.MapToUpdateArticleCommand(payload));

    /// <summary>
    /// Removes existing article.
    /// </summary>
    /// <param name="payload">Article data.</param>
    /// <returns>MediatR unit value.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> RemoveArticle([FromBody] RemoveArticleDto payload)
        => await Mediator.Send(ArticlesMapper.MapToRemoveArticleCommand(payload));
}