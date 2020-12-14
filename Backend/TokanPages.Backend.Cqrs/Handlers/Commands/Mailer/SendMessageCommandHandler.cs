using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Models;
using TokanPages.Backend.Core.TemplateHelper;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{

    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Unit>
    {

        private readonly ISmtpClientService FSmtpClientService;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly ITemplateHelper FTemplateHelper;

        public SendMessageCommandHandler(ISmtpClientService ASmtpClientService, 
            IAzureStorageService AAzureStorageService, ITemplateHelper ATemplateHelper)
        {
            FSmtpClientService = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
            FTemplateHelper = ATemplateHelper;
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

            FSmtpClientService.HtmlBody = 
                await FTemplateHelper.MakeBody(Constants.Emails.Templates.ContactForm, NewValues, FAzureStorageService.GetBaseUrl);
            await FSmtpClientService.Send();//TODO: add error handling

            return await Task.FromResult(Unit.Value);

        }

    }

}
