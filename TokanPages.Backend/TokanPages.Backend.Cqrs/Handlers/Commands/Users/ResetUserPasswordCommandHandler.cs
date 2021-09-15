namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Shared.Services.TemplateService;
    using Shared.Services.DateTimeService;
    using Shared.Resources;
    using Core.Exceptions;
    using Storage.Models;
    using Shared.Models;
    using Core.Logger;
    using SmtpClient;
    using Database;
    using MediatR;
    using Shared;
    
    public class ResetUserPasswordCommandHandler : TemplateHandler<ResetUserPasswordCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly ILogger FLogger;

        private readonly HttpClient FHttpClient;
        
        private readonly ISmtpClientService FSmtpClientService;
        
        private readonly ITemplateService FTemplateService;
        
        private readonly IDateTimeService FDateTimeService; 

        private readonly AzureStorage FAzureStorage;
        
        private readonly ApplicationPaths FApplicationPaths;

        private readonly ExpirationSettings FExpirationSettings;
        
        public ResetUserPasswordCommandHandler(DatabaseContext ADatabaseContext, ILogger ALogger, HttpClient AHttpClient, 
            ISmtpClientService ASmtpClientService, ITemplateService ATemplateService, IDateTimeService ADateTimeService, 
            AzureStorage AAzureStorage, ApplicationPaths AApplicationPaths, ExpirationSettings AExpirationSettings)
        {
            FDatabaseContext = ADatabaseContext;
            FLogger = ALogger;
            FHttpClient = AHttpClient;
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
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { ARequest.EmailAddress };
            FSmtpClientService.Subject = "Reset user password";

            var LResetLink = $"{FApplicationPaths.DeploymentOrigin}{FApplicationPaths.UpdatePasswordPath}{LResetId}";
            
            var LNewValues = new List<TemplateItem>
            {
                new () { Tag = "{RESET_LINK}", Value = LResetLink },
                new () { Tag = "{EXPIRATION}", Value = $"{LExpirationDate}" }
            };

            var LUrl = $"{FAzureStorage.BaseUrl}{Constants.Emails.Templates.RESET_PASSWORD}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LTemplateFromUrl = await FHttpClient.GetAsync(LUrl, ACancellationToken);
            var LTemplate = await LTemplateFromUrl.Content.ReadAsStringAsync(ACancellationToken);
            FSmtpClientService.HtmlBody = FTemplateService.MakeBody(LTemplate, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");

            return await Task.FromResult(Unit.Value);
        }
    }
}