using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Settings;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.TemplateHelper.Model;
using Templates = TokanPages.Backend.Shared.Constants.Emails.Templates;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendNewsletterCommandHandler : TemplateHandler<SendNewsletterCommand, Unit>
    {
        private readonly ISmtpClientService FSmtpClientService;
        private readonly ITemplateHelper FTemplateHelper;
        private readonly IFileUtilityService FFileUtilityService;
        private readonly AzureStorageSettings FAzureStorageSettings;
        private readonly AppUrls FAppUrls;

        public SendNewsletterCommandHandler(ISmtpClientService ASmtpClientService, ITemplateHelper ATemplateHelper, 
            IFileUtilityService AFileUtilityService, AzureStorageSettings AAzureStorageSettings, AppUrls AAppUrls) 
        {
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
                    new Item { Tag = "{CONTENT}", Value = ARequest.Message },
                    new Item { Tag = "{UPDATE_EMAIL_LINK}", Value = LUpdateSubscriberLink },
                    new Item { Tag = "{UNSUBSCRIBE_LINK}", Value = LUnsubscribeLink }
                };

                var LUrl = $"{FAzureStorageSettings.BaseUrl}{Templates.NEWSLETTER}";
                var LTemplateFromUrl = await FFileUtilityService.GetFileFromUrl(LUrl, ACancellationToken);
                FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplateFromUrl, LNewValues);

                var LResult = await FSmtpClientService.Send();
                if (!LResult.IsSucceeded) 
                    throw new BusinessException(nameof(CommonErrorCodes.MAILER_ERROR), LResult.ErrorDesc);
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
