using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using TokanPages.Backend.Application.Content.Assets.Commands;
using TokanPages.Backend.Application.Content.Assets.Queries;
using TokanPages.Backend.Configuration.Options;
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
    private readonly AppSettingsModel _appSettings;

    /// <summary>
    /// Assets controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    /// <param name="configuration"></param>
    public AssetsController(IMediator mediator, IOptions<AppSettingsModel> configuration) : base(mediator)
    {
        _appSettings = configuration.Value;
    }

    /// <summary>
    /// Returns article asset (file associated with an article).
    /// </summary>
    /// <param name="id">Article ID.</param>
    /// <param name="assetName">Full asset name.</param>
    /// <returns>File</returns>
    [HttpGet]
    [ETagFilter]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetArticleAsset([FromQuery] string id = "", string assetName = "")
    {
        AddCacheControl(assetName);

        var result = await Mediator.Send(new GetArticleAssetQuery
        {
            Id = id,
            AssetName = assetName
        });

        return result;
    }
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
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNonVideoAsset([FromQuery] string blobName, [FromQuery] bool? canDownload = default)
    {
        AddCacheControl(blobName);

        var result = await Mediator.Send(new GetNonVideoAssetQuery
        {
            BlobName = blobName,
            CanDownload = canDownload ?? false
        });

        if (result.FileContent is null)
            throw new GeneralException(ErrorCodes.ERROR_UNEXPECTED);

        if (canDownload is null or false)
            return result.FileContent;

        HttpContext.Response.Headers.Append("Content-disposition", $"attachment; filename={result.FileName}");
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
    [AuthorizeUser(Role.GodOfAsgard, Role.EverydayUser)]
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
    [AuthorizeUser(Role.EverydayUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(GetVideoStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<GetVideoStatusQueryResult> GetProcessingStatus([FromRoute] Guid id)
        => await Mediator.Send(new GetVideoStatusQuery { TicketId = id });

    /// <summary>
    /// Add cache control to the HTTP response so the browser can cache given file.
    /// We include only pre-defined media (like images) and non-media files (like scripts).
    /// </summary>
    /// <param name="fileName"></param>
    private void AddCacheControl(string fileName)
    {
        var cacheMediaFiles = _appSettings.CacheMediaFiles;
        var cacheNonMediaFiles = _appSettings.CacheNonMediaFiles;
        var cacheConfiguration = _appSettings.CacheConfiguration;
        var cacheList = $"{cacheMediaFiles};{cacheNonMediaFiles}".Split(";");

        var fileExtension = Path.GetExtension(fileName).Replace(".", "");
        if (!cacheList.Contains(fileExtension))
            return;

        HttpContext.Response.Headers.CacheControl = new StringValues(new [] { cacheConfiguration });
        HttpContext.Response.Headers.Pragma = new StringValues(new [] { cacheConfiguration });
    }
}