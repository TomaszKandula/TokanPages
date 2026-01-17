using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewslettersQueryHandler : RequestHandler<GetNewslettersQuery, List<GetNewslettersQueryResult>>
{
    public GetNewslettersQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) : base(operationDbContext, loggerService) { }

    public override async Task<List<GetNewslettersQueryResult>> Handle(GetNewslettersQuery request, CancellationToken cancellationToken) 
    {
        return await OperationDbContext.Newsletters
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