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
        
        private readonly AzureStorage FAzureStorage;
        
        private readonly ApplicationPaths FApplicationPaths;
        
        public ResetUserPasswordCommandHandler(DatabaseContext ADatabaseContext, ILogger ALogger, HttpClient AHttpClient, 
            ISmtpClientService ASmtpClientService, ITemplateService ATemplateService, AzureStorage AAzureStorage, 
            ApplicationPaths AApplicationPaths)
        {
            FDatabaseContext = ADatabaseContext;
            FLogger = ALogger;
            FHttpClient = AHttpClient;
            FSmtpClientService = ASmtpClientService;
            FTemplateService = ATemplateService;
            FAzureStorage = AAzureStorage;
            FApplicationPaths = AApplicationPaths;
        }

        public override async Task<Unit> Handle(ResetUserPasswordCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);

            if (!LUsers.Any())
                return await Task.FromResult(Unit.Value);

            var LCurrentUser = LUsers.First();
            LCurrentUser.CryptedPassword = string.Empty;
            LCurrentUser.ResetId = Guid.NewGuid();
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { ARequest.EmailAddress };
            FSmtpClientService.Subject = "Reset user password";

            var LResetLink = $"{FApplicationPaths.DeploymentOrigin}{FApplicationPaths.ResetPath}";
            
            var LNewValues = new List<TemplateItem>
            {
                new () { Tag = "{RESET_LINK}", Value = LResetLink }
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