using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.MetricsService.Abstractions;

namespace TokanPages.Backend.Application.Content.Metrics.Queries;

public class GetQualityGateQueryHandler : RequestHandler<GetQualityGateQuery, IActionResult>
{
    private readonly IMetricsService _metricsService;

    public GetQualityGateQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IMetricsService metricsService) : base(operationDbContext, loggerService)
        => _metricsService = metricsService;

    public override async Task<IActionResult> Handle(GetQualityGateQuery request, CancellationToken cancellationToken)
    {
        return await _metricsService.GetQualityGate(request.Project);
    }
}