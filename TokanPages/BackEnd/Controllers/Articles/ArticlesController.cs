using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.AppLogger;
using TokanPages.BackEnd.Helpers.Statics;
using TokanPages.BackEnd.Controllers.Articles.Model.Responses;

namespace TokanPages.BackEnd.Controllers.Articles
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Standard")]
    public class ArticlesController : ControllerBase 
    {

        private readonly ILogicContext FLogicContext;
        private readonly IAppLogger    FAppLogger;

        public ArticlesController(ILogicContext ALogicContext, IAppLogger AAppLogger) 
        {
            FLogicContext = ALogicContext;
            FAppLogger    = AAppLogger;
        }

        /// <summary>
        /// Returns all items from Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(
            statusCode: 200,
            description: "Returns all items from Articles collection.",
            type: typeof(ReturnArticles)
        )]
        // GET api/v1/articles/
        [HttpGet]
        public async Task<IActionResult> GetItemsAsync()
        {

            var LResponse = new ReturnArticles { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                //...

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
                FAppLogger.LogFatality($"GET api/v1/articles/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Returns an item from Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(
            statusCode: 200,
            description: "Returns an item from Articles collection.",
            type: typeof(ReturnArticle)
        )]
        // GET api/v1/articles/{uid}/
        [HttpGet]
        public async Task<IActionResult> GetItemAsync([FromRoute] string Uid)
        {

            var LResponse = new ReturnArticle { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                //...

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
                FAppLogger.LogFatality($"GET api/v1/articles/{Uid}/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Add new article into Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(
            statusCode: 200,
            description: "Add new article into Articles collection.",
            type: typeof(ArticleAdded)
        )]
        // POST api/v1/articles/
        [HttpPost]
        public async Task<IActionResult> AddItemAsync()
        {

            var LResponse = new ArticleAdded { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                //...

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
                FAppLogger.LogFatality($"POST api/v1/articles/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Update existing article in Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(
            statusCode: 200,
            description: "Update existing article in Articles collection.",
            type: typeof(ArticleUpdated)
        )]
        // PATCH api/v1/articles/{uid}
        [HttpPatch]
        public async Task<IActionResult> ChangeItemAsync([FromRoute] string Uid)
        {

            var LResponse = new ArticleUpdated { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                //...

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
                FAppLogger.LogFatality($"PATCH api/v1/articles/{Uid} | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Delete existing article from Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(
            statusCode: 200,
            description: "Update existing article from Articles collection.",
            type: typeof(ArticleDeleted)
        )]
        // DELETE api/v1/articles/{uid}
        [HttpDelete]
        public async Task<IActionResult> RemoveItemAsync([FromRoute] string Uid)
        {

            var LResponse = new ArticleDeleted { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                //...

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
                FAppLogger.LogFatality($"PATCH api/v1/articles/{Uid} | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

    }

}
