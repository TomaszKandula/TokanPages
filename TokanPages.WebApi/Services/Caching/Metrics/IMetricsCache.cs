namespace TokanPages.WebApi.Services.Caching.Metrics
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public interface IMetricsCache
    {
        Task<IActionResult> GetMetrics(string project, string metric, bool noCache = false);

        Task<IActionResult> GetQualityGate(string project, bool noCache = false);
    }
}