namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Shared.Services;
using Shared.Resources;
using Services.EmailSenderService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using Services.EmailSenderService.Models;

public class ResetUserPasswordCommandHandler : Cqrs.RequestHandler<ResetUserPasswordCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IApplicationSettings _applicationSettings;

    public ResetUserPasswordCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, 
        IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _applicationSettings = applicationSettings;
    }

    public override async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var users = await DatabaseContext.Users
            .Where(users => users.EmailAddress == request.EmailAddress)
            .ToListAsync(cancellationToken);

        if (!users.Any())
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var currentUser = users.First();
        var resetId = Guid.NewGuid();
        var expirationDate = _dateTimeService.Now.AddMinutes(_applicationSettings.ExpirationSettings.ResetIdExpiresIn);
        currentUser.CryptedPassword = string.Empty;
        currentUser.ResetId = resetId;
        currentUser.ResetIdEnds = expirationDate;
        currentUser.LastUpdated = _dateTimeService.Now;
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        await SendNotification(request.EmailAddress, resetId, expirationDate, cancellationToken);

        return Unit.Value;
    }

    private async Task SendNotification(string emailAddress, Guid resetId, DateTime expirationDate, CancellationToken cancellationToken)
    {
        var configuration = new ResetPasswordConfiguration
        {
            EmailAddress = emailAddress,
            ResetId = resetId,
            ExpirationDate = expirationDate
        };

        await _emailSenderService.SendNotification(configuration, cancellationToken);
    }
}