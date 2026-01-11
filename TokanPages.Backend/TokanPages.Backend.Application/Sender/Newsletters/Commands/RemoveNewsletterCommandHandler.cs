using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class RemoveNewsletterCommandHandler : RequestHandler<RemoveNewsletterCommand, Unit>
{
    public RemoveNewsletterCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var newsletterData = await DatabaseContext.Newsletters
            .Where(newsletter => newsletter.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);
            
        if (newsletterData is null)
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        DatabaseContext.Newsletters.Remove(newsletterData);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}