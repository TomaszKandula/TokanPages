namespace TokanPages.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Core.Utilities.CustomHttpClient;

    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProxyBaseController : ControllerBase
    {
        protected readonly ICustomHttpClient FCustomHttpClient;

        protected readonly SonarQube FSonarQube;
        
        protected readonly AzureStorage FAzureStorage; 
        
        public ProxyBaseController(ICustomHttpClient ACustomHttpClient, SonarQube ASonarQube, AzureStorage AAzureStorage)
        {
            FCustomHttpClient = ACustomHttpClient;
            FSonarQube = ASonarQube;
            FAzureStorage = AAzureStorage;
        }
    }
}