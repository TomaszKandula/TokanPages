namespace TokanPages.WebApi.Controllers.Proxy
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Shared;
    using Backend.Storage.Models;
    using Backend.Shared.Attributes;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;

    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AssetsController : ControllerBase
    {
        private readonly ICustomHttpClient FCustomHttpClient;

        private readonly AzureStorage FAzureStorage; 

        public AssetsController(ICustomHttpClient ACustomHttpClient, AzureStorage AAzureStorage)
        {
            FCustomHttpClient = ACustomHttpClient;
            FAzureStorage = AAzureStorage;
        }

        [HttpGet]
        [ETagFilter(200)]
        public async Task<ContentResult> GetAsset([FromQuery] string ABlobName)
        {
            try
            {
                var LRequestUrl = $"{FAzureStorage.BaseUrl}/content/assets/{ABlobName}";
                var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET"};
                var LResults = await FCustomHttpClient.Execute(LConfiguration);

                return FCustomHttpClient.GetContentResult(
                    (int)LResults.StatusCode, 
                    LResults.Content, 
                    LResults.ContentType?.MediaType);
            }
            catch (Exception LException)
            {
                return FCustomHttpClient.GetContentResult(
                    (int)HttpStatusCode.InternalServerError, 
                    LException.Message, 
                    Constants.ContentTypes.TEXT_PLAIN);
            }
        }
    }
}