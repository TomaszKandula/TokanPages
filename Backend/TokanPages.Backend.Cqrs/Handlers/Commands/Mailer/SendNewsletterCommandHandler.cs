using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Shared.Settings;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{

    public class SendNewsletterCommandHandler : IRequestHandler<SendNewsletterCommand, Unit>
    {

        private readonly ISmtpClientService FSmtpClientService;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly AppUrls FAppUrls;

        public SendNewsletterCommandHandler(ISmtpClientService ASmtpClientService, IAzureStorageService AAzureStorageService, AppUrls AAppUrls) 
        {
            FSmtpClientService = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
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

                FSmtpClientService.HtmlBody = await MakeBody(Constants.Emails.Templates.Newsletter, NewValues);
                await FSmtpClientService.Send();//TODO: error handling

            }

            return await Task.FromResult(Unit.Value);

        }

        private async Task<string> MakeBody(string ATemplate, List<ValueTag> AValueTag)
        {

            var LStorageUrl = $"{FAzureStorageService.GetBaseUrl}{ATemplate}";
            var LTemplate = await GetFileFromUrl(LStorageUrl);

            if (AValueTag == null || !AValueTag.Any()) return null;

            foreach (var AItem in AValueTag)
            {
                LTemplate = LTemplate.Replace(AItem.Tag, AItem.Value);
            }

            return LTemplate;

        }

        private async Task<string> GetFileFromUrl(string Url)
        {
            try
            {
                var LHttpClient = new HttpClient();
                var LResponse = await LHttpClient.GetAsync(Url);
                return await LResponse.Content.ReadAsStringAsync();
            }
            catch
            {
                return Url;
            }
        }

    }

}
