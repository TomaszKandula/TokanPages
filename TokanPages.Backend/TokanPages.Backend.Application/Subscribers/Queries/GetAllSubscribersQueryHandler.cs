using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Subscribers.Queries;

public class GetAllSubscribersQueryHandler : RequestHandler<GetAllSubscribersQuery, List<GetAllSubscribersQueryResult>>
{
    public GetAllSubscribersQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetAllSubscribersQueryResult>> Handle(GetAllSubscribersQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Subscribers
            .AsNoTracking()
            .Select(subscribers => new GetAllSubscribersQueryResult 
            { 
                Id = subscribers.Id,
                Email = subscribers.Email,
                IsActivated = subscribers.IsActivated,
                NewsletterCount = subscribers.Count
            })
            .ToListAsync(cancellationToken);
    }
}