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

    public class AssetsController : ProxyBaseController
    {
        public AssetsController(ICustomHttpClient ACustomHttpClient, SonarQube ASonarQube, AzureStorage AAzureStorage) 
            : base(ACustomHttpClient, ASonarQube, AAzureStorage) { }

        [HttpGet]
        [ETagFilter(200)]
        public async Task<IActionResult> GetAsset([FromQuery] string ABlobName)
        {
            var LRequestUrl = $"{FAzureStorage.BaseUrl}/content/assets/{ABlobName}";
            var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET"};
            var LResults = await FCustomHttpClient.Execute(LConfiguration);

            if (LResults.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(LResults);

            return File(LResults.Content, LResults.ContentType?.MediaType);
        }
    }
}