using MediatR;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Persistence.DataAccess.Repositories.User.Models;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class ResetUserPasswordCommandHandler : RequestHandler<ResetUserPasswordCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly AppSettingsModel _appSettings;

    private readonly IUserService _userService;
    
    private readonly IUserRepository _userRepository;
    
    private readonly IMessagingRepository _messagingRepository;

    public ResetUserPasswordCommandHandler(ILoggerService loggerService, IEmailSenderService emailSenderService, 
        IDateTimeService dateTimeService, IOptions<AppSettingsModel> options, IUserService userService, 
        IUserRepository userRepository, IMessagingRepository messagingRepository) : base(loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _appSettings = options.Value;
        _userService = userService;
        _userRepository = userRepository;
        _messagingRepository = messagingRepository;
    }

    public override async Task<Unit> Handle(ResetUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserDetails(request.EmailAddress);
        if (user is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var resetId = Guid.NewGuid();
        var resetMaturity = _appSettings.LimitResetMaturity;
        var resetPassword = new ResetUserPasswordDto
        {
            UserId = user.UserId,
            ResetMaturity = resetMaturity,
            ResetId = resetId
        };

        await _userRepository.ResetUserPassword(resetPassword);

        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var expirationDate = baseDateTime.AddMinutes(resetMaturity);

        var messageId = Guid.NewGuid();
        await _messagingRepository.CreateServiceBusMessage(messageId);
        var configuration = new ResetPasswordConfiguration
        {
            LanguageId = request.LanguageId,
            MessageId = messageId,
            EmailAddress = request.EmailAddress,
            ResetId = resetId,
            ExpirationDate = expirationDate
        };

        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);
        return Unit.Value;
    }
}