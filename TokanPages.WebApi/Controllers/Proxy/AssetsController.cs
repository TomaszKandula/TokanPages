namespace TokanPages.WebApi.Controllers.Proxy
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Shared;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Shared.Attributes;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;

    public class AssetsController : ProxyBaseController
    {
        public AssetsController(ICustomHttpClient ACustomHttpClient, SonarQube ASonarQube, AzureStorage AAzureStorage) 
            : base(ACustomHttpClient, ASonarQube, AAzureStorage) { }

        [HttpGet]
        [ETagFilter(200)]
        public async Task<IActionResult> GetAsset([FromQuery] string ABlobName)
        {
            try
            {
                var LRequestUrl = $"{FAzureStorage.BaseUrl}/content/assets/{ABlobName}";
                var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET"};
                var LResults = await FCustomHttpClient.Execute(LConfiguration);

                if (LResults.StatusCode != HttpStatusCode.OK)
                    return new ContentResult
                    {
                        StatusCode = (int)LResults.StatusCode,
                        Content = System.Text.Encoding.Default.GetString(LResults.Content),
                        ContentType = Constants.ContentTypes.TEXT_PLAIN
                    };

                return File(LResults.Content, LResults.ContentType?.MediaType);
            }
            catch (Exception LException)
            {
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Content = LException.Message,
                    ContentType = Constants.ContentTypes.TEXT_PLAIN
                };
            }
        }
    }
}