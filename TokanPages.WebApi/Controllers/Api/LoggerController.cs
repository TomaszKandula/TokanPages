namespace TokanPages.WebApi.Controllers.Api;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Enums;
using Backend.Shared.Attributes;
using Backend.Cqrs.Handlers.Queries.Logger;
using MediatR;

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