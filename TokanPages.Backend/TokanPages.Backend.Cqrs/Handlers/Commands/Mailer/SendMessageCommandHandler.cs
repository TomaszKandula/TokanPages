namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    using System.Net.Http;
    using System.Threading;
    using System.Globalization;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Shared;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Shared.Services.TemplateService;
    using Shared.Services.DateTimeService;
    using MediatR;

    public class SendMessageCommandHandler : TemplateHandler<SendMessageCommand, Unit>
    {
        private readonly ILogger FLogger;

        private readonly HttpClient FHttpClient;

        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateService FTemplateService;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly AzureStorage FAzureStorage;
        
        public SendMessageCommandHandler(ILogger ALogger, HttpClient AHttpClient, ISmtpClientService ASmtpClientService, 
            ITemplateService ATemplateService, IDateTimeService ADateTimeService, AzureStorage AAzureStorage)
        {
            FLogger = ALogger;
            FHttpClient = AHttpClient;
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

            var LNewValues = new List<TemplateItem>
            {
                new () { Tag = "{FIRST_NAME}", Value = ARequest.FirstName },
                new () { Tag = "{LAST_NAME}", Value = ARequest.LastName },
                new () { Tag = "{EMAIL_ADDRESS}", Value = ARequest.UserEmail },
                new () { Tag = "{USER_MSG}", Value = ARequest.Message },
                new () { Tag = "{DATE_TIME}", Value = FDateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
            };

            var LUrl = $"{FAzureStorage.BaseUrl}{Constants.Emails.Templates.CONTACT_FORM}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LTemplateFromUrl = await FHttpClient.GetAsync(LUrl, ACancellationToken);
            var LTemplate = await LTemplateFromUrl.Content.ReadAsStringAsync(ACancellationToken);
            FSmtpClientService.HtmlBody = FTemplateService.MakeBody(LTemplate, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");

            return await Task.FromResult(Unit.Value);
        }
    }
}
