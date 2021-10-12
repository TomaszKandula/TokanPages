namespace TokanPages.WebApi.Controllers.Proxy
{
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Shared.Attributes;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;

    public class ArticlesController : ProxyBaseController
    {
        public ArticlesController(ICustomHttpClient ACustomHttpClient, SonarQube ASonarQube, AzureStorage AAzureStorage) 
            : base(ACustomHttpClient, ASonarQube, AAzureStorage) { }

        [HttpGet("Images")]
        [ETagFilter(200)]
        public async Task<IActionResult> GetArticleImage([FromQuery] string AId)
        {
            var LRequestUrl = $"{FAzureStorage.BaseUrl}/content/articles/{AId}/image.jpg";
            var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET"};
            var LResults = await FCustomHttpClient.Execute(LConfiguration);

            if (LResults.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(LResults);

            return File(LResults.Content, LResults.ContentType?.MediaType);
        }
    }
}