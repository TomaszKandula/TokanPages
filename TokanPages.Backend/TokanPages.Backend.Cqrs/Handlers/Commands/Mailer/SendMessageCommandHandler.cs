using System.Net.Http;
using System.Threading;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.DateTimeService;
using Templates = TokanPages.Backend.Shared.Constants.Emails.Templates;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendMessageCommandHandler : TemplateHandler<SendMessageCommand, Unit>
    {
        private readonly ILogger FLogger;

        private readonly HttpClient FHttpClient;

        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateHelper FTemplateHelper;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly AzureStorageSettingsModel FAzureStorageSettingsModel;
        
        public SendMessageCommandHandler(ILogger ALogger, HttpClient AHttpClient, ISmtpClientService ASmtpClientService, 
            ITemplateHelper ATemplateHelper, IDateTimeService ADateTimeService, AzureStorageSettingsModel AAzureStorageSettingsModel)
        {
            FLogger = ALogger;
            FHttpClient = AHttpClient;
            FSmtpClientService = ASmtpClientService;
            FTemplateHelper = ATemplateHelper;
            FDateTimeService = ADateTimeService;
            FAzureStorageSettingsModel = AAzureStorageSettingsModel;
        }

        public override async Task<Unit> Handle(SendMessageCommand ARequest, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { Constants.Emails.Addresses.CONTACT };
            FSmtpClientService.Subject = $"New user message from {ARequest.FirstName}";

            var LNewValues = new List<TemplateItemModel>
            {
                new () { Tag = "{FIRST_NAME}", Value = ARequest.FirstName },
                new () { Tag = "{LAST_NAME}", Value = ARequest.LastName },
                new () { Tag = "{EMAIL_ADDRESS}", Value = ARequest.UserEmail },
                new () { Tag = "{USER_MSG}", Value = ARequest.Message },
                new () { Tag = "{DATE_TIME}", Value = FDateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
            };

            var LUrl = $"{FAzureStorageSettingsModel.BaseUrl}{Templates.CONTACT_FORM}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LTemplateFromUrl = await FHttpClient.GetAsync(LUrl, ACancellationToken);
            var LTemplate = await LTemplateFromUrl.Content.ReadAsStringAsync(ACancellationToken);
            FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplate, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");

            return await Task.FromResult(Unit.Value);
        }
    }
}
