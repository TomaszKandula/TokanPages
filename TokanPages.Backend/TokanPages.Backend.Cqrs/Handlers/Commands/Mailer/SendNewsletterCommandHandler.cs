using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.TemplateHelper.Model;
using Templates = TokanPages.Backend.Shared.Constants.Emails.Templates;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendNewsletterCommandHandler : TemplateHandler<SendNewsletterCommand, Unit>
    {
        private readonly ILogger FLogger;

        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateHelper FTemplateHelper;
        
        private readonly IFileUtilityService FFileUtilityService;
        
        private readonly AzureStorageSettings FAzureStorageSettings;
        
        private readonly AppUrls FAppUrls;

        public SendNewsletterCommandHandler(ILogger ALogger, ISmtpClientService ASmtpClientService, 
            ITemplateHelper ATemplateHelper, IFileUtilityService AFileUtilityService, 
            AzureStorageSettings AAzureStorageSettings, AppUrls AAppUrls)
        {
            FLogger = ALogger;
            FSmtpClientService = ASmtpClientService;
            FTemplateHelper = ATemplateHelper;
            FFileUtilityService = AFileUtilityService;
            FAzureStorageSettings = AAzureStorageSettings;
            FAppUrls = AAppUrls;
        }

        public override async Task<Unit> Handle(SendNewsletterCommand ARequest, CancellationToken ACancellationToken) 
        {
            var LUpdateSubscriberBaseLink = FAppUrls.DeploymentOrigin + FAppUrls.UpdateSubscriberPath;
            var LUnsubscribeBaseLink = FAppUrls.DeploymentOrigin + FAppUrls.UnsubscribePath;

            FLogger.LogInfo($"Update subscriber base URL: {LUpdateSubscriberBaseLink}.");
            FLogger.LogInfo($"Unsubscribe base URL: {LUnsubscribeBaseLink}.");
            
            foreach (var LSubscriber in ARequest.SubscriberInfo)
            {
                FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
                FSmtpClientService.Tos = new List<string> { LSubscriber.Email };
                FSmtpClientService.Bccs = null;
                FSmtpClientService.Subject = ARequest.Subject;

                var LUpdateSubscriberLink = LUpdateSubscriberBaseLink + LSubscriber.Id;
                var LUnsubscribeLink = LUnsubscribeBaseLink + LSubscriber.Id;
                var LNewValues = new List<Item>
                {
                    new () { Tag = "{CONTENT}", Value = ARequest.Message },
                    new () { Tag = "{UPDATE_EMAIL_LINK}", Value = LUpdateSubscriberLink },
                    new () { Tag = "{UNSUBSCRIBE_LINK}", Value = LUnsubscribeLink }
                };

                var LUrl = $"{FAzureStorageSettings.BaseUrl}{Templates.NEWSLETTER}";
                FLogger.LogInfo($"Getting newsletter template from URL: {LUrl}.");
                
                var LTemplateFromUrl = await FFileUtilityService.GetFileFromUrl(LUrl, ACancellationToken);
                FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplateFromUrl, LNewValues);

                var LResult = await FSmtpClientService.Send();
                if (!LResult.IsSucceeded) 
                    throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
