using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.Shared;
using TokanPages.BackEnd.AppLogger;
using TokanPages.BackEnd.Helpers.Statics;
using TokanPages.BackEnd.Controllers.Articles.Model;
using TokanPages.BackEnd.Controllers.Articles.Model.Responses;

namespace TokanPages.BackEnd.Controllers.Articles
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Standard")]
    public class ArticlesController : BaseController
    {

        public ArticlesController(ILogicContext ALogicContext, IAppLogger AAppLogger) 
            : base(ALogicContext, AAppLogger)
        {
        }

        /// <summary>
        /// Returns all items from Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(ReturnArticles), description: "Returns all items from Articles collection.")]
        // GET api/v1/articles/
        [HttpGet]
        public async Task<IActionResult> GetItemsAsync()
        {

            var LResponse = new ReturnArticles { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo("GET api/v1/articles/ | Calling CosmosDB to get all articles..."); 
                var LData = await FLogicContext.Articles.GetAllArticles();

                if (LData == null || !LData.Any()) 
                {
                    LResponse.Error.ErrorCode = Constants.Errors.EmptyList.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.EmptyList.ErrorDesc;
                    FAppLogger.LogWarn($"GET api/v1/articles/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.Articles = LData;
                LResponse.Meta.RowsAffected = LResponse.Articles.Count;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"GET api/v1/articles/ | Returned: {LData.Count} articles.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"GET api/v1/articles/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Returns an item from Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(ReturnArticle), description: "Returns an item from Articles collection.")]
        // GET api/v1/articles/{id}/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemAsync([FromRoute] Guid Id)
        {

            var LResponse = new ReturnArticle { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo($"GET api/v1/articles/{Id}/ | Calling CosmosDB to get article...");
                var LData = await FLogicContext.Articles.GetSingleArticle(Id);

                if (LData == null) 
                {
                    LResponse.Error.ErrorCode = Constants.Errors.NoSuchItem.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.NoSuchItem.ErrorDesc;
                    FAppLogger.LogWarn($"GET api/v1/articles/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.Article = LData;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"GET api/v1/articles/{Id} | Returned: '{LData.Title}' article.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"GET api/v1/articles/{Id}/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Add new article into Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(ArticleAdded), description: "Add new article into Articles collection.")]
        // POST api/v1/articles/
        [HttpPost]
        public async Task<IActionResult> AddItemAsync([FromBody] ArticleRequest PayLoad)
        {

            var LResponse = new ArticleAdded { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                // Field 'PayLoad.Text' should be used to create text.html 
                // and place on Azure Storage Blob under returned ID.
                // ...

                FAppLogger.LogInfo("POST api/v1/articles/ | Calling CosmosDB to insert new article...");
                var LNewId = await FLogicContext.Articles.AddNewArticle(PayLoad);

                if (LNewId == Guid.Empty) 
                {
                    LResponse.Error.ErrorCode = Constants.Errors.UnableToPost.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.UnableToPost.ErrorDesc;
                    FAppLogger.LogError($"POST api/v1/articles/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.NewUid = LNewId;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"POST api/v1/articles/ | New article has been posted under id: '{LNewId}'.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"POST api/v1/articles/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Update existing article in Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(ArticleUpdated), description: "Update existing article in Articles collection.")]
        // PATCH api/v1/articles/
        [HttpPatch]
        public async Task<IActionResult> ChangeItemAsync([FromBody] ArticleRequest PayLoad)
        {

            var LResponse = new ArticleUpdated { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                // Field 'PayLoad.Text' should be used to update text.html 
                // that resides on Azure Storage Blob under returned ID.
                // ...

                FAppLogger.LogInfo("PATCH api/v1/articles/ | Calling CosmosDB to update article...");
                var LStatusCode = await FLogicContext.Articles.UpdateArticle(PayLoad);

                if (LStatusCode != HttpStatusCode.OK) 
                {
                    LResponse.Error.ErrorCode = Constants.Errors.UnableToModify.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.UnableToModify.ErrorDesc;
                    FAppLogger.LogError($"PATCH api/v1/articles/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"PATCH api/v1/articles/ | Article has been updated.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"PATCH api/v1/articles/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

        /// <summary>
        /// Delete existing article from Articles collection.
        /// </summary>
        /// <returns></returns>
        [SwaggerResponse(statusCode: 200, type: typeof(ArticleDeleted), description: "Delete existing article from Articles collection.")]
        // DELETE api/v1/articles/{id}/
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveItemAsync([FromRoute] Guid Id)
        {

            var LResponse = new ArticleDeleted { Meta = { RequesterIpAddress = IpAddress.Get(HttpContext) } };
            var LStartTime = DateTime.Now.TimeOfDay;
            try
            {

                FAppLogger.LogInfo($"DELETE api/v1/articles/{Id}/ | Calling CosmosDB to remove article...");
                var LStatusCode = await FLogicContext.Articles.DeleteArticle(Id);

                if (LStatusCode != HttpStatusCode.OK) 
                {
                    LResponse.Error.ErrorCode = Constants.Errors.UnableToRemove.ErrorCode;
                    LResponse.Error.ErrorDesc = Constants.Errors.UnableToRemove.ErrorDesc;
                    FAppLogger.LogError($"DELETE api/v1/articles/{Id}/ | {LResponse.Error.ErrorDesc}.");
                    return StatusCode(200, LResponse);
                }

                LResponse.IsSucceeded = true;
                LResponse.Meta.RowsAffected = 1;
                LResponse.Meta.ProcessingTimeSpan = (DateTime.Now.TimeOfDay - LStartTime).ToString();

                FAppLogger.LogInfo($"DELETE api/v1/articles/{Id}/ | Article has been removed.");
                return StatusCode(200, LResponse);

            }
            catch (Exception LException)
            {
                LResponse.Error.ErrorCode = LException.HResult.ToString();
                LResponse.Error.ErrorDesc = string.IsNullOrEmpty(LException.InnerException?.Message)
                    ? LException.Message
                    : $"{LException.Message} ({LException.InnerException.Message}).";
                FAppLogger.LogFatality($"DELETE api/v1/articles/{Id}/ | Error has been raised: {LResponse.Error.ErrorDesc}");
                return StatusCode(500, LResponse);
            }

        }

    }

}
