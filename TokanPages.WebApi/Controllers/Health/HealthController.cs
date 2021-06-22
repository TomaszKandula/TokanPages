using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Database;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.SmtpClient.Models;

namespace TokanPages.WebApi.Controllers.Health
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private const string CANNOT_CONNECT_DATABASE = "Cannot connect to the database";
        
        private readonly DatabaseContext FDatabaseContext;

        private readonly ISmtpClientService FSmtpClientService;

        public HealthController(DatabaseContext ADatabaseContext, ISmtpClientService ASmtpClientService)
        {
            FDatabaseContext = ADatabaseContext;
            FSmtpClientService = ASmtpClientService;
        }

        /// <summary>
        /// Checks the critical components of the application: SMTP server and SQL Server.
        /// Because the application depends on a database and a e-mail system, the health check endpoint
        /// connect to those components. If the application cannot connect to a critical component,
        /// then the path return a HTTP error response code to indicate that the app is unhealthy.
        /// </summary>
        /// <returns>JSON model with response details</returns>
        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            try
            {
                var LCanConnectAndAuthenticate = await FSmtpClientService.CanConnectAndAuthenticate();
                if (!LCanConnectAndAuthenticate.IsSucceeded)
                    return StatusCode(500, LCanConnectAndAuthenticate);

                var LCanConnectToDatabase = await FDatabaseContext.Database.CanConnectAsync();
                if (!LCanConnectToDatabase)
                    return StatusCode(500, new SendActionResult
                    {
                        ErrorCode = nameof(CANNOT_CONNECT_DATABASE),
                        ErrorDesc = CANNOT_CONNECT_DATABASE
                    });

                return StatusCode(200, new SendActionResult { IsSucceeded = true });
            }
            catch (Exception LException)
            {
                return StatusCode(500, new SendActionResult
                {
                    ErrorCode = LException.HResult.ToString(),
                    ErrorDesc = LException.Message
                });
            }
        }
    }
}