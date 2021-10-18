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
        public Metrics(ICustomHttpClient customHttpClient, SonarQube sonarQube, AzureStorage azureStorage) 
            : base(customHttpClient, sonarQube, azureStorage) { }

        /// <summary>
        /// Returns badge from SonarQube server for given project name and metric type.
        /// All badges have the same style.
        /// </summary>
        /// <param name="project">SonarQube analysis project name</param>
        /// <param name="metric">SonarQube metric type</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet]
        [ETagFilter(200)]
        public async Task<IActionResult> GetMetrics([FromQuery] string project, string metric)
        {
            if (string.IsNullOrEmpty(project) && string.IsNullOrEmpty(metric))
                return new ContentResult 
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameters '{nameof(project)}' and '{nameof(metric)}' are missing", 
                    ContentType = Constants.ContentTypes.TextPlain
                };

            var parameterList = new Dictionary<string, string>
            {
                { nameof(project), project },
                { nameof(metric), metric }
            };
                
            var missingParameterName = CustomHttpClient.GetFirstEmptyParameterName(parameterList);

            if (!string.IsNullOrEmpty(missingParameterName))
                return new ContentResult 
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameter '{missingParameterName}' is missing", 
                    ContentType = Constants.ContentTypes.TextPlain
                };

            if (!Constants.MetricNames.NameList.Contains(metric))
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameter '{nameof(metric)}' is invalid.", 
                    ContentType = Constants.ContentTypes.TextPlain
                };

            var requestUrl = $"{SonarQube.Server}/api/project_badges/measure?project={project}&metric={metric}";
            var authentication = new BasicAuthentication { Login = SonarQube.Token, Password = string.Empty };
            var configuration = new Configuration { Url = requestUrl, Method = "GET", Authentication = authentication};
            var results = await CustomHttpClient.Execute(configuration);

            if (results.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(results);

            return File(results.Content, results.ContentType?.MediaType);
        }

        /// <summary>
        /// Returns large quality gate badge from SonarQube server for given project name.
        /// </summary>
        /// <param name="project">SonarQube analysis project name</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet("Quality")]
        [ETagFilter(200)]
        public async Task<IActionResult> GetQualityGate([FromQuery] string project)
        {
            if (string.IsNullOrEmpty(project))
                return new ContentResult
                {
                    StatusCode = (int)HttpStatusCode.BadRequest, 
                    Content = $"Parameter '{nameof(project)}' is missing", 
                    ContentType = Constants.ContentTypes.TextPlain
                };

            var requestUrl = $"{SonarQube.Server}/api/project_badges/quality_gate?project={project}";
            var authentication = new BasicAuthentication { Login = SonarQube.Token, Password = string.Empty };
            var configuration = new Configuration { Url = requestUrl, Method = "GET", Authentication = authentication};
            var results = await CustomHttpClient.Execute(configuration);

            if (results.StatusCode != HttpStatusCode.OK)
                return GetContentResultFromResults(results);

            return File(results.Content, results.ContentType?.MediaType);
        }
    }
}