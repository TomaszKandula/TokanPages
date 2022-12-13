using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Logger.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;

namespace TokanPages.WebApi.Controllers.Api;

/// <summary>
/// API endpoints definitions for logger.
/// </summary>
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class LogsController : ApiBaseController
{
    /// <summary>
    /// Logger controller.
    /// </summary>
    /// <param name="mediator"></param>
    public LogsController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns list of log files.
    /// </summary>
    /// <returns>Object.</returns>
    [HttpGet]
    [Route("[action]")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(GetLogFilesListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetLogFilesListQueryResult> GetList()
        => await Mediator.Send(new GetLogFilesListQuery());

    /// <summary>
    /// Returns file with server logs.
    /// </summary>
    /// <param name="fileName">Log file name.</param>
    /// <returns>File.</returns>
    [HttpGet]
    [Route("{fileName}/[action]")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetContent([FromRoute] string fileName)
        => await Mediator.Send(new GetLogFileContentQuery { LogFileName = fileName });
}