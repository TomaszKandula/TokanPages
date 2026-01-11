using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetBatchProcessingStatusesQueryHandler : RequestHandler<GetBatchProcessingStatusesQuery, IEnumerable<GetBatchProcessingStatusesQueryResult>>
{
    public GetBatchProcessingStatusesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService)
        : base(databaseContext, loggerService) { }

    public override async Task<IEnumerable<GetBatchProcessingStatusesQueryResult>> Handle(GetBatchProcessingStatusesQuery request, CancellationToken cancellationToken)
    {
        var statuses = Enum.GetValues<ProcessingStatus>();
        var result = statuses
            .Select((processingStatuses, index) => new GetBatchProcessingStatusesQueryResult
            {
                SystemCode = index,
                ProcessingStatus = processingStatuses.ToString().ToUpper()
            })
            .WhereIf(
                !string.IsNullOrEmpty(request.FilterBy), 
                response => response.ProcessingStatus.Equals(request.FilterBy, StringComparison.InvariantCultureIgnoreCase))
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} batch processing status(es)");
        return await Task.FromResult(result);
    }
}