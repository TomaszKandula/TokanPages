namespace TokanPages.WebApi.Controllers.Api;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Backend.Domain.Enums;
using Backend.Shared.Attributes;
using Backend.Cqrs.Handlers.Queries.Logger;
using MediatR;

[Authorize]
[ApiVersion("1.0")]
public class LoggerController : ApiBaseController
{
    public LoggerController(IMediator mediator) : base(mediator) { }

    [HttpGet]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(GetLogFilesListQueryResult), StatusCodes.Status200OK)]
    public async Task<GetLogFilesListQueryResult> GetLogFilesList()
        => await Mediator.Send(new GetLogFilesListQuery());

    [HttpGet("{fileName}")]
    [AuthorizeUser(Roles.GodOfAsgard)]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLogFileContent([FromRoute] string fileName)
        => await Mediator.Send(new GetLogFileContentQuery { LogFileName = fileName });
}