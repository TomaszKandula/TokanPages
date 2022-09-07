namespace TokanPages.Backend.Cqrs.Handlers.Queries.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Utilities.LoggerService;

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