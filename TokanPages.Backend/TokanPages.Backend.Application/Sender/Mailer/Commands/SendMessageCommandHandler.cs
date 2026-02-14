using System.Globalization;
using MediatR;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Sender.Mailer.Commands;

public class SendMessageCommandHandler : RequestHandler<SendMessageCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly AppSettingsModel _appSettings;

    private readonly IUserService _userService;

    private readonly IMessagingRepository _messagingRepository;
    
    private readonly ISenderRepository _senderRepository;

    public SendMessageCommandHandler(ILoggerService loggerService, IEmailSenderService emailSenderService, 
        IDateTimeService dateTimeService, IOptions<AppSettingsModel> options, IUserService userService, 
        IMessagingRepository messagingRepository, ISenderRepository senderRepository) : base(loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _appSettings = options.Value;
        _userService = userService;
        _messagingRepository = messagingRepository;
        _senderRepository = senderRepository;
    }

    public override async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var dateTime = baseDateTime.ToString(CultureInfo.InvariantCulture);

        var baseUrl = _appSettings.AzStorageBaseUrl;
        var contactForm = _appSettings.PathsTemplatesContactForm;

        var templateUrl = $"{baseUrl}{contactForm}";
        var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
        LoggerService.LogInformation($"Getting email template from URL: {templateUrl}.");

        var contactAddress = _appSettings.EmailAddressContact;
        var messageId = Guid.NewGuid();

        var templateValues = new Dictionary<string, string>
        {
            { "{FIRST_NAME}", request.FirstName },
            { "{LAST_NAME}", request.LastName },
            { "{EMAIL_ADDRESS}", request.UserEmail },
            { "{DATE_TIME}", dateTime }
        };

        var message = new SendMessageConfiguration
        {
            MessageId = messageId,
            From = contactAddress,
            To = new List<string> { contactAddress },
            Subject = $"New user message from {request.FirstName}"
        };

        if (!string.IsNullOrWhiteSpace(request.BusinessData))
        {
            var payloadId = Guid.NewGuid();
            templateValues.Add("{USER_MSG}", $"New business inquiry registered with the System ID: {payloadId}.");
            message.Subject = "New business inquiry";
            await _senderRepository.CreateBusinessInquiry(request.BusinessData);
        }
        else
        {
            templateValues.Add("{USER_MSG}", request.Message);
        }

        message.Body = template.MakeBody(templateValues);

        await _messagingRepository.CreateServiceBusMessage(messageId);
        await _emailSenderService.SendToServiceBus(message, cancellationToken);

        return Unit.Value;
    }
}