using System.Globalization;
using MediatR;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.DataAccess.Contexts;
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

    public SendMessageCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, 
        IConfiguration configuration, IUserService userService) : base(operationDbContext, loggerService)
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
            From = contactAddress ?? "",
            To = new List<string> { contactAddress ?? "" },
            Subject = $"New user message from {request.FirstName}"
        };

        if (!string.IsNullOrWhiteSpace(request.BusinessData))
        {
            var payloadId = Guid.NewGuid();
            templateValues.Add("{USER_MSG}", $"New business inquiry registered with the System ID: {payloadId}.");
            message.Subject = "New business inquiry";
            var businessInquiry = new BusinessInquiry
            {
                Id = payloadId,
                JsonData = request.BusinessData,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = Guid.Empty
            };

            await OperationDbContext.BusinessInquiries.AddAsync(businessInquiry, cancellationToken);
        }
        else
        {
            templateValues.Add("{USER_MSG}", request.Message);
        }

        message.Body = template.MakeBody(templateValues);

        await OperationDbContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        await OperationDbContext.SaveChangesAsync(cancellationToken);
        await _emailSenderService.SendToServiceBus(message, cancellationToken);

        return Unit.Value;
    }
}