namespace TokanPages.Backend.Application.Handlers.Commands.Subscribers;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using MediatR;

public class UpdateSubscriberCommandHandler : Application.RequestHandler<UpdateSubscriberCommand, Unit>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    public UpdateSubscriberCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IUserService userService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
    }

    public override async Task<Unit> Handle(UpdateSubscriberCommand request, CancellationToken cancellationToken) 
    {
        var subscriber = await DatabaseContext.Subscribers
            .Where(subscribers => subscribers.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (subscriber is null) 
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        var emailCollection = await DatabaseContext.Subscribers
            .AsNoTracking()
            .Where(subscribers => subscribers.Email == request.Email)
            .ToListAsync(cancellationToken);

        if (emailCollection.Count == 1)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var user = await _userService.GetUser(cancellationToken);
        subscriber.Email = request.Email ?? subscriber.Email;
        subscriber.Count = request.Count ?? subscriber.Count;
        subscriber.IsActivated = request.IsActivated ?? subscriber.IsActivated;
        subscriber.ModifiedAt = _dateTimeService.Now;
        subscriber.ModifiedBy = user?.UserId ?? Guid.Empty;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}