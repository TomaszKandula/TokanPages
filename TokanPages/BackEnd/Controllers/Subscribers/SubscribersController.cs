using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.AppLogger;
using Swashbuckle.AspNetCore.Annotations;
using TokanPages.BackEnd.Shared;
using TokanPages.BackEnd.Helpers.Statics;
using TokanPages.BackEnd.Controllers.Subscribers.Model;
using TokanPages.BackEnd.Controllers.Subscribers.Model.Responses;

namespace TokanPages.BackEnd.Controllers.Subscribers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Standard")]
    public class SubscribersController : BaseController
    {

        public SubscribersController(IConfiguration AConfiguration, ILogicContext ALogicContext, IAppLogger AAppLogger)
            : base(AConfiguration, ALogicContext, AAppLogger)
        {
        }

        /// <summary>
        /// Returns all items from Subscribers collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(ReturnSubscribers), description: "Returns all items from Subscribers collection.")]
        // GET api/v1/subscribers/
        [HttpGet]
        public async Task<IActionResult> GetAllSubscribers() 
        {

            var LResponse = new ReturnSubscribers { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo("GET api/v1/subscribers/ | Calling CosmosDB to get all subscribers...");
                var LData = await FLogicContext.Subscribers.GetAllSubscribers();

                if (LData == null || !LData.Any())
                {
                    LResponse.Error.ErrorCode = Constants.Errors.EmptyList.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.EmptyList.ErrorDesc;
                    FAppLogger.LogWarn($"GET api/v1/subscribers/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.Subscribers = LData;               
                LResponse.Meta.RowsAffected = LResponse.Subscribers.Count;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"GET api/v1/subscribers/ | Returned: {LData.Count} subscribers.");
                return StatusCode(200, LResponse);
                
            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"GET api/v1/subscribers/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Returns an item from Subscribers collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(ReturnSubscriber), description: "Returns an item from Subscribers collection.")]
        // GET api/v1/subscribers/{id}/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemAsync([FromRoute] string Id)
        {

            var LResponse = new ReturnSubscriber { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo($"GET api/v1/subscribers/{Id}/ | Calling CosmosDB to get subscriber data...");
                var LData = await FLogicContext.Subscribers.GetSingleSubscriber(Id);

                if (LData == null)
                {
                    LResponse.Error.ErrorCode = Constants.Errors.NoSuchItem.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.NoSuchItem.ErrorDesc;
                    FAppLogger.LogWarn($"GET api/v1/subscribers/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.Subscriber = LData;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"GET api/v1/subscribers/{Id} | Returned: '{LData.Email}' subscriber.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"GET api/v1/subscribers/{Id}/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Add new subscriber into Subscribers collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(SubscriberAdded), description: "Add new article into Subscribers collection.")]
        // POST api/v1/subscribers/
        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] SubscriberRequest PayLoad)
        {

            var LResponse = new SubscriberAdded { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo("POST api/v1/subscribers/ | Calling CosmosDB to insert new subscriber...");
                
                var LResult = await FLogicContext.Subscribers.AddNewSubscriber(PayLoad);

                if (LResult.Error.ErrorCode != Constants.Errors.Default.ErrorCode) 
                {
                    LResponse.Error.ErrorCode = LResult.Error.ErrorCode;
                    LResponse.Error.ErrorDesc = LResult.Error.ErrorDesc;
                    FAppLogger.LogError($"POST api/v1/subscribers/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.NewUid = LResult.NewId;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"POST api/v1/subscribers/ | New subscriber has been posted under id: '{LResult.NewId}'.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"POST api/v1/subscribers/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Update existing subscriber in Subscribers collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(SubscriberUpdated), description: "Update existing subscriber in Subscribers collection.")]
        // PATCH api/v1/subscribers/
        [HttpPatch]
        public async Task<IActionResult> ChangeItemAsync([FromBody] SubscriberRequest PayLoad)
        {

            var LResponse = new SubscriberUpdated { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo("PATCH api/v1/subscribers/ | Calling CosmosDB to update subscriber...");
                var LStatusCode = await FLogicContext.Subscribers.UpdateSubscriber(PayLoad);

                if (LStatusCode != HttpStatusCode.OK)
                {
                    LResponse.Error.ErrorCode = Constants.Errors.UnableToModify.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.UnableToModify.ErrorDesc;
                    FAppLogger.LogError($"PATCH api/v1/subscribers/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"PATCH api/v1/subscribers/ | Subscriber has been updated.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"PATCH api/v1/subscribers/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Delete existing subscriber from Subscribers collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(SubscriberDeleted), description: "Delete existing subscriber from Subscribers collection.")]
        // DELETE api/v1/subscribers/{id}/
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItemAsync([FromRoute] string Id)
        {

            var LResponse = new SubscriberDeleted { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo($"DELETE api/v1/subscribers/{Id}/ | Calling CosmosDB to remove subscriber...");
                var LStatusCode = await FLogicContext.Subscribers.DeleteSubscriber(Id);

                if (LStatusCode != HttpStatusCode.NoContent)
                {
                    LResponse.Error.ErrorCode = Constants.Errors.UnableToRemove.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.UnableToRemove.ErrorDesc;
                    FAppLogger.LogError($"DELETE api/v1/subscribers/{Id}/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"DELETE api/v1/subscribers/{Id}/ | Subscriber has been removed.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"DELETE api/v1/subscribers/{Id}/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

    }

}
