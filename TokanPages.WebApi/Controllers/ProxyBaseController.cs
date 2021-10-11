namespace TokanPages.WebApi.Controllers
{
    using System;
    using System.Net;
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
        protected readonly ICustomHttpClient FCustomHttpClient;

        protected readonly SonarQube FSonarQube;

        protected readonly AzureStorage FAzureStorage;

        protected static ContentResult GetContentResultFromResults(Results AResults) => new ()
        {
            StatusCode = (int)AResults.StatusCode,
            ContentType = Constants.ContentTypes.TEXT_PLAIN,
            Content = AResults.Content == null 
                ? string.Empty 
                : System.Text.Encoding.Default.GetString(AResults.Content)
        };

        protected static ContentResult GetInternalServerError(Exception AException) => new ()
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
            Content = AException.Message,
            ContentType = Constants.ContentTypes.TEXT_PLAIN
        };

        public ProxyBaseController(ICustomHttpClient ACustomHttpClient, SonarQube ASonarQube, AzureStorage AAzureStorage)
        {
            FCustomHttpClient = ACustomHttpClient;
            FSonarQube = ASonarQube;
            FAzureStorage = AAzureStorage;
        }
    }
}