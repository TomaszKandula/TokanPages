using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Assets.Commands;
using TokanPages.Backend.Application.Assets.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.WebApi.Controllers.Mappers;
using TokanPages.WebApi.Dto.Assets;

namespace TokanPages.WebApi.Controllers.Api;

/// <summary>
/// API endpoints definitions for assets.
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching images/videos.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class AssetsController : ApiBaseController
{
    /// <summary>
    /// Assets controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    public AssetsController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns article asset (file associated with an article).
    /// </summary>
    /// <param name="id">Article ID.</param>
    /// <param name="assetName">Full asset name.</param>
    /// <returns>File</returns>
    [HttpGet]
    [ETagFilter]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "id", "assetName" })]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArticleAsset([FromQuery] string id = "", string assetName = "")
        => await Mediator.Send(new GetArticleAssetQuery { Id = id, AssetName = assetName });

    /// <summary>
    /// Allow to upload a single asset to an Azure Storage.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.EverydayUser.
    /// </remarks>
    /// <param name="payLoad">Binary data to be uploaded.</param>
    /// <returns>Full blob name of uploaded asset.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(typeof(AddSingleAssetCommandResult), StatusCodes.Status200OK)]
    public async Task<AddSingleAssetCommandResult> AddSingleAsset([FromForm] AddSingleAssetDto payLoad)
        => await Mediator.Send(AssetsMapper.MapToAddSingleAssetCommand(payLoad));
}