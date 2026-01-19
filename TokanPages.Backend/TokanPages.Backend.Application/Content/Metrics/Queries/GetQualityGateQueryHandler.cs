using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.MetricsService;

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