namespace TokanPages.WebApi.Controllers.Health
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Database;
    using Backend.SmtpClient;
    using Backend.Shared.Resources;
    
    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly ISmtpClientService FSmtpClientService;

        public HealthController(DatabaseContext ADatabaseContext, ISmtpClientService ASmtpClientService)
        {
            FDatabaseContext = ADatabaseContext;
            FSmtpClientService = ASmtpClientService;
        }

        /// <summary>
        /// Checks the critical components of the application: SMTP server and SQL Server.
        /// Because the application depends on a database and an e-mail system, the health check endpoint
        /// connect to those components. If the application cannot connect to a critical component,
        /// then the path return a HTTP error response code to indicate that the application is unhealthy.
        /// </summary>
        /// <remarks>
        /// Azure Health Check requires returned HTTP status code to be Internal Server Error (500) when tests fail;
        /// and OK (200) when all tests pass.
        /// </remarks>
        /// <returns>JSON model with response details</returns>
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var LCanConnectAndAuthenticate = await FSmtpClientService.CanConnectAndAuthenticate();
            if (!LCanConnectAndAuthenticate.IsSucceeded)
                return StatusCode(500, LCanConnectAndAuthenticate);

            var LCanConnectToDatabase = await FDatabaseContext.Database.CanConnectAsync();
            if (!LCanConnectToDatabase)
                return StatusCode(500, new Backend.Shared.Models.ActionResult
                {
                    ErrorCode = nameof(ErrorCodes.CANNOT_CONNECT_DATABASE),
                    ErrorDesc = ErrorCodes.CANNOT_CONNECT_DATABASE
                });

            return StatusCode(200, new Backend.Shared.Models.ActionResult { IsSucceeded = true });
        }
    }
}