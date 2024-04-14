using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Core.Errors;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Revenue.Controllers;

/// <summary>
/// Base controller implementation.
/// </summary>
[ApiController]
[AllowAnonymous]
[ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status400BadRequest)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status403Forbidden)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status422UnprocessableEntity)]
[ProducesResponseType(typeof(ApplicationError), StatusCodes.Status500InternalServerError)]
[ExcludeFromCodeCoverage]
public class ApiBaseController : ControllerBase
{
    /// <summary>
    /// Mediator service instance.
    /// </summary>
    protected readonly IMediator Mediator;

    /// <inheritdoc />
    public ApiBaseController(IMediator mediator) => Mediator = mediator;
}