﻿using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using TokanPages.Backend.Shared;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Storage.Settings;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.DateTimeService;
using TokanPages.Backend.Core.Services.TemplateHelper.Model;
using Templates = TokanPages.Backend.Shared.Constants.Emails.Templates;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendMessageCommandHandler : TemplateHandler<SendMessageCommand, Unit>
    {
        private readonly ISmtpClientService FSmtpClientService;
        private readonly ITemplateHelper FTemplateHelper;
        private readonly IFileUtilityService FFileUtilityService;
        private readonly IDateTimeService FDateTimeService;
        private readonly AzureStorageSettings FAzureStorageSettings;
        
        public SendMessageCommandHandler(ISmtpClientService ASmtpClientService, ITemplateHelper ATemplateHelper, 
            IFileUtilityService AFileUtilityService, IDateTimeService ADateTimeService, AzureStorageSettings AAzureStorageSettings)
        {
            FSmtpClientService = ASmtpClientService;
            FTemplateHelper = ATemplateHelper;
            FFileUtilityService = AFileUtilityService;
            FDateTimeService = ADateTimeService;
            FAzureStorageSettings = AAzureStorageSettings;
        }

        public override async Task<Unit> Handle(SendMessageCommand ARequest, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { Constants.Emails.Addresses.CONTACT };
            FSmtpClientService.Subject = $"New user message from {ARequest.FirstName}";

            var LNewValues = new List<Item>
            {
                new Item { Tag = "{FIRST_NAME}", Value = ARequest.FirstName },
                new Item { Tag = "{LAST_NAME}", Value = ARequest.LastName },
                new Item { Tag = "{EMAIL_ADDRESS}", Value = ARequest.UserEmail },
                new Item { Tag = "{USER_MSG}", Value = ARequest.Message },
                new Item { Tag = "{DATE_TIME}", Value = FDateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
            };

            var LUrl = $"{FAzureStorageSettings.BaseUrl}{Templates.CONTACT_FORM}";
            var LTemplateFromUrl = await FFileUtilityService.GetFileFromUrl(LUrl, ACancellationToken);
            FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplateFromUrl, LNewValues);

            var LResult = await FSmtpClientService.Send();
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(CommonErrorCodes.MAILER_ERROR), LResult.ErrorDesc);

            return await Task.FromResult(Unit.Value);
        }
    }
}
