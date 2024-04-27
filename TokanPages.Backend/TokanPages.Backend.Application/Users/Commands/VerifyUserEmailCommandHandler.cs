using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace TokanPages.Backend.Application.Users.Commands;

public class VerifyUserEmailCommandHandler : RequestHandler<VerifyUserEmailCommand, Unit>
{
    private readonly IConfiguration _configuration;

    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IEmailSenderService _emailSenderService;

    public VerifyUserEmailCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IConfiguration configuration, IUserService userService, IDateTimeService dateTimeService, 
        IEmailSenderService emailSenderService) : base(databaseContext, loggerService)
    {
        _configuration = configuration;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _emailSenderService = emailSenderService;
    }

    public override async Task<Unit> Handle(VerifyUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(isTracking: true, cancellationToken: cancellationToken);

        var activationId = Guid.NewGuid();
        var activationMaturity = _configuration.GetValue<int>("Limit_Activation_Maturity");
        var activationIdEnds = _dateTimeService.Now.AddMinutes(activationMaturity);

        user.ActivationId = activationId;
        user.ActivationIdEnds = activationIdEnds;

        var messageId = await PrepareNotificationUncommitted(cancellationToken);
        await CommitAllChanges(cancellationToken);
        await SendNotification(messageId, request.EmailAddress, activationId, activationIdEnds, cancellationToken);

        LoggerService.LogInformation($"Sending activation email to verify user email address.");
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

    private async Task SendNotification(Guid messageId, string emailAddress, Guid activationId, DateTime activationIdEnds, CancellationToken cancellationToken)
    {
        var configuration = new VerifyEmailConfiguration
        {
            MessageId = messageId,
            EmailAddress = emailAddress,
            VerificationId = activationId,
            VerificationIdEnds = activationIdEnds
        };

        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);
    }
}