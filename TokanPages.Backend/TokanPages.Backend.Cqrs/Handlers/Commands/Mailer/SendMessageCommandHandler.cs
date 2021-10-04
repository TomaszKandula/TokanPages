namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using System.Threading;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared;
    using SmtpClient;
    using Core.Logger;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;
    using MediatR;

    public class SendMessageCommandHandler : TemplateHandler<SendMessageCommand, Unit>
    {
        private readonly ILogger FLogger;

        private readonly ICustomHttpClient FCustomHttpClient;

        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateService FTemplateService;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly AzureStorage FAzureStorage;
        
        public SendMessageCommandHandler(ILogger ALogger, ICustomHttpClient ACustomHttpClient, ISmtpClientService ASmtpClientService, 
            ITemplateService ATemplateService, IDateTimeService ADateTimeService, AzureStorage AAzureStorage)
        {
            FLogger = ALogger;
            FCustomHttpClient = ACustomHttpClient;
            FSmtpClientService = ASmtpClientService;
            FTemplateService = ATemplateService;
            FDateTimeService = ADateTimeService;
            FAzureStorage = AAzureStorage;
        }

        public override async Task<Unit> Handle(SendMessageCommand ARequest, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { Constants.Emails.Addresses.CONTACT };
            FSmtpClientService.Subject = $"New user message from {ARequest.FirstName}";

            var LNewValues = new Dictionary<string, string>
            {
                { "{FIRST_NAME}", ARequest.FirstName },
                { "{LAST_NAME}", ARequest.LastName },
                { "{EMAIL_ADDRESS}", ARequest.UserEmail },
                { "{USER_MSG}", ARequest.Message },
                { "{DATE_TIME}", FDateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
            };

            var LUrl = $"{FAzureStorage.BaseUrl}{Constants.Emails.Templates.CONTACT_FORM}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LConfiguration = new Configuration { Url = LUrl, Method = "GET" };
            var LResults = await FCustomHttpClient.Execute(LConfiguration, ACancellationToken);
            var LTemplate = System.Text.Encoding.Default.GetString(LResults.Content);
            FSmtpClientService.HtmlBody = FTemplateService.MakeBody(LTemplate, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");

            return await Task.FromResult(Unit.Value);
        }
    }
}
