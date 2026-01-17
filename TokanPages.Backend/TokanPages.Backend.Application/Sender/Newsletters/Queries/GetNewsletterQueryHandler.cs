using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewsletterQueryHandler : RequestHandler<GetNewsletterQuery, GetNewsletterQueryResult>
{
    public GetNewsletterQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) : base(operationDbContext, loggerService) { }

    public override async Task<GetNewsletterQueryResult> Handle(GetNewsletterQuery request, CancellationToken cancellationToken) 
    {
        var newsletterData = await OperationDbContext.Newsletters
            .AsNoTracking()
            .Where(newsletter => newsletter.Id == request.Id)
            .Select(newsletter => new GetNewsletterQueryResult 
            { 
                Id = newsletter.Id,
                Email = newsletter.Email,
                IsActivated = newsletter.IsActivated,
                NewsletterCount = newsletter.Count,
                CreatedAt = newsletter.CreatedAt,
                ModifiedAt = newsletter.ModifiedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        return newsletterData ?? throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
    }
}