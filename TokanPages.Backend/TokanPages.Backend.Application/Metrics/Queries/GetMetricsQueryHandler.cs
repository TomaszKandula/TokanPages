using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.MetricsService;

namespace TokanPages.Backend.Application.Metrics.Queries;

public class GetMetricsQueryHandler : RequestHandler<GetMetricsQuery, IActionResult>
{
    private readonly IMetricsService _metricsService;

    public GetMetricsQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IMetricsService metricsService) : base(databaseContext, loggerService) 
        => _metricsService = metricsService;

    public override async Task<IActionResult> Handle(GetMetricsQuery request, CancellationToken cancellationToken)
    {
        return await _metricsService.GetMetrics(request.Project, request.Metric);
    }
}