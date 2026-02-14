using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.MetricsService.Abstractions;

namespace TokanPages.Backend.Application.Content.Metrics.Queries;

public class GetQualityGateQueryHandler : RequestHandler<GetQualityGateQuery, IActionResult>
{
    private readonly IMetricsService _metricsService;

    public GetQualityGateQueryHandler(ILoggerService loggerService, IMetricsService metricsService) 
        : base(loggerService) => _metricsService = metricsService;

    public override async Task<IActionResult> Handle(GetQualityGateQuery request, CancellationToken cancellationToken)
    {
        return await _metricsService.GetQualityGate(request.Project);
    }
}