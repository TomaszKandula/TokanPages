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
                var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/measure?project={AProject}&metric={AMetric}";
                var LContent = await GetContent(LRequestUrl);
                return GetContentResult(200, Constants.ContentTypes.IMAGE_SVG, LContent);
            }
            catch (Exception LException)
            {
                return GetContentResult(500, Constants.ContentTypes.TEXT_PLAIN, LException.Message);
            }
        }

        /// <summary>
        /// Returns large quality gate badge from SonarQube server for given project name.
        /// </summary>
        /// <param name="AProject">SonarQube analysis project name</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet]
        public async Task<ContentResult> GetQualityGate([FromQuery] string AProject)
        {
            try
            {
                var LRequestUrl = $"{FSonarQube.Server}/api/project_badges/quality_gate?project={AProject}";
                var LContent = await GetContent(LRequestUrl);
                return GetContentResult(200, Constants.ContentTypes.IMAGE_SVG, LContent);
            }
            catch (Exception LException)
            {
                return GetContentResult(500, Constants.ContentTypes.TEXT_PLAIN, LException.Message);
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

        private static ContentResult GetContentResult(int? AStatusCode, string AContentType, string AContent)
        {
            return new () 
            {
                StatusCode = AStatusCode,
                ContentType = AContentType,
                Content = AContent
            };
        }
    }
}