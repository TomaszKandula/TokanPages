using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewslettersQueryHandler : RequestHandler<GetNewslettersQuery, List<GetNewslettersQueryResult>>
{
    public GetNewslettersQueryHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService) : base(operationsDbContext, loggerService) { }

    public override async Task<List<GetNewslettersQueryResult>> Handle(GetNewslettersQuery request, CancellationToken cancellationToken) 
    {
        return await OperationsDbContext.Newsletters
            .AsNoTracking()
            .Select(newsletter => new GetNewslettersQueryResult 
            { 
                Id = newsletter.Id,
                Email = newsletter.Email,
                IsActivated = newsletter.IsActivated,
                NewsletterCount = newsletter.Count
            })
            .ToListAsync(cancellationToken);
    }
}