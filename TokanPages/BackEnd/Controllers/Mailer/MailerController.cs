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
using TokanPages.BackEnd.Logic.Mailer.Model;

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

                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
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

                FLogicContext.Mailer.From    = Constants.Emails.Addresses.Contact;
                FLogicContext.Mailer.Tos     = new List<string> { Constants.Emails.Addresses.Contact };
                FLogicContext.Mailer.Subject = $"New user message from {PayLoad.FirstName}";

                var NewValues = new List<ValueTag>
                { 
                    new ValueTag { Tag = "{FIRST_NAME}",    Value = PayLoad.FirstName },
                    new ValueTag { Tag = "{LAST_NAME}",     Value = PayLoad.LastName },
                    new ValueTag { Tag = "{EMAIL_ADDRESS}", Value = PayLoad.UserEmail },
                    new ValueTag { Tag = "{USER_MSG}",      Value = PayLoad.Message },
                    new ValueTag { Tag = "{DATE_TIME}",     Value = DateTime.Now.ToString() }
                };
                
                FLogicContext.Mailer.Body = await FLogicContext.Mailer
                    .MakeBody(Constants.Emails.Templates.ContactForm, NewValues);

                var LResult = await FLogicContext.Mailer.Send();
                if (!LResult.IsSucceeded) 
                {
                    LResponse.Error.ErrorCode = LResult.ErrorCode;
                    LResponse.Error.ErrorDesc = LResult.ErrorDesc;
                    FAppLogger.LogError($"POST api/v1/mailer/message/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"POST api/v1/mailer/message/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Send newsletter to subscribed users.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(MessagePosted), description: "Send newsletter to subscribed users.")]
        // POST api/v1/mailer/newsletter/
        [HttpPost("newsletter")]
        public async Task<IActionResult> SendNewsletter([FromBody] NewMessage PayLoad)
        {

            var LResponse = new MessagePosted { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FLogicContext.Mailer.From    = Constants.Emails.Addresses.Contact;
                FLogicContext.Mailer.Tos     = new List<string> { Constants.Emails.Addresses.Contact };
                FLogicContext.Mailer.Bccs    = PayLoad.EmailTos;
                FLogicContext.Mailer.Subject = PayLoad.Subject;

                var NewValues = new List<ValueTag>
                {
                    new ValueTag { Tag = "{CONTENT}", Value = PayLoad.Message }
                };

                FLogicContext.Mailer.Body = await FLogicContext.Mailer
                    .MakeBody(Constants.Emails.Templates.Newsletter, NewValues);

                var LResult = await FLogicContext.Mailer.Send();
                if (!LResult.IsSucceeded)
                {
                    LResponse.Error.ErrorCode = LResult.ErrorCode;
                    LResponse.Error.ErrorDesc = LResult.ErrorDesc;
                    FAppLogger.LogError($"POST api/v1/mailer/newsletter/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"POST api/v1/mailer/newsletter/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

    }

}
