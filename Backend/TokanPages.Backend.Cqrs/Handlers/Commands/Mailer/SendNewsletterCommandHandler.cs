using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Settings;
using TokanPages.Backend.Core.Exceptions;
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
        private readonly IAzureStorageService FAzureStorageService;
        private readonly ITemplateHelper FTemplateHelper;
        private readonly IFileUtilityService FFileUtilityService;
        private readonly AppUrls FAppUrls;

        public SendNewsletterCommandHandler(ISmtpClientService ASmtpClientService, 
            IAzureStorageService AAzureStorageService, ITemplateHelper ATemplateHelper, AppUrls AAppUrls, IFileUtilityService AFileUtilityService) 
        {
            FSmtpClientService = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
            FTemplateHelper = ATemplateHelper;
            FFileUtilityService = AFileUtilityService;
            FAppUrls = AAppUrls;
        }

        public override async Task<Unit> Handle(SendNewsletterCommand ARequest, CancellationToken ACancellationToken) 
        {
            var UpdateSubscriberBaseLink = FAppUrls.DeploymentOrigin + FAppUrls.UpdateSubscriberPath;
            var UnsubscribeBaseLink = FAppUrls.DeploymentOrigin + FAppUrls.UnsubscribePath;
            foreach (var Subscriber in ARequest.SubscriberInfo)
            {
                FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
                FSmtpClientService.Tos = new List<string> { Subscriber.Email };
                FSmtpClientService.Bccs = null;
                FSmtpClientService.Subject = ARequest.Subject;

                var UpdateSubscriberLink = UpdateSubscriberBaseLink + Subscriber.Id;
                var UnsubscribeLink = UnsubscribeBaseLink + Subscriber.Id;
                var NewValues = new List<Item>
                    {
                        new Item { Tag = "{CONTENT}", Value = ARequest.Message },
                        new Item { Tag = "{UPDATE_EMAIL_LINK}", Value = UpdateSubscriberLink },
                        new Item { Tag = "{UNSUBSCRIBE_LINK}", Value = UnsubscribeLink }
                    };

                var LTemplateFromUrl = await FFileUtilityService.GetFileFromUrl($"{FAzureStorageService.GetBaseUrl}{Templates.NEWSLETTER}", ACancellationToken);
                FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplateFromUrl, NewValues);

                var LResult = await FSmtpClientService.Send();
                if (!LResult.IsSucceeded) 
                {
                    throw new BusinessException(nameof(CommonErrorCodes.MAILER_ERROR), LResult.ErrorDesc);
                }
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
