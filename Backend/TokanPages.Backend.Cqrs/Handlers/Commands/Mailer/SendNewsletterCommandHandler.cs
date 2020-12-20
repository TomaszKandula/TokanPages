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
        private readonly IFileUtility FFileUtility;
        private readonly AppUrls FAppUrls;

        public SendNewsletterCommandHandler(ISmtpClientService ASmtpClientService, 
            IAzureStorageService AAzureStorageService, ITemplateHelper ATemplateHelper, AppUrls AAppUrls, IFileUtility AFileUtility) 
        {
            FSmtpClientService = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
            FTemplateHelper = ATemplateHelper;
            FFileUtility = AFileUtility;
            FAppUrls = AAppUrls;
        }

        public override async Task<Unit> Handle(SendNewsletterCommand ARequest, CancellationToken ACancellationToken) 
        {

            var UnsubscribeBaseLink = FAppUrls.DeploymentOrigin + FAppUrls.UnsubscribePath;
            foreach (var Subscriber in ARequest.SubscriberInfo)
            {

                FSmtpClientService.From = Constants.Emails.Addresses.Contact;
                FSmtpClientService.Tos = new List<string> { Subscriber.Email };
                FSmtpClientService.Bccs = null;
                FSmtpClientService.Subject = ARequest.Subject;

                var UnsubscribeLink = UnsubscribeBaseLink + Subscriber.Id;
                var NewValues = new List<Item>
                    {
                        new Item { Tag = "{CONTENT}", Value = ARequest.Message },
                        new Item { Tag = "{UNSUBSCRIBE_LINK}", Value = UnsubscribeLink }
                    };

                var LTemplateFromUrl = await FFileUtility.GetFileFromUrl($"{FAzureStorageService.GetBaseUrl}{Templates.Newsletter}", ACancellationToken);
                FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplateFromUrl, NewValues);

                var LResult = await FSmtpClientService.Send();
                if (!LResult.IsSucceeded) 
                {
                    throw new BusinessException(nameof(CommonErrorCodes.ERROR_MAILER), LResult.ErrorDesc);
                }

            }

            return await Task.FromResult(Unit.Value);

        }

    }

}
