namespace TokanPages.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Backend.Core.Models;
using MediatR;
    
[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status500InternalServerError)]
public class ApiBaseController : ControllerBase
{
    protected readonly IMediator Mediator;

    public ApiBaseController(IMediator mediator) => Mediator = mediator;
}