using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Services.MetricsService;

public interface IMetricsService
{
    Task<IActionResult> GetMetrics(string project, string metric);

    Task<IActionResult> GetQualityGate(string project);
}