using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Models;
using TokanPages.Backend.Shared.Settings;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.TemplateHelper;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{

    public class SendNewsletterCommandHandler : IRequestHandler<SendNewsletterCommand, Unit>
    {

        private readonly ISmtpClientService FSmtpClientService;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly ITemplateHelper FTemplateHelper;
        private readonly AppUrls FAppUrls;

        public SendNewsletterCommandHandler(ISmtpClientService ASmtpClientService, 
            IAzureStorageService AAzureStorageService, ITemplateHelper ATemplateHelper, AppUrls AAppUrls) 
        {
            FSmtpClientService = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
            FTemplateHelper = ATemplateHelper;
            FAppUrls = AAppUrls;
        }

        public async Task<Unit> Handle(SendNewsletterCommand ARequest, CancellationToken ACancellationToken) 
        {

            var UnsubscribeBaseLink = FAppUrls.DeploymentOrigin + FAppUrls.UnsubscribePath;
            foreach (var Subscriber in ARequest.SubscriberInfo)
            {

                FSmtpClientService.From = Constants.Emails.Addresses.Contact;
                FSmtpClientService.Tos = new List<string> { Subscriber.Email };
                FSmtpClientService.Bccs = null;
                FSmtpClientService.Subject = ARequest.Subject;

                var UnsubscribeLink = UnsubscribeBaseLink + Subscriber.Id;
                var NewValues = new List<ValueTag>
                    {
                        new ValueTag { Tag = "{CONTENT}", Value = ARequest.Message },
                        new ValueTag { Tag = "{UNSUBSCRIBE_LINK}", Value = UnsubscribeLink }
                    };

                FSmtpClientService.HtmlBody = 
                    await FTemplateHelper.MakeBody(Constants.Emails.Templates.Newsletter, NewValues, FAzureStorageService.GetBaseUrl);
                
                var LResult = await FSmtpClientService.Send();
                if (!LResult.IsSucceeded) 
                {
                    throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), LResult.ErrorDesc);
                }

            }

            return await Task.FromResult(Unit.Value);

        }

    }

}
