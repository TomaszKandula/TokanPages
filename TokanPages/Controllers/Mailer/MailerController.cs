using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;
using TokanPages.Logic;
using TokanPages.AppLogger;
using TokanPages.Backend.Shared;
using TokanPages.Logic.Mailer.Model;
using TokanPages.Controllers.Mailer.Model;
using TokanPages.Controllers.Mailer.Model.Responses;

namespace TokanPages.Controllers.Mailer
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Standard")]
    public class MailerController : BaseController
    {

        private readonly IConfiguration FConfiguration;

        public MailerController(ILogicContext ALogicContext, IAppLogger AAppLogger, IConfiguration AConfiguration)
            : base(ALogicContext, AAppLogger)
        {
            FConfiguration = AConfiguration;
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

            var LResponse = new EmailVerified();
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
        public async Task<IActionResult> SendMessage([FromBody] SendMessage PayLoad) 
        {

            var LResponse = new MessagePosted();
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
        [SwaggerResponse(statusCode: 200, type: typeof(NewsletterPosted), description: "Send newsletter to subscribed users.")]
        // POST api/v1/mailer/newsletter/
        [HttpPost("newsletter")]
        public async Task<IActionResult> SendNewsletter([FromBody] SendNewsletter PayLoad)
        {

            var LResponse = new NewsletterPosted();
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {
                var UnsubscribeBaseLink = FConfiguration.GetSection("UnsubscribeBaseLink").Value;
                foreach (var Subscriber in PayLoad.SubscribersData) 
                {

                    FLogicContext.Mailer.From = Constants.Emails.Addresses.Contact;
                    FLogicContext.Mailer.Tos  = new List<string> { Subscriber.Email };
                    FLogicContext.Mailer.Bccs = null;
                    FLogicContext.Mailer.Subject = PayLoad.Subject;

                    var UnsubscribeLink = UnsubscribeBaseLink + Subscriber.Id;
                    var NewValues = new List<ValueTag>
                    {
                        new ValueTag { Tag = "{CONTENT}", Value = PayLoad.Message },
                        new ValueTag { Tag = "{UNSUBSCRIBE_LINK}", Value = UnsubscribeLink }
                    };

                    FLogicContext.Mailer.Body = await FLogicContext.Mailer
                        .MakeBody(Constants.Emails.Templates.Newsletter, NewValues);

                    var LResult = await FLogicContext.Mailer.Send();

                    if (!LResult.IsSucceeded)
                        FAppLogger.LogError($"POST api/v1/mailer/newsletter/ | " +
                            $"Error Code: {LResult.ErrorCode}. Error Desc.: '{LResult.ErrorDesc}'. Subscriber Id: {Subscriber.Id}.");

                    FAppLogger.LogInfo($"POST api/v1/mailer/newsletter/ | Newsletter sent to: {Subscriber.Email} (Id: {Subscriber.Id}).");

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
