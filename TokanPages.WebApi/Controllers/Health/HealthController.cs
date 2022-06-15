namespace TokanPages.WebApi.Controllers.Health;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Backend.Database;
using Backend.Shared.Resources;

/// <summary>
/// Health controller
/// </summary>
[ApiController]
[AllowAnonymous]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class HealthController : ControllerBase
{
    private readonly DatabaseContext _databaseContext;

    /// <summary>
    /// Health controller
    /// </summary>
    /// <param name="databaseContext">DatabaseContext instance</param>
    public HealthController(DatabaseContext databaseContext) => _databaseContext = databaseContext;

    /// <summary>
    /// Checks the critical components of the application
    /// </summary>
    /// <remarks>
    /// Because the application depends on a database, the health check endpoint connect to the component.
    /// If the application cannot connect to a critical component, then the path return a HTTP error response code 
    /// to indicate that the application is unhealthy.
    /// Azure Health Check requires returned HTTP status code to be Internal Server Error (500) when tests fail;
    /// and OK (200) when all tests pass.
    /// </remarks>
    /// <returns>JSON model with response details</returns>
    [HttpGet("status")]
    [ProducesResponseType(typeof(IActionResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStatus()
    {
        var canConnectToDatabase = await _databaseContext.Database.CanConnectAsync();
        if (!canConnectToDatabase)
            return StatusCode(500, new Backend.Shared.Models.ActionResult
            {
                ErrorCode = nameof(ErrorCodes.CANNOT_CONNECT_DATABASE),
                ErrorDesc = ErrorCodes.CANNOT_CONNECT_DATABASE
            });

        return StatusCode(200, new Backend.Shared.Models.ActionResult { IsSucceeded = true });
    }
}