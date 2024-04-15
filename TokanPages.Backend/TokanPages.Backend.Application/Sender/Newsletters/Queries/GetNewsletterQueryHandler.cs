using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Sender.Newsletters.Queries;

public class GetNewsletterQueryHandler : RequestHandler<GetNewsletterQuery, GetNewsletterQueryResult>
{
    public GetNewsletterQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<GetNewsletterQueryResult> Handle(GetNewsletterQuery request, CancellationToken cancellationToken) 
    {
        var subscriber = await DatabaseContext.Newsletters
            .AsNoTracking()
            .Where(subscribers => subscribers.Id == request.Id)
            .Select(subscribers => new GetNewsletterQueryResult 
            { 
                Id = subscribers.Id,
                Email = subscribers.Email,
                IsActivated = subscribers.IsActivated,
                NewsletterCount = subscribers.Count,
                CreatedAt = subscribers.CreatedAt,
                ModifiedAt = subscribers.ModifiedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (subscriber is null) 
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        return subscriber;
    }
}