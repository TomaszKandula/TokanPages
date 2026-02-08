using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.MetricsService.Abstractions;

namespace TokanPages.Backend.Application.Content.Metrics.Queries;

public class GetMetricsQueryHandler : RequestHandler<GetMetricsQuery, IActionResult>
{
    private readonly IMetricsService _metricsService;

    public GetMetricsQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IMetricsService metricsService) : base(operationDbContext, loggerService) 
        => _metricsService = metricsService;

    public override async Task<IActionResult> Handle(GetMetricsQuery request, CancellationToken cancellationToken)
    {
        return await _metricsService.GetMetrics(request.Project, request.Metric);
    }
}