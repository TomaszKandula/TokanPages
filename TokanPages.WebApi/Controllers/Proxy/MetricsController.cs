namespace TokanPages.WebApi.Controllers.Proxy
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Backend.Shared;
    using Backend.Shared.Models;
    using Backend.Storage.Models;
    using Backend.Shared.Attributes;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Models;
    using Backend.Core.Utilities.CustomHttpClient.Authentication;

    public class Metrics : ProxyBaseController
    {
        public Metrics(ICustomHttpClient ACustomHttpClient, SonarQube ASonarQube, AzureStorage AAzureStorage) 
            : base(ACustomHttpClient, ASonarQube, AAzureStorage) { }

        /// <summary>
        /// Returns badge from SonarQube server for given project name and metric type.
        /// All badges have the same style.
        /// </summary>
        /// <param name="AProject">SonarQube analysis project name</param>
        /// <param name="AMetric">SonarQube metric type</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet]
        [ETagFilter(200)]
        public async Task<IActionResult> GetMetrics([FromQuery] string AProject, string AMetric)
        {
            if (string.IsNullOrEmpty(AProject) && string.IsNullOrEmpty(AMetric))
                return new ContentResult 
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameters '{nameof(AProject)}' and '{nameof(AMetric)}' are missing", 
                    ContentType = Constants.ContentTypes.TEXT_PLAIN
                };

            var LParameterList = new Dictionary<string, string>
            {
                { nameof(AProject), AProject },
                { nameof(AMetric), AMetric }
            };
                
            var LMissingParameterName = FCustomHttpClient.GetFirstEmptyParameterName(LParameterList);

            if (!string.IsNullOrEmpty(LMissingParameterName))
                return new ContentResult 
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameter '{LMissingParameterName}' is missing", 
                    ContentType = Constants.ContentTypes.TEXT_PLAIN
                };

            if (!Constants.MetricNames.NameList.Contains(AMetric))
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameter '{nameof(AMetric)}' is invalid.", 
                    ContentType = Constants.ContentTypes.TEXT_PLAIN
                };

            var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/measure?project={AProject}&metric={AMetric}";
            var LAuthentication = new BasicAuthentication { Login = FSonarQube.Token, Password = string.Empty };
            var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET", Authentication = LAuthentication};
            var LResults = await FCustomHttpClient.Execute(LConfiguration);

            if (LResults.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(LResults);

            return File(LResults.Content, LResults.ContentType?.MediaType);
        }

        /// <summary>
        /// Returns large quality gate badge from SonarQube server for given project name.
        /// </summary>
        /// <param name="AProject">SonarQube analysis project name</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet("Quality")]
        [ETagFilter(200)]
        public async Task<IActionResult> GetQualityGate([FromQuery] string AProject)
        {
            if (string.IsNullOrEmpty(AProject))
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameter '{nameof(AProject)}' is missing", 
                    ContentType = Constants.ContentTypes.TEXT_PLAIN
                };

            var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/quality_gate?project={AProject}";
            var LAuthentication = new BasicAuthentication { Login = FSonarQube.Token, Password = string.Empty };
            var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET", Authentication = LAuthentication};
            var LResults = await FCustomHttpClient.Execute(LConfiguration);

            if (LResults.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(LResults);

            return File(LResults.Content, LResults.ContentType?.MediaType);
        }
    }
}