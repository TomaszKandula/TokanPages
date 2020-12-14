using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Shared.Models;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Unit>
    {

        private readonly ISmtpClientService FSmtpClientService;
        private readonly IAzureStorageService FAzureStorageService;

        public SendMessageCommandHandler(ISmtpClientService ASmtpClientService, IAzureStorageService AAzureStorageService)
        {
            FSmtpClientService = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
        }

        public async Task<Unit> Handle(SendMessageCommand ARequest, CancellationToken ACancellationToken)
        {

            FSmtpClientService.From = Constants.Emails.Addresses.Contact;
            FSmtpClientService.Tos = new List<string> { Constants.Emails.Addresses.Contact };
            FSmtpClientService.Subject = $"New user message from {ARequest.FirstName}";

            var NewValues = new List<ValueTag>
                {
                    new ValueTag { Tag = "{FIRST_NAME}",    Value = ARequest.FirstName },
                    new ValueTag { Tag = "{LAST_NAME}",     Value = ARequest.LastName },
                    new ValueTag { Tag = "{EMAIL_ADDRESS}", Value = ARequest.UserEmail },
                    new ValueTag { Tag = "{USER_MSG}",      Value = ARequest.Message },
                    new ValueTag { Tag = "{DATE_TIME}",     Value = DateTime.UtcNow.ToString() }
                };

            FSmtpClientService.HtmlBody = await MakeBody(Constants.Emails.Templates.ContactForm, NewValues);
            await FSmtpClientService.Send();//TODO: add error handling

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
