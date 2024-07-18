using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Content.Assets.Commands;
using TokanPages.Backend.Application.Content.Assets.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Backend.Shared.Resources;
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
[Route("api/v{version:apiVersion}/content/[controller]/[action]")]
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
    /// Returns file from storage by its full name.
    /// </summary>
    /// <remarks>
    /// This endpoint will reject any video file (we have separate endpoint for video content).
    /// </remarks>
    /// <param name="blobName">Full blob name (case sensitive).</param>
    /// <param name="canDownload">Web Browser will always download a file instead of showing it.</param>
    /// <returns>File.</returns>
    [HttpGet]
    [ETagFilter]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new[] { "blobName" })]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNonVideoAsset([FromQuery] string blobName, [FromQuery] bool? canDownload = default)
    {
        var result = await Mediator.Send(new GetNonVideoAssetQuery
        {
            BlobName = blobName,
            CanDownload = canDownload ?? false
        });

        if (result.FileContent is null)
            throw new GeneralException(ErrorCodes.ERROR_UNEXPECTED);

        if (canDownload is null or false)
            return result.FileContent;

        HttpContext.Response.Headers.Add("Content-disposition", $"attachment; filename={result.FileName}");
        return result.FileContent;
    }

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
    
    /// <summary>
    /// Returns video processing status by given ticket ID.
    /// </summary>
    /// <param name="id">Ticket ID.</param>
    /// <returns>Processing details.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetVideoStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<GetVideoStatusQueryResult> GetProcessingStatus([FromRoute] Guid id)
        => await Mediator.Send(new GetVideoStatusQuery { TicketId = id });
}