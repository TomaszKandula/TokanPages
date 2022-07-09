namespace TokanPages.WebApi.Controllers.Api;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Enums;
using Services.Caching.Assets;
using Backend.Shared.Attributes;
using Backend.Cqrs.Handlers.Queries.Assets;
using MediatR;

/// <summary>
/// API endpoints definitions for assets
/// </summary>
[ApiVersion("1.0")]
public class AssetsController : ApiBaseController
{
    private readonly IAssetsCache _assetsCache;

    /// <summary>
    /// Assets controller
    /// </summary>
    /// <param name="mediator">Mediator instance</param>
    /// <param name="assetsCache">AssetCache instance</param>
    public AssetsController(IMediator mediator,  IAssetsCache assetsCache) 
        : base(mediator) => _assetsCache = assetsCache;

    /// <summary>
    /// Returns list of uploaded assets (files)
    /// </summary>
    /// <returns>Object</returns>
    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(GetAssetsListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetAssetsListQueryResult> GetAssetsList() 
        => await Mediator.Send(new GetAssetsListQuery());

    /// <summary>
    /// Returns storage asset file
    /// </summary>
    /// <param name="blobName">Full asset name</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>File</returns>
    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsset([FromQuery] string blobName = "", bool noCache = false)
        => await _assetsCache.GetAsset(blobName, noCache);

    /// <summary>
    /// Returns article asset (file associated with an article)
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <param name="assetName">Full asset name</param>
    /// <param name="noCache">Enable/disable REDIS cache</param>
    /// <returns>File</returns>
    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArticleAsset([FromQuery] string id = "", string assetName = "", bool noCache = false)
        => await _assetsCache.GetArticleAsset(id, assetName, noCache);
}