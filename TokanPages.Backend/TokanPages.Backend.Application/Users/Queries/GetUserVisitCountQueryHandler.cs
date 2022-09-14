using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserVisitCountQueryHandler : RequestHandler<GetUserVisitCountQuery, GetUserVisitCountQueryResult>
{
    public GetUserVisitCountQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<GetUserVisitCountQueryResult> Handle(GetUserVisitCountQuery request, CancellationToken cancellationToken)
    {
        var uniqueCount = await (from httpRequest in DatabaseContext.HttpRequests
            select new { httpRequest.SourceAddress })
            .Distinct()
            .CountAsync(cancellationToken);
        
        return new GetUserVisitCountQueryResult
        {
            UniqueCount = uniqueCount
        };
    }
}