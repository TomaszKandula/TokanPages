using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Services.MetricsService.Abstractions;

public interface IMetricsService
{
    Task<IActionResult> GetMetrics(string project, string metric);

    Task<IActionResult> GetQualityGate(string project);
}