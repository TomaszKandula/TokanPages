namespace TokanPages.WebApi.Controllers.Proxy
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Services.Caching.Metrics;

    public class Metrics : ProxyBaseController
    {
        private readonly IMetricsCache _metricsCache;

        public Metrics(IMetricsCache metricsCache) => _metricsCache = metricsCache;

        /// <summary>
        /// Returns badge from SonarQube server for given project name and metric type.
        /// All badges have the same style.
        /// </summary>
        /// <param name="project">SonarQube analysis project name</param>
        /// <param name="metric">SonarQube metric type</param>
        /// <param name="noCache">Allows to disable response cache.</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet]
        public async Task<IActionResult> GetMetrics([FromQuery] string project, string metric, bool noCache = false)
            => await _metricsCache.GetMetrics(project, metric, noCache);

        /// <summary>
        /// Returns large quality gate badge from SonarQube server for given project name.
        /// </summary>
        /// <param name="project">SonarQube analysis project name</param>
        /// <param name="noCache">Allows to disable response cache.</param>
        /// <returns>SonarQube badge</returns>
        [HttpGet("Quality")]
        public async Task<IActionResult> GetQualityGate([FromQuery] string project, bool noCache = false)
            => await _metricsCache.GetQualityGate(project, noCache);
    }
}