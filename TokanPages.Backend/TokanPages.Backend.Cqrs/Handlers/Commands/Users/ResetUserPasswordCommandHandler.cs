namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;
    using MediatR;

    public class ResetUserPasswordCommandHandler : TemplateHandler<ResetUserPasswordCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly ILogger FLogger;

        private readonly ICustomHttpClient FCustomHttpClient;

        private readonly ISmtpClientService FSmtpClientService;

        private readonly ITemplateService FTemplateService;

        private readonly IDateTimeService FDateTimeService; 

        private readonly AzureStorage FAzureStorage;

        private readonly ApplicationPaths FApplicationPaths;

        private readonly ExpirationSettings FExpirationSettings;

        public ResetUserPasswordCommandHandler(DatabaseContext ADatabaseContext, ILogger ALogger, ICustomHttpClient ACustomHttpClient, 
            ISmtpClientService ASmtpClientService, ITemplateService ATemplateService, IDateTimeService ADateTimeService, 
            AzureStorage AAzureStorage, ApplicationPaths AApplicationPaths, ExpirationSettings AExpirationSettings)
        {
            FDatabaseContext = ADatabaseContext;
            FLogger = ALogger;
            FCustomHttpClient = ACustomHttpClient;
            FSmtpClientService = ASmtpClientService;
            FTemplateService = ATemplateService;
            FDateTimeService = ADateTimeService;
            FAzureStorage = AAzureStorage;
            FApplicationPaths = AApplicationPaths;
            FExpirationSettings = AExpirationSettings;
        }

        public override async Task<Unit> Handle(ResetUserPasswordCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);

            if (!LUsers.Any())
                throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            var LCurrentUser = LUsers.First();
            var LResetId = Guid.NewGuid();
            var LExpirationDate = FDateTimeService.Now.AddMinutes(FExpirationSettings.ResetIdExpiresIn);
            LCurrentUser.CryptedPassword = string.Empty;
            LCurrentUser.ResetId = LResetId;
            LCurrentUser.ResetIdEnds = LExpirationDate;
            LCurrentUser.LastUpdated = FDateTimeService.Now;
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            await SendNotification(ARequest.EmailAddress, LResetId, LExpirationDate, ACancellationToken);

            return await Task.FromResult(Unit.Value);
        }

        private async Task SendNotification(string AEmailAddress, Guid AResetId, DateTime AExpirationDate, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { AEmailAddress };
            FSmtpClientService.Subject = "Reset user password";

            var LResetLink = $"{FApplicationPaths.DeploymentOrigin}{FApplicationPaths.UpdatePasswordPath}{AResetId}";
            
            var LNewValues = new Dictionary<string, string>
            {
                { "{RESET_LINK}", LResetLink },
                { "{EXPIRATION}", $"{AExpirationDate}" }
            };

            var LUrl = $"{FAzureStorage.BaseUrl}{Constants.Emails.Templates.RESET_PASSWORD}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LConfiguration = new Configuration { Url = LUrl, Method = "GET" };
            var LResults = await FCustomHttpClient.Execute(LConfiguration, ACancellationToken);

            if (LResults.Content == null)
                throw new BusinessException(ErrorCodes.TEMPLATE_CONTENT_EMPTY, ErrorCodes.TEMPLATE_CONTENT_EMPTY);

            var LTemplate = System.Text.Encoding.Default.GetString(LResults.Content);
            FSmtpClientService.HtmlBody = FTemplateService.MakeBody(LTemplate, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");
        }
    }
}