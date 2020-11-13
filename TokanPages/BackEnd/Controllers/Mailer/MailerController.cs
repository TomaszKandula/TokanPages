using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.AppLogger;
using Swashbuckle.AspNetCore.Annotations;
using TokanPages.BackEnd.Shared;
using TokanPages.BackEnd.Helpers.Statics;
using TokanPages.BackEnd.Controllers.Mailer.Model;
using TokanPages.BackEnd.Controllers.Mailer.Model.Responses;

namespace TokanPages.BackEnd.Controllers.Mailer
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Standard")]
    public class MailerController : ControllerBase
    {

        private readonly ILogicContext FLogicContext;
        private readonly IAppLogger    FAppLogger;

        public MailerController(ILogicContext ALogicContext, IAppLogger AAppLogger)
        {
            FLogicContext = ALogicContext;
            FAppLogger    = AAppLogger;
        }

        /// <summary>
        /// Get inspection results for given email address.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(EmailVerified), description: "Get inspection results for given email address.")]
        // GET api/v1/mailer/inspection/{email}/
        [HttpGet("inspection/{email}")]
        public async Task<IActionResult> VerifyEmailAddress([FromRoute] string Email) 
        {

            var LResponse = new EmailVerified { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try 
            {

                var LCheckFormat = FLogicContext.MailChecker.IsAddressCorrect(new List<string> { Email });
                var LCheckDomain = await FLogicContext.MailChecker.IsDomainCorrect(Email);

                LResponse.IsFormatCorrect = LCheckFormat.FirstOrDefault().IsValid;
                LResponse.IsDomainCorrect = LCheckDomain;

                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                LResponse.Meta.RowsAffected = 0;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();
                FAppLogger.LogFatality($"GET api/v1/mailer/inspection/{Email}/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Send new message from a user.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(MessagePosted), description: "Send new message from a user.")]
        // POST api/v1/mailer/message/
        [HttpPost("message")]
        public async Task<IActionResult> SendMessage([FromBody] NewMessage PayLoad) 
        {

            var LResponse = new MessagePosted { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FLogicContext.Mailer.From    = PayLoad.EmailFrom;
                FLogicContext.Mailer.To      = Constants.Emails.Addresses.Personal;
                FLogicContext.Mailer.Subject = $"New user message from {PayLoad.FirstName}";
                FLogicContext.Mailer.Body    = PayLoad.Message;

                var LResult = await FLogicContext.Mailer.Send();
                if (!LResult.IsSucceeded) 
                {
                    LResponse.Error.ErrorCode = LResult.ErrorCode;
                    LResponse.Error.ErrorDesc = LResult.ErrorMessage;
                    FAppLogger.LogError($"POST api/v1/mailer/message/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                LResponse.Meta.RowsAffected = 0;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();
                FAppLogger.LogFatality($"POST api/v1/mailer/message/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

    }

}
