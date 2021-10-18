namespace TokanPages.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Shared;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;

    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProxyBaseController : ControllerBase
    {
        protected readonly ICustomHttpClient CustomHttpClient;

        protected readonly SonarQube SonarQube;

        protected readonly AzureStorage AzureStorage;

        protected static ContentResult GetContentResultFromResults(Results results) => new ()
        {
            StatusCode = (int)results.StatusCode,
            ContentType = Constants.ContentTypes.TextPlain,
            Content = results.Content == null 
                ? string.Empty 
                : System.Text.Encoding.Default.GetString(results.Content)
        };

        public ProxyBaseController(ICustomHttpClient customHttpClient, SonarQube sonarQube, AzureStorage azureStorage)
        {
            CustomHttpClient = customHttpClient;
            SonarQube = sonarQube;
            AzureStorage = azureStorage;
        }
    }
}