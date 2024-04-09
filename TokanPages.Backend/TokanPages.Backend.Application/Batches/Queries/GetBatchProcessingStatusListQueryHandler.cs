using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Batches.Queries;

public class GetBatchProcessingStatusListQueryHandler : RequestHandler<GetBatchProcessingStatusListQuery, IEnumerable<GetBatchProcessingStatusListQueryResult>>
{
    private readonly IUserService _userService;

    public GetBatchProcessingStatusListQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService) 
        : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<IEnumerable<GetBatchProcessingStatusListQueryResult>> Handle(GetBatchProcessingStatusListQuery request, CancellationToken cancellationToken)
    {
        var statuses = Enum.GetValues<ProcessingStatuses>();
        var result = statuses
            .Select((processingStatuses, index) => new GetBatchProcessingStatusListQueryResult
            {
                SystemCode = index,
                ProcessingStatus = processingStatuses.ToString().ToUpper()
            })
            .WhereIf(
                !string.IsNullOrEmpty(request.FilterBy), 
                response => response.ProcessingStatus == request.FilterBy.ToUpper())
            .ToList();

        LoggerService.LogInformation($"Returned {result.Count} batch processing status(es)");
        return await Task.FromResult(result);
    }
}