using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;
using MediatR;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Persistence.DataAccess.Repositories.User;

namespace TokanPages.Backend.Application.Users.Commands;

public class VerifyUserEmailCommandHandler : RequestHandler<VerifyUserEmailCommand, Unit>
{
    private readonly AppSettingsModel _appSettings;

    private readonly IUserService _userService;

    private readonly IUserRepository _userRepository;
    
    private readonly IMessagingRepository _messagingRepository;

    private readonly IDateTimeService _dateTimeService;

    private readonly IEmailSenderService _emailSenderService;

    public VerifyUserEmailCommandHandler(ILoggerService loggerService, IOptions<AppSettingsModel> options, 
        IUserService userService, IDateTimeService dateTimeService, IEmailSenderService emailSenderService, 
        IUserRepository userRepository, IMessagingRepository messagingRepository) : base(loggerService)
    {
        _appSettings = options.Value;
        _userService = userService;
        _dateTimeService = dateTimeService;
        _emailSenderService = emailSenderService;
        _userRepository = userRepository;
        _messagingRepository = messagingRepository;
    }

    public override async Task<Unit> Handle(VerifyUserEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser();

        var messageId = Guid.NewGuid();
        var activationId = Guid.NewGuid();
        var activationMaturity = _appSettings.LimitActivationMaturity;
        var activationIdEnds = _dateTimeService.Now.AddMinutes(activationMaturity);
        var configuration = new VerifyEmailConfiguration
        {
            LanguageId = request.LanguageId,
            MessageId = messageId,
            EmailAddress = request.EmailAddress,
            VerificationId = activationId,
            VerificationIdEnds = activationIdEnds
        };

        await _messagingRepository.CreateServiceBusMessage(messageId);
        await _userRepository.UpdateUserActivation(user.UserId, activationId, activationIdEnds);
        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);

        LoggerService.LogInformation("Sending activation email to verify user email address.");
        return Unit.Value;
    }
}