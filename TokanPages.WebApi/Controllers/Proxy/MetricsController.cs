namespace TokanPages.WebApi.Controllers.Proxy
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Backend.Shared;
    using Backend.Shared.Models;
    using Backend.Shared.Attributes;
    using Backend.Core.Utilities.CustomHttpClient;
    using Backend.Core.Utilities.CustomHttpClient.Authentication;

    [Route("api/v1/SonarQube/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class Metrics : ControllerBase
    {
        private readonly SonarQube FSonarQube;

        private readonly ICustomHttpClient FCustomHttpClient;

        public Metrics(SonarQube ASonarQube, ICustomHttpClient ACustomHttpClient)
        {
            FSonarQube = ASonarQube;
            FCustomHttpClient = ACustomHttpClient;
        }

        /// <summary>
        /// Returns badge from SonarQube server for given project name and metric type.
        /// All badges have the same style.
        /// </summary>
        /// <param name="AProject">SonarQube analysis project name</param>
        /// <param name="AMetric">SonarQube metric type</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet]
        [ETagFilter(200)]
        public async Task<ContentResult> GetMetrics([FromQuery] string AProject, string AMetric)
        {
            try
            {
                if (string.IsNullOrEmpty(AProject) && string.IsNullOrEmpty(AMetric))
                    return FCustomHttpClient.GetContentResult(
                        (int)HttpStatusCode.BadRequest, 
                        $"Parameters '{nameof(AProject)}' and '{nameof(AMetric)}' are missing", 
                        Constants.ContentTypes.TEXT_PLAIN);

                var LParameterList = new Dictionary<string, string>
                {
                    { nameof(AProject), AProject },
                    { nameof(AMetric), AMetric }
                };
                
                var LMissingParameterName = FCustomHttpClient.GetFirstEmptyParameterName(LParameterList);

                if (!string.IsNullOrEmpty(LMissingParameterName))
                    return FCustomHttpClient.GetContentResult(
                        (int)HttpStatusCode.BadRequest, 
                        $"Parameter '{LMissingParameterName}' is missing", 
                        Constants.ContentTypes.TEXT_PLAIN);

                if (!Constants.MetricNames.NameList.Contains(AMetric))
                    return FCustomHttpClient.GetContentResult(
                        (int)HttpStatusCode.BadRequest, 
                        $"Parameter '{nameof(AMetric)}' is invalid.", 
                        Constants.ContentTypes.TEXT_PLAIN);

                var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/measure?project={AProject}&metric={AMetric}";
                var LAuthentication = new BasicAuthentication { Login = FSonarQube.Token, Password = string.Empty };
                var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET", Authentication = LAuthentication};
                var (LContent, LStatusCode) = await FCustomHttpClient.Execute(LConfiguration);

                return FCustomHttpClient.GetContentResult(
                    (int)LStatusCode, 
                    LContent, 
                    Constants.ContentTypes.IMAGE_SVG);
            }
            catch (Exception LException)
            {
                return FCustomHttpClient.GetContentResult(
                    (int)HttpStatusCode.InternalServerError, 
                    LException.Message, 
                    Constants.ContentTypes.TEXT_PLAIN);
            }
        }

        /// <summary>
        /// Returns large quality gate badge from SonarQube server for given project name.
        /// </summary>
        /// <param name="AProject">SonarQube analysis project name</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet("Quality")]
        [ETagFilter(200)]
        public async Task<ContentResult> GetQualityGate([FromQuery] string AProject)
        {
            try
            {
                if (string.IsNullOrEmpty(AProject))
                    return FCustomHttpClient.GetContentResult(
                        (int)HttpStatusCode.BadRequest, 
                        $"Parameter '{nameof(AProject)}' is missing", 
                        Constants.ContentTypes.TEXT_PLAIN);

                var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/quality_gate?project={AProject}";
                var LAuthentication = new BasicAuthentication { Login = FSonarQube.Token, Password = string.Empty };
                var LConfiguration = new Configuration { Url = LRequestUrl, Method = "GET", Authentication = LAuthentication};
                var (LContent, LStatusCode) = await FCustomHttpClient.Execute(LConfiguration);

                return FCustomHttpClient.GetContentResult(
                    (int)LStatusCode, 
                    LContent, 
                    Constants.ContentTypes.IMAGE_SVG);
            }
            catch (Exception LException)
            {
                return FCustomHttpClient.GetContentResult(
                    (int)HttpStatusCode.InternalServerError, 
                    LException.Message, 
                    Constants.ContentTypes.TEXT_PLAIN);
            }
        }
    }
}