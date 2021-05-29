using System;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Shared.Settings;

namespace TokanPages.Controllers.Proxy
{
    [Route("api/v1/SonarQube/[controller]")]
    [ApiController]
    public class Metrics : ControllerBase
    {
        private const string AUTHORIZATION = "Authorization";
        
        private readonly SonarQube FSonarQube;
        
        public Metrics(SonarQube ASonarQube) => FSonarQube = ASonarQube;
        
        /// <summary>
        /// Returns badge from SonarQube server for given project name and metric type.
        /// All badges have the same style.
        /// </summary>
        /// <param name="AProject">SonarQube analysis project name</param>
        /// <param name="AMetric">SonarQube metric type</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet]
        public async Task<ContentResult> GetMetrics([FromQuery] string AProject, string AMetric)
        {
            try
            {
                if (string.IsNullOrEmpty(AProject) && string.IsNullOrEmpty(AMetric))
                    return GetContentResult(400, $"Parameters '{nameof(AProject)}' and '{nameof(AMetric)}' are missing");

                var LMissingParameterName = GetEmptyParameterName(AProject, nameof(AProject),
                    AMetric, nameof(AMetric));
                
                if (!string.IsNullOrEmpty(LMissingParameterName))
                    return GetContentResult(400, $"Parameter '{LMissingParameterName}' is missing");

                var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/measure?project={AProject}&metric={AMetric}";
                var LContent = await GetContent(LRequestUrl);
                return GetContentResult(200, LContent, Constants.ContentTypes.IMAGE_SVG);
            }
            catch (Exception LException)
            {
                return GetContentResult(500, LException.Message);
            }
        }

        /// <summary>
        /// Returns large quality gate badge from SonarQube server for given project name.
        /// </summary>
        /// <param name="AProject">SonarQube analysis project name</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet("Quality")]
        public async Task<ContentResult> GetQualityGate([FromQuery] string AProject)
        {
            try
            {
                if (string.IsNullOrEmpty(AProject))
                    return GetContentResult(400, $"Parameter '{nameof(AProject)}' is missing");
                
                var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/quality_gate?project={AProject}";
                var LContent = await GetContent(LRequestUrl);
                return GetContentResult(200, LContent, Constants.ContentTypes.IMAGE_SVG);
            }
            catch (Exception LException)
            {
                return GetContentResult(500, LException.Message);
            }
        }

        private async Task<string> GetContent(string ARequestUrl)
        {
            using var LHttpClient = new HttpClient();
            using var LRequest = new HttpRequestMessage(new HttpMethod("GET"), ARequestUrl);

            var LBase64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{FSonarQube.Token}:"));
            LRequest.Headers.TryAddWithoutValidation(AUTHORIZATION, $"Basic {LBase64Authorization}"); 
                
            var LResponse = await LHttpClient.SendAsync(LRequest);
            return await LResponse.Content.ReadAsStringAsync();
        }

        private static ContentResult GetContentResult(int? AStatusCode, string AContent, string AContentType = Constants.ContentTypes.TEXT_PLAIN)
        {
            return new () 
            {
                StatusCode = AStatusCode,
                ContentType = AContentType,
                Content = AContent
            };
        }

        private static string GetEmptyParameterName(string AFirstValue, string AFirstValueName, string ASecondValue, string ASecondValueName)
        {
            if (string.IsNullOrEmpty(AFirstValue))
                return AFirstValueName;

            return string.IsNullOrEmpty(ASecondValue) 
                ? ASecondValueName 
                : string.Empty;
        }
    }
}