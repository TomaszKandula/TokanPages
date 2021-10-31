namespace TokanPages.WebApi.Controllers.Proxy
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;

    public class AssetsController : ProxyBaseController
    {
        public AssetsController(ICustomHttpClient customHttpClient, SonarQube sonarQube, AzureStorage azureStorage) 
            : base(customHttpClient, sonarQube, azureStorage) { }

        [HttpGet]
        public async Task<IActionResult> GetAsset([FromQuery] string blobName)
        {
            var requestUrl = $"{AzureStorage.BaseUrl}/content/assets/{blobName}";
            var configuration = new Configuration { Url = requestUrl, Method = "GET"};
            var results = await CustomHttpClient.Execute(configuration);

            if (results.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(results);

            return File(results.Content, results.ContentType?.MediaType);
        }

        [HttpGet("Article")]
        public async Task<IActionResult> GetArticleAsset([FromQuery] string id, string assetName)
        {
            var requestUrl = $"{AzureStorage.BaseUrl}/content/articles/{id}/{assetName}";
            var configuration = new Configuration { Url = requestUrl, Method = "GET"};
            var results = await CustomHttpClient.Execute(configuration);

            if (results.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(results);

            return File(results.Content, results.ContentType?.MediaType);
        }
    }
}