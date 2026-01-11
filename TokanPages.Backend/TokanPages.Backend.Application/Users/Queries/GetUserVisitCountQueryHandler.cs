using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserVisitCountQueryHandler : RequestHandler<GetUserVisitCountQuery, GetUserVisitCountQueryResult>
{
    public GetUserVisitCountQueryHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService) 
        : base(operationsDbContext, loggerService) { }

    public override async Task<GetUserVisitCountQueryResult> Handle(GetUserVisitCountQuery request, CancellationToken cancellationToken)
    {
        var uniqueCount = await (from httpRequest in OperationsDbContext.HttpRequests
            select new { httpRequest.SourceAddress })
            .Distinct()
            .CountAsync(cancellationToken);
        
        return new GetUserVisitCountQueryResult
        {
            UniqueCount = uniqueCount
        };
    }
}