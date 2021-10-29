namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using MediatR;
    using System.Threading;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared;
    using Database;
    using SmtpClient;
    using Core.Utilities.LoggerService;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class SendMessageCommandHandler : TemplateHandler<SendMessageCommand, Unit>
    {
        private readonly ICustomHttpClient _customHttpClient;

        private readonly ISmtpClientService _smtpClientService;
        
        private readonly ITemplateService _templateService;
        
        private readonly IDateTimeService _dateTimeService;
        
        private readonly AzureStorage _azureStorage;
        
        public SendMessageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
            ICustomHttpClient customHttpClient, ISmtpClientService smtpClientService, ITemplateService templateService, 
            IDateTimeService dateTimeService, AzureStorage azureStorage) : base(databaseContext, loggerService)
        {
            _customHttpClient = customHttpClient;
            _smtpClientService = smtpClientService;
            _templateService = templateService;
            _dateTimeService = dateTimeService;
            _azureStorage = azureStorage;
        }

        public override async Task<Unit> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            _smtpClientService.From = Constants.Emails.Addresses.Contact;
            _smtpClientService.Tos = new List<string> { Constants.Emails.Addresses.Contact };
            _smtpClientService.Subject = $"New user message from {request.FirstName}";

            var newValues = new Dictionary<string, string>
            {
                { "{FIRST_NAME}", request.FirstName },
                { "{LAST_NAME}", request.LastName },
                { "{EMAIL_ADDRESS}", request.UserEmail },
                { "{USER_MSG}", request.Message },
                { "{DATE_TIME}", _dateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
            };

            var url = $"{_azureStorage.BaseUrl}{Constants.Emails.Templates.ContactForm}";
            LoggerService.LogInformation($"Getting email template from URL: {url}.");

            var configuration = new Configuration { Url = url, Method = "GET" };
            var results = await _customHttpClient.Execute(configuration, cancellationToken);

            if (results.Content == null)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

            var template = System.Text.Encoding.Default.GetString(results.Content);
            _smtpClientService.HtmlBody = _templateService.MakeBody(template, newValues);

            var result = await _smtpClientService.Send(cancellationToken);
            if (!result.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {result.ErrorDesc}");

            return await Task.FromResult(Unit.Value);
        }
    }
}
