using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class AddNewsletterCommandHandler : RequestHandler<AddNewsletterCommand, Guid>
{
    private readonly IDateTimeService _dateTimeService;

    public AddNewsletterCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService) : base(operationsDbContext, loggerService) => _dateTimeService = dateTimeService;

    public override async Task<Guid> Handle(AddNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var emailCollection = await OperationsDbContext.Newsletters
            .AsNoTracking()
            .Where(subscribers => subscribers.Email == request.Email)
            .ToListAsync(cancellationToken);

        if (emailCollection.Count == 1)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var newSubscriber = new Domain.Entities.Newsletter
        {
            Email = request.Email,
            Count = 0,
            IsActivated = true,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await OperationsDbContext.Newsletters.AddAsync(newSubscriber, cancellationToken);
        await OperationsDbContext.SaveChangesAsync(cancellationToken);
        return newSubscriber.Id;
    }
}