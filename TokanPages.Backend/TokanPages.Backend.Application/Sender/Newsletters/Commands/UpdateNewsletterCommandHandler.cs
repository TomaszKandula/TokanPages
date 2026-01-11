using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Sender.Newsletters.Commands;

public class UpdateNewsletterCommandHandler : RequestHandler<UpdateNewsletterCommand, Unit>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    public UpdateNewsletterCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IUserService userService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
    }

    public override async Task<Unit> Handle(UpdateNewsletterCommand request, CancellationToken cancellationToken) 
    {
        var newsletterData = await DatabaseContext.Newsletters
            .Where(newsletter => newsletter.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (newsletterData is null) 
            throw new BusinessException(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS), ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);

        var emailCollection = await DatabaseContext.Newsletters
            .AsNoTracking()
            .Where(newsletter => newsletter.Email == request.Email)
            .ToListAsync(cancellationToken);

        if (emailCollection.Count == 1)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var user = await _userService.GetUser(cancellationToken);
        newsletterData.Email = request.Email ?? newsletterData.Email;
        newsletterData.Count = request.Count ?? newsletterData.Count;
        newsletterData.IsActivated = request.IsActivated ?? newsletterData.IsActivated;
        newsletterData.ModifiedAt = _dateTimeService.Now;
        newsletterData.ModifiedBy = user?.UserId ?? Guid.Empty;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}