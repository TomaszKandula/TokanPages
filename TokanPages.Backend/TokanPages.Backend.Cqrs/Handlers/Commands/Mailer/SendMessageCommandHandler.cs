namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

using MediatR;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Shared;
using Database;
using Shared.Models;
using Shared.Services;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using Core.Utilities.TemplateService;
using Core.Utilities.CustomHttpClient;
using Core.Utilities.CustomHttpClient.Models;

public class SendMessageCommandHandler : Cqrs.RequestHandler<SendMessageCommand, Unit>
{
    private readonly ICustomHttpClient _customHttpClient;
        
    private readonly ITemplateService _templateService;
        
    private readonly IDateTimeService _dateTimeService;

    private readonly IApplicationSettings _applicationSettings;
        
    public SendMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ICustomHttpClient customHttpClient, ITemplateService templateService, IDateTimeService dateTimeService, 
        IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _customHttpClient = customHttpClient;
        _templateService = templateService;
        _dateTimeService = dateTimeService;
        _applicationSettings = applicationSettings;
    }

    public override async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var newValues = new Dictionary<string, string>
        {
            { "{FIRST_NAME}", request.FirstName },
            { "{LAST_NAME}", request.LastName },
            { "{EMAIL_ADDRESS}", request.UserEmail },
            { "{USER_MSG}", request.Message },
            { "{DATE_TIME}", _dateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
        };

        var url = $"{_applicationSettings.AzureStorage.BaseUrl}{Constants.Emails.Templates.ContactForm}";
        LoggerService.LogInformation($"Getting email template from URL: {url}.");

        var configuration = new Configuration { Url = url, Method = "GET" };
        var getTemplate = await _customHttpClient.Execute(configuration, cancellationToken);

        if (getTemplate.Content == null)
            throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

        var template = Encoding.Default.GetString(getTemplate.Content);
        var payload = new EmailSenderPayload
        {
            PrivateKey = _applicationSettings.EmailSender.PrivateKey,
            From = Constants.Emails.Addresses.Contact,
            To = new List<string> { Constants.Emails.Addresses.Contact },
            Subject = $"New user message from {request.FirstName}",
            Body = _templateService.MakeBody(template, newValues)
        };

        configuration = new Configuration { Url = _applicationSettings.EmailSender.BaseUrl, Method = "POST", StringContent = 
            new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.Default, "application/json") };

        var sendEmail = await _customHttpClient.Execute(configuration, cancellationToken);
        if (sendEmail.StatusCode != HttpStatusCode.OK)
            throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}");

        return Unit.Value;
    }
}