using TokanPages.Backend.Domain.Entities;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application.Users.Commands;

public class VerifyUserEmailCommandHandler : RequestHandler<VerifyUserEmailCommand, Unit>
{
    private readonly AppSettingsModel _appSettings;

    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IEmailSenderService _emailSenderService;

    public VerifyUserEmailCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IOptions<AppSettingsModel> options, IUserService userService, IDateTimeService dateTimeService, 
        IEmailSenderService emailSenderService) : base(operationDbContext, loggerService)
    {
        _appSettings = options.Value;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _emailSenderService = emailSenderService;
    }

    public override async Task<Unit> Handle(VerifyUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(isTracking: true, cancellationToken: cancellationToken);

        var activationId = Guid.NewGuid();
        var activationMaturity = _appSettings.LimitActivationMaturity;
        var activationIdEnds = _dateTimeService.Now.AddMinutes(activationMaturity);

        user.ActivationId = activationId;
        user.ActivationIdEnds = activationIdEnds;

        var messageId = await PrepareNotificationUncommitted(cancellationToken);
        await CommitAllChanges(cancellationToken);
        await SendNotification(messageId, request.LanguageId, request.EmailAddress, activationId, activationIdEnds, cancellationToken);

        LoggerService.LogInformation($"Sending activation email to verify user email address.");
        return Unit.Value;
    }

    private async Task CommitAllChanges(CancellationToken cancellationToken = default) 
        => await OperationDbContext.SaveChangesAsync(cancellationToken);

    private async Task<Guid> PrepareNotificationUncommitted(CancellationToken cancellationToken)
    {
        var messageId = Guid.NewGuid();
        var serviceBusMessage = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        await OperationDbContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        return messageId;
    }

    private async Task SendNotification(Guid messageId, string languageId, string emailAddress, Guid activationId, DateTime activationIdEnds, CancellationToken cancellationToken)
    {
        var configuration = new VerifyEmailConfiguration
        {
            LanguageId = languageId,
            MessageId = messageId,
            EmailAddress = emailAddress,
            VerificationId = activationId,
            VerificationIdEnds = activationIdEnds
        };

        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);
    }
}