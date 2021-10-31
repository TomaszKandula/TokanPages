namespace TokanPages.WebApi.Controllers.Proxy
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Core.Attributes;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;

    public class ArticlesController : ProxyBaseController
    {
        public ArticlesController(ICustomHttpClient customHttpClient, SonarQube sonarQube, AzureStorage azureStorage) 
            : base(customHttpClient, sonarQube, azureStorage) { }

        [HttpGet("Assets")]
        [ETagFilter(200)]
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