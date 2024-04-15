using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Newsletters.Queries;

public class GetNewslettersQueryHandler : RequestHandler<GetNewslettersQuery, List<GetNewslettersQueryResult>>
{
    public GetNewslettersQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetNewslettersQueryResult>> Handle(GetNewslettersQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Newsletters
            .AsNoTracking()
            .Select(subscribers => new GetNewslettersQueryResult 
            { 
                Id = subscribers.Id,
                Email = subscribers.Email,
                IsActivated = subscribers.IsActivated,
                NewsletterCount = subscribers.Count
            })
            .ToListAsync(cancellationToken);
    }
}