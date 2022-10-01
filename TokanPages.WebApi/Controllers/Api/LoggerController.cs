using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Logger.Queries;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Attributes;

namespace TokanPages.WebApi.Controllers.Api;

/// <summary>
/// API endpoints definitions for logger
/// </summary>
[Authorize]
[ApiVersion("1.0")]
public class LoggerController : ApiBaseController
{
    /// <summary>
    /// Logger controller
    /// </summary>
    /// <param name="mediator"></param>
    public LoggerController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Returns list of log files
    /// </summary>
    /// <returns>Object</returns>
    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(GetLogFilesListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetLogFilesListQueryResult> GetLogFilesList()
        => await Mediator.Send(new GetLogFilesListQuery());

    /// <summary>
    /// Returns file with server logs
    /// </summary>
    /// <param name="fileName">Log file name</param>
    /// <returns>File</returns>
    [HttpGet("{fileName}")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLogFileContent([FromRoute] string fileName)
        => await Mediator.Send(new GetLogFileContentQuery { LogFileName = fileName });
}