using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserVisitCountQueryHandler : RequestHandler<GetUserVisitCountQuery, GetUserVisitCountQueryResult>
{
    public GetUserVisitCountQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) 
        : base(operationDbContext, loggerService) { }

    public override async Task<GetUserVisitCountQueryResult> Handle(GetUserVisitCountQuery request, CancellationToken cancellationToken)
    {
        var uniqueCount = await (from httpRequest in OperationDbContext.HttpRequests
            select new { httpRequest.SourceAddress })
            .Distinct()
            .CountAsync(cancellationToken);
        
        return new GetUserVisitCountQueryResult
        {
            UniqueCount = uniqueCount
        };
    }
}