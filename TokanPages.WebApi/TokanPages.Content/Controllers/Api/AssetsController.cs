using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Assets.Commands;
using TokanPages.Backend.Application.Assets.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Content.Controllers.Mappers;
using TokanPages.Content.Dto.Assets;

namespace TokanPages.Content.Controllers.Api;

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
    /// Returns image file from storage by its full name.
    /// </summary>
    /// <param name="blobName">Full blob name (case sensitive).</param>
    /// <returns>Image file.</returns>
    [HttpGet]
    [ETagFilter]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "blobName" })]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetImageAsset([FromQuery] string blobName) => 
        await Mediator.Send(new GetImageAssetQuery { BlobName = blobName });

    /// <summary>
    /// Allow to upload a single image asset to an Azure Storage.
    /// </summary>
    /// <remarks>
    /// Requires: Roles.GodOfAsgard, Roles.EverydayUser.
    /// </remarks>
    /// <param name="payload">Binary data to be uploaded.</param>
    /// <returns>Full blob name of uploaded asset.</returns>
    [HttpPost]
    [AuthorizeUser(Roles.GodOfAsgard, Roles.EverydayUser)]
    [ProducesResponseType(typeof(AddImageAssetCommandResult), StatusCodes.Status200OK)]
    public async Task<AddImageAssetCommandResult> AddImageAsset([FromForm] AddImageAssetDto payload)
        => await Mediator.Send(AssetsMapper.MapToAddImageAssetCommand(payload));

    /// <summary>
    /// Returns video file from storage by its full name.
    /// </summary>
    /// <remarks>
    /// This endpoint serves as a proxy to an Azure Blob Storage.
    /// </remarks>
    /// <param name="blobName">Full blob name (case sensitive).</param>
    /// <returns>Video file.</returns>
    [HttpGet]
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [ProducesResponseType(StatusCodes.Status206PartialContent)]
    public async Task GetVideoAsset([FromQuery] string blobName)
        => await Mediator.Send(new GetVideoAssetQuery { BlobName = blobName });

    /// <summary>
    /// Allow to upload a single video asset to an Azure Storage.
    /// </summary>
    /// <param name="payload">Binary data to be uploaded.</param>
    /// <returns>Ticket ID.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AddVideoAssetCommandResult), StatusCodes.Status200OK)]
    public async Task<AddVideoAssetCommandResult> AddVideoAsset([FromForm] AddVideoAssetDto payload)
        => await Mediator.Send(AssetsMapper.MapToAddVideoAssetCommand(payload));
}