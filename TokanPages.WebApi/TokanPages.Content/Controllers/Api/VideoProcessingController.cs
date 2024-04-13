using TokanPages.Backend.Application.VideoProcessing;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Content.Controllers.Api;

/// <summary>
/// API endpoints definitions for video processing.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class VideoProcessingController : ApiBaseController
{
    /// <inheritdoc />
    public VideoProcessingController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns video processing status by given ticket ID.
    /// </summary>
    /// <param name="id">Ticket ID.</param>
    /// <returns>Processing details.</returns>
    [HttpGet]
    [Route("{id:guid}/[action]")]
    [AuthorizeUser(Roles.EverydayUser)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(VideoProcessingStatusQueryResult), StatusCodes.Status200OK)]
    public async Task<VideoProcessingStatusQueryResult> GetProcessingStatus([FromRoute] Guid id)
        => await Mediator.Send(new VideoProcessingStatusQuery { TicketId = id });
}