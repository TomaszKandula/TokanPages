using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Logger;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.TemplateHelper;
using Templates = TokanPages.Backend.Shared.Constants.Emails.Templates;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendNewsletterCommandHandler : TemplateHandler<SendNewsletterCommand, Unit>
    {
        private readonly ILogger FLogger;

        private readonly HttpClient FHttpClient;
        
        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateHelper FTemplateHelper;
        
        private readonly AzureStorageSettingsModel FAzureStorageSettingsModel;
        
        private readonly ApplicationPathsModel FApplicationPathsModel;

        public SendNewsletterCommandHandler(ILogger ALogger, HttpClient AHttpClient, ISmtpClientService ASmtpClientService, 
            ITemplateHelper ATemplateHelper, AzureStorageSettingsModel AAzureStorageSettingsModel, ApplicationPathsModel AApplicationPathsModel)
        {
            FLogger = ALogger;
            FHttpClient = AHttpClient;
            FSmtpClientService = ASmtpClientService;
            FTemplateHelper = ATemplateHelper;
            FAzureStorageSettingsModel = AAzureStorageSettingsModel;
            FApplicationPathsModel = AApplicationPathsModel;
        }

        public override async Task<Unit> Handle(SendNewsletterCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LUpdateSubscriberBaseLink = FApplicationPathsModel.DeploymentOrigin + FApplicationPathsModel.UpdateSubscriberPath;
            var LUnsubscribeBaseLink = FApplicationPathsModel.DeploymentOrigin + FApplicationPathsModel.UnsubscribePath;

            FLogger.LogInformation($"Update subscriber base URL: {LUpdateSubscriberBaseLink}.");
            FLogger.LogInformation($"Unsubscribe base URL: {LUnsubscribeBaseLink}.");
            
            foreach (var LSubscriber in ARequest.SubscriberInfo)
            {
                FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
                FSmtpClientService.Tos = new List<string> { LSubscriber.Email };
                FSmtpClientService.Bccs = null;
                FSmtpClientService.Subject = ARequest.Subject;

                var LUpdateSubscriberLink = LUpdateSubscriberBaseLink + LSubscriber.Id;
                var LUnsubscribeLink = LUnsubscribeBaseLink + LSubscriber.Id;
                var LNewValues = new List<TemplateItemModel>
                {
                    new () { Tag = "{CONTENT}", Value = ARequest.Message },
                    new () { Tag = "{UPDATE_EMAIL_LINK}", Value = LUpdateSubscriberLink },
                    new () { Tag = "{UNSUBSCRIBE_LINK}", Value = LUnsubscribeLink }
                };

                var LUrl = $"{FAzureStorageSettingsModel.BaseUrl}{Templates.NEWSLETTER}";
                FLogger.LogInformation($"Getting newsletter template from URL: {LUrl}.");
                
                var LTemplateFromUrl = await FHttpClient.GetAsync(LUrl, ACancellationToken);
                var LTemplate = await LTemplateFromUrl.Content.ReadAsStringAsync(ACancellationToken);
                FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplate, LNewValues);

                var LResult = await FSmtpClientService.Send(ACancellationToken);
                if (!LResult.IsSucceeded) 
                    throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
