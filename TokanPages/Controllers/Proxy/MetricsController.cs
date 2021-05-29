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

                using var LHttpClient = new HttpClient();
                using var LRequest = new HttpRequestMessage(new HttpMethod("GET"), LRequestUrl);
                
                var LBase64Authorization = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{FSonarQube.Token}:"));
                LRequest.Headers.TryAddWithoutValidation(AUTHORIZATION, $"Basic {LBase64Authorization}"); 
                
                var LResponse = await LHttpClient.SendAsync(LRequest);
                var LContent = await LResponse.Content.ReadAsStringAsync();

                return new ContentResult 
                {
                    StatusCode = 200,
                    ContentType = Constants.ContentTypes.IMAGE_SVG,
                    Content = LContent
                };
            }
            catch (Exception LException)
            {
                return new ContentResult 
                {
                    StatusCode = 500,
                    ContentType = Constants.ContentTypes.TEXT_PLAIN,
                    Content = LException.Message
                };
            }
        }
    }
}