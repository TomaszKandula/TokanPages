using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Shared.Helpers;
using TokanPages.Backend.Shared.Attributes;

namespace TokanPages.WebApi.Controllers.Proxy
{
    [Route("api/v1/SonarQube/[controller]")]
    [ApiController]
    public class Metrics : ControllerBase
    {
        private readonly SonarQubeSettingsModel FSonarQubeSettingsModel;
        
        public Metrics(SonarQubeSettingsModel ASonarQubeSettingsModel) => FSonarQubeSettingsModel = ASonarQubeSettingsModel;
        
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
                    return HttpClientContent.GetContentResult(400, $"Parameters '{nameof(AProject)}' and '{nameof(AMetric)}' are missing");

                var LParameterList = new List<ParameterModel>
                {
                    new () { Key = nameof(AProject), Value = AProject },
                    new () { Key = nameof(AMetric), Value = AMetric }
                };
                
                var LMissingParameterName = HttpClientContent.GetFirstEmptyParameterName(LParameterList);
                
                if (!string.IsNullOrEmpty(LMissingParameterName))
                    return HttpClientContent.GetContentResult(400, $"Parameter '{LMissingParameterName}' is missing");

                if (!Constants.MetricNames.NameList.Contains(AMetric))
                    return HttpClientContent.GetContentResult(400, $"Parameter '{nameof(AMetric)}' is invalid.");

                var LRequestUrl = $"{FSonarQubeSettingsModel.Server}/api/project_badges/measure?project={AProject}&metric={AMetric}";
                var LContent = await HttpClientContent.GetContent(LRequestUrl, FSonarQubeSettingsModel.Token);
                return HttpClientContent.GetContentResult(200, LContent, Constants.ContentTypes.IMAGE_SVG);
            }
            catch (Exception LException)
            {
                return HttpClientContent.GetContentResult(500, LException.Message);
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
                    return HttpClientContent.GetContentResult(400, $"Parameter '{nameof(AProject)}' is missing");
                
                var LRequestUrl = $"{FSonarQubeSettingsModel.Server}/api/project_badges/quality_gate?project={AProject}";
                var LContent = await HttpClientContent.GetContent(LRequestUrl, FSonarQubeSettingsModel.Token);
                return HttpClientContent.GetContentResult(200, LContent, Constants.ContentTypes.IMAGE_SVG);
            }
            catch (Exception LException)
            {
                return HttpClientContent.GetContentResult(500, LException.Message);
            }
        }
    }
}