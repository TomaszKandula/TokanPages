using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class ResetUserPasswordCommandHandler : RequestHandler<ResetUserPasswordCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IConfiguration _configuration;

    private readonly IUserService _userService;

    public ResetUserPasswordCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, IConfiguration configuration, 
        IUserService userService) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _configuration = configuration;
        _userService = userService;
    }

    public override async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await DatabaseContext.Users
            .Where(users => users.IsActivated)
            .Where(users => !users.IsDeleted)
            .Where(users => users.EmailAddress == request.EmailAddress)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var resetId = Guid.NewGuid();
        var resetMaturity = _configuration.GetValue<int>("Limit_Reset_Maturity");

        user.CryptedPassword = string.Empty;
        user.ResetId = resetId;
        user.ResetIdEnds = _dateTimeService.Now.AddMinutes(resetMaturity);
        user.ModifiedAt = _dateTimeService.Now;
        user.ModifiedBy = user.Id;

        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var expirationDate = baseDateTime.AddMinutes(resetMaturity);

        var messageId = await PrepareNotificationUncommitted(cancellationToken);
        await CommitAllChanges(cancellationToken);
        await SendNotification(messageId, request.EmailAddress!, resetId, expirationDate, cancellationToken);

        return Unit.Value;
    }

    private async Task CommitAllChanges(CancellationToken cancellationToken = default) 
        => await DatabaseContext.SaveChangesAsync(cancellationToken);

    private async Task<Guid> PrepareNotificationUncommitted(CancellationToken cancellationToken)
    {
        var messageId = Guid.NewGuid();
        var serviceBusMessage = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        await DatabaseContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        return messageId;
    }

    private async Task SendNotification(Guid messageId, string emailAddress, Guid resetId, DateTime expirationDate, CancellationToken cancellationToken)
    {
        var configuration = new ResetPasswordConfiguration
        {
            MessageId = messageId,
            EmailAddress = emailAddress,
            ResetId = resetId,
            ExpirationDate = expirationDate
        };

        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);
    }
}