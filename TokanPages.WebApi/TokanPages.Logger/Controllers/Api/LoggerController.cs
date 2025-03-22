using MediatR;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Logger.Controllers.Mappers;
using TokanPages.Logger.Dto;

namespace TokanPages.Logger.Controllers.Api;

/// <summary>
/// API endpoints definitions for logger.
/// </summary>
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public class LoggerController : ApiBaseController
{
    /// <summary>
    /// Logger controller.
    /// </summary>
    /// <param name="mediator"></param>
    public LoggerController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Log message from the frontend (ClientApp).
    /// </summary>
    /// <param name="payload">Message details.</param>
    /// <returns>Empty object.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Unit), StatusCodes.Status200OK)]
    public async Task<Unit> LogMessage([FromBody] LogMessageDto payload) 
        => await Mediator.Send(LoggerMapper.MapToLogMessageCommand(payload));
}