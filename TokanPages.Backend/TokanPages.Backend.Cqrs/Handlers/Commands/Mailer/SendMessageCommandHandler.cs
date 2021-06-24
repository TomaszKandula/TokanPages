﻿using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;
using TokanPages.Backend.Shared;
using TokanPages.Backend.SmtpClient;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Services.AppLogger;
using TokanPages.Backend.Core.Services.FileUtility;
using TokanPages.Backend.Core.Services.TemplateHelper;
using TokanPages.Backend.Core.Services.DateTimeService;
using Templates = TokanPages.Backend.Shared.Constants.Emails.Templates;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Mailer
{
    public class SendMessageCommandHandler : TemplateHandler<SendMessageCommand, Unit>
    {
        private readonly ILogger FLogger;

        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateHelper FTemplateHelper;
        
        private readonly IFileUtilityService FFileUtilityService;
        
        private readonly IDateTimeService FDateTimeService;
        
        private readonly AzureStorageSettingsModel FAzureStorageSettingsModel;
        
        public SendMessageCommandHandler(ISmtpClientService ASmtpClientService, ITemplateHelper ATemplateHelper, 
            IFileUtilityService AFileUtilityService, IDateTimeService ADateTimeService, 
            AzureStorageSettingsModel AZureStorageSettingsModel, ILogger ALogger)
        {
            FSmtpClientService = ASmtpClientService;
            FTemplateHelper = ATemplateHelper;
            FFileUtilityService = AFileUtilityService;
            FDateTimeService = ADateTimeService;
            FAzureStorageSettingsModel = AZureStorageSettingsModel;
            FLogger = ALogger;
        }

        public override async Task<Unit> Handle(SendMessageCommand ARequest, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { Constants.Emails.Addresses.CONTACT };
            FSmtpClientService.Subject = $"New user message from {ARequest.FirstName}";

            var LNewValues = new List<TemplateItemModel>
            {
                new () { Tag = "{FIRST_NAME}", Value = ARequest.FirstName },
                new () { Tag = "{LAST_NAME}", Value = ARequest.LastName },
                new () { Tag = "{EMAIL_ADDRESS}", Value = ARequest.UserEmail },
                new () { Tag = "{USER_MSG}", Value = ARequest.Message },
                new () { Tag = "{DATE_TIME}", Value = FDateTimeService.Now.ToString(CultureInfo.InvariantCulture) }
            };

            var LUrl = $"{FAzureStorageSettingsModel.BaseUrl}{Templates.CONTACT_FORM}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LTemplateFromUrl = await FFileUtilityService.GetFileFromUrl(LUrl, ACancellationToken);
            FSmtpClientService.HtmlBody = FTemplateHelper.MakeBody(LTemplateFromUrl, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");

            return await Task.FromResult(Unit.Value);
        }
    }
}
