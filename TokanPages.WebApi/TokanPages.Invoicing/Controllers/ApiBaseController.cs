using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using TokanPages.Backend.Core.Errors;

namespace TokanPages.Invoicing.Controllers;

/// <summary>
/// Base controller with MediatR.
/// </summary>
[ApiController]
[AllowAnonymous]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status500InternalServerError)]
public class ApiBaseController : ControllerBase
{
    /// <summary>
    /// Mediator instance.
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    /// Base controller.
    /// </summary>
    /// <param name="mediator">Mediator instance.</param>
    public ApiBaseController(IMediator mediator) => Mediator = mediator;
}