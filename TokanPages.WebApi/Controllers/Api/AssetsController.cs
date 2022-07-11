namespace TokanPages.WebApi.Controllers.Api;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Domain.Enums;
using Backend.Shared.Attributes;
using Backend.Cqrs.Handlers.Queries.Assets;
using Attributes;
using MediatR;

/// <summary>
/// API endpoints definitions for assets
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching images/videos
/// </remarks>
[ApiVersion("1.0")]
public class AssetsController : ApiBaseController
{
    /// <summary>
    /// Assets controller
    /// </summary>
    /// <param name="mediator">Mediator instance</param>
    public AssetsController(IMediator mediator) : base(mediator) { }

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
    /// <returns>File</returns>
    [HttpGet]
    [ETagFilter]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "blobName" })]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsset([FromQuery] string blobName = "")
        => await Mediator.Send(new GetSingleAssetQuery { BlobName = blobName });

    /// <summary>
    /// Returns article asset (file associated with an article)
    /// </summary>
    /// <param name="id">Article ID</param>
    /// <param name="assetName">Full asset name</param>
    /// <returns>File</returns>
    [HttpGet]
    [ETagFilter]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "id", "assetName" })]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArticleAsset([FromQuery] string id = "", string assetName = "")
        => await Mediator.Send(new GetArticleAssetQuery { Id = id, AssetName = assetName });
}