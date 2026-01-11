using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.MetricsService;

namespace TokanPages.Backend.Application.Content.Metrics.Queries;

public class GetQualityGateQueryHandler : RequestHandler<GetQualityGateQuery, IActionResult>
{
    private readonly IMetricsService _metricsService;

    public GetQualityGateQueryHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, IMetricsService metricsService) : base(operationsDbContext, loggerService)
        => _metricsService = metricsService;

    public override async Task<IActionResult> Handle(GetQualityGateQuery request, CancellationToken cancellationToken)
    {
        return await _metricsService.GetQualityGate(request.Project);
    }
}