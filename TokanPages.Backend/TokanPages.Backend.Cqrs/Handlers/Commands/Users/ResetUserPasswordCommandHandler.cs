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
using Services.UserService;
using Services.EmailSenderService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using Services.EmailSenderService.Models;

public class ResetUserPasswordCommandHandler : Cqrs.RequestHandler<ResetUserPasswordCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IApplicationSettings _applicationSettings;

    private readonly IUserService _userService;

    public ResetUserPasswordCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, IApplicationSettings applicationSettings, 
        IUserService userService) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _applicationSettings = applicationSettings;
        _userService = userService;
    }

    public override async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var currentUser = await DatabaseContext.Users
            .Where(users => users.IsActivated)
            .Where(users => !users.IsDeleted)
            .Where(users => users.EmailAddress == request.EmailAddress)
            .SingleOrDefaultAsync(cancellationToken);

        if (currentUser is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var expiresIn = _applicationSettings.LimitSettings.ResetIdExpiresIn;
        var resetId = Guid.NewGuid();

        currentUser.CryptedPassword = string.Empty;
        currentUser.ResetId = resetId;
        currentUser.ResetIdEnds = _dateTimeService.Now.AddMinutes(expiresIn);
        currentUser.ModifiedAt = _dateTimeService.Now;
        currentUser.ModifiedBy = currentUser.Id;
        
        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var expirationDate = baseDateTime.AddMinutes(expiresIn);

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        await SendNotification(request.EmailAddress!, resetId, expirationDate, cancellationToken);

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