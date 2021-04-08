using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.TemplateHelper.Model;
using Templates = TokanPages.Backend.Shared.Constants.Emails.Templates;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendMessageCommandHandler : TemplateHandler<SendMessageCommand, Unit>
    {
        private readonly ISmtpClientService FSmtpClientService;
        private readonly IAzureStorageService FAzureStorageService;
        private readonly ITemplateHelper FTemplateHelper;
        private readonly IFileUtility FFileUtility;

        public SendMessageCommandHandler(ISmtpClientService ASmtpClientService, 
            IAzureStorageService AAzureStorageService, ITemplateHelper ATemplateHelper, IFileUtility AFileUtility)
        {
            FSmtpClientService = ASmtpClientService;
            FAzureStorageService = AAzureStorageService;
            FTemplateHelper = ATemplateHelper;
            FFileUtility = AFileUtility;
        }

        public override async Task<Unit> Handle(SendMessageCommand ARequest, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { Constants.Emails.Addresses.CONTACT };
            FSmtpClientService.Subject = $"New user message from {ARequest.FirstName}";

            var NewValues = new List<Item>
                {
                    new Item { Tag = "{FIRST_NAME}",    Value = ARequest.FirstName },
                    new Item { Tag = "{LAST_NAME}",     Value = ARequest.LastName },
                    new Item { Tag = "{EMAIL_ADDRESS}", Value = ARequest.UserEmail },
                    new Item { Tag = "{USER_MSG}",      Value = ARequest.Message },
                    new Item { Tag = "{DATE_TIME}",     Value = DateTime.UtcNow.ToString() }
                };

            var LTemplateFromUrl = await FFileUtility.GetFileFromUrl($"{FAzureStorageService.GetBaseUrl}{Templates.CONTACT_FORM}", ACancellationToken);
            FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplateFromUrl, NewValues);

            var LResult = await FSmtpClientService.Send();
            if (!LResult.IsSucceeded)
            {
                throw new BusinessException(nameof(CommonErrorCodes.MAILER_ERROR), LResult.ErrorDesc);
            }

            return await Task.FromResult(Unit.Value);
        }
    }
}
