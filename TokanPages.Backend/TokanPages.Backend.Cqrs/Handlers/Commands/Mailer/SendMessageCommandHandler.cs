namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

using MediatR;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared;
using Database;
using Shared.Models;
using Shared.Services;
using Core.Extensions;
using Services.EmailSenderService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;

public class SendMessageCommandHandler : Cqrs.RequestHandler<SendMessageCommand, Unit>
{
    private readonly IEmailSenderService _emailSenderService;
        
    private readonly IDateTimeService _dateTimeService;

    private readonly IApplicationSettings _applicationSettings;
        
    public SendMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IEmailSenderService emailSenderService, IDateTimeService dateTimeService, 
        IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _emailSenderService = emailSenderService;
        _dateTimeService = dateTimeService;
        _applicationSettings = applicationSettings;
    }

    public override async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var templateValues = new Dictionary<string, string>
        {
            { "{FIRST_NAME}", request.FirstName },
            { "{LAST_NAME}", request.LastName },
            { "{EMAIL_ADDRESS}", request.UserEmail },
            { "{USER_MSG}", request.Message },
            { "{DATE_TIME}", _dateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
        };

        var templateUrl = $"{_applicationSettings.AzureStorage.BaseUrl}{Constants.Emails.Templates.ContactForm}";
        var template = await _emailSenderService.GetEmailTemplate(templateUrl, cancellationToken);
        LoggerService.LogInformation($"Getting email template from URL: {templateUrl}.");

        var payload = new EmailSenderPayload
        {
            From = Constants.Emails.Addresses.Contact,
            To = new List<string> { Constants.Emails.Addresses.Contact },
            Subject = $"New user message from {request.FirstName}",
            Body = template.MakeBody(templateValues)
        };

        await _emailSenderService.SendEmail(payload, cancellationToken);
        return Unit.Value;
    }
}