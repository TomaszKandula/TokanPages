using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Subscribers.Commands;

public class AddSubscriberCommandHandler : RequestHandler<AddSubscriberCommand, Guid>
{
    private readonly IDateTimeService _dateTimeService;

    public AddSubscriberCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService) : base(databaseContext, loggerService) => _dateTimeService = dateTimeService;

    public override async Task<Guid> Handle(AddSubscriberCommand request, CancellationToken cancellationToken) 
    {
        var emailCollection = await DatabaseContext.Subscribers
            .AsNoTracking()
            .Where(subscribers => subscribers.Email == request.Email)
            .ToListAsync(cancellationToken);

        if (emailCollection.Count == 1)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var newSubscriber = new Domain.Entities.Subscribers
        {
            Email = request.Email,
            Count = 0,
            IsActivated = true,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await DatabaseContext.Subscribers.AddAsync(newSubscriber, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return newSubscriber.Id;
    }
}