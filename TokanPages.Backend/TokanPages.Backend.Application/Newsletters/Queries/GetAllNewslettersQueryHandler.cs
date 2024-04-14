using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Newsletters.Queries;

public class GetAllNewslettersQueryHandler : RequestHandler<GetAllNewslettersQuery, List<GetAllNewslettersQueryResult>>
{
    public GetAllNewslettersQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<List<GetAllNewslettersQueryResult>> Handle(GetAllNewslettersQuery request, CancellationToken cancellationToken) 
    {
        return await DatabaseContext.Newsletters
            .AsNoTracking()
            .Select(subscribers => new GetAllNewslettersQueryResult 
            { 
                Id = subscribers.Id,
                Email = subscribers.Email,
                IsActivated = subscribers.IsActivated,
                NewsletterCount = subscribers.Count
            })
            .ToListAsync(cancellationToken);
    }
}