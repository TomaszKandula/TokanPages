﻿using System.Globalization;
using MediatR;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Sender.Mailer.Commands;

public class SendMessageCommandHandler : RequestHandler<SendMessageCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IConfiguration _configuration;

    private readonly IUserService _userService;

    public SendMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, 
        IConfiguration configuration, IUserService userService) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _configuration = configuration;
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

        var baseUrl = _configuration.GetValue<string>("AZ_Storage_BaseUrl");
        var contactForm = _configuration.GetValue<string>("Paths_Templates_ContactForm");

        var templateUrl = $"{baseUrl}{contactForm}";
        var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
        LoggerService.LogInformation($"Getting email template from URL: {templateUrl}.");

        var contactAddress = _configuration.GetValue<string>("Email_Address_Contact");
        var messageId = Guid.NewGuid();

        var serviceBusMessage = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        var payload = new SendMessageConfiguration
        {
            MessageId = messageId,
            From = contactAddress,
            To = new List<string> { contactAddress },
            Subject = $"New user message from {request.FirstName}",
            Body = template.MakeBody(templateValues)
        };

        await DatabaseContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        await _emailSenderService.SendToServiceBus(payload, cancellationToken);

        return Unit.Value;
    }
}