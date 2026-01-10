using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Content.Cached.Commands;
using TokanPages.Backend.Application.Content.Cached.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Content.Controllers.Mappers;
using TokanPages.Content.Dto.Cached;

namespace TokanPages.Content.Controllers.Api;

/// <summary>
/// API endpoints definitions for cached files by SpaCachingService.
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching content.
/// </remarks>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/content/[controller]")]
public class CachedController : ApiBaseController
{
    /// <summary>
    /// Cached controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    public CachedController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns requested file.
    /// </summary>
    /// <param name="fileName">Requested file name. If empty, then it fallbacks to 'index.html'.</param>
    /// <returns>File.</returns>
    [HttpGet]
    [ETagFilter]
    [Route("{fileName?}")]
    public async Task<FileContentResult> Get([FromRoute] string? fileName = null)
        => await Mediator.Send(new GetFileByNameQuery { FileName = fileName });

    /// <summary>
    /// Allows to upload a file to a container (local directory).
    /// </summary>
    /// <param name="payload">Binary file.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(UploadFileToLocalStorageCommandResult), StatusCodes.Status200OK)]
    public async Task<UploadFileToLocalStorageCommandResult> Upload([FromForm] UploadFileToLocalStorageDto payload) 
        => await Mediator.Send(CachedMapper.MapToUploadFileToLocalStorageCommand(payload));

    /// <summary>
    /// Allows to perform SPA caching for given URLs.
    /// </summary>
    /// <param name="payload">Urls.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [Route("[action]")]
    [AuthorizeUser(Role.GodOfAsgard, Role.EverydayUser)]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status201Created)]
    public async Task<Unit> OrderCache([FromBody] RequestProcessingDto payload) 
        => await Mediator.Send(CachedMapper.MapToOrderSpaCachingCommand(payload));
}