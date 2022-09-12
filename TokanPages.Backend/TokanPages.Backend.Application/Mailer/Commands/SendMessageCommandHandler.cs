﻿namespace TokanPages.Backend.Application.Mailer.Commands;

using MediatR;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Persistence.Database;
using Shared.Services;
using Core.Extensions;
using WebApi.Dto.Mailer;
using Services.UserService;
using Services.EmailSenderService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;

public class SendMessageCommandHandler : Application.RequestHandler<SendMessageCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IApplicationSettings _applicationSettings;

    private readonly IUserService _userService;

    public SendMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, 
        IApplicationSettings applicationSettings, IUserService userService) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _applicationSettings = applicationSettings;
        _userService = userService;
    }

    public override async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var dateTime = baseDateTime.ToString(CultureInfo.InvariantCulture);

        var templateValues = new Dictionary<string, string>
        {
            { "{FIRST_NAME}", request.FirstName },
            { "{LAST_NAME}", request.LastName },
            { "{EMAIL_ADDRESS}", request.UserEmail },
            { "{USER_MSG}", request.Message },
            { "{DATE_TIME}", dateTime }
        };

        var baseUrl = _applicationSettings.AzureStorage.BaseUrl;
        var contactForm = _applicationSettings.ApplicationPaths.Templates.ContactForm;

        var templateUrl = $"{baseUrl}{contactForm}";
        var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
        LoggerService.LogInformation($"Getting email template from URL: {templateUrl}.");

        var contact = _applicationSettings.EmailSender.Addresses.Contact;
        var payload = new SenderPayloadDto
        {
            From = contact,
            To = new List<string> { contact },
            Subject = $"New user message from {request.FirstName}",
            Body = template.MakeBody(templateValues)
        };

        await _emailSenderService.SendEmail(payload, cancellationToken);
        return Unit.Value;
    }
}