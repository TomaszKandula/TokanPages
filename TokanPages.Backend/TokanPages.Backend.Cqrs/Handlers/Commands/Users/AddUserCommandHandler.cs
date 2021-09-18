namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{   
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using SmtpClient;
    using Core.Logger;
    using Shared.Models;
    using Storage.Models;
    using System.Net.Http;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.CipheringService;
    using System.Collections.Generic;
    using Shared.Services.DateTimeService;
    using Shared.Services.TemplateService;

    public class AddUserCommandHandler : TemplateHandler<AddUserCommand, Guid>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IDateTimeService FDateTimeService;

        private readonly ICipheringService FCipheringService;

        private readonly ISmtpClientService FSmtpClientService;

        private readonly ILogger FLogger;

        private readonly ITemplateService FTemplateService;

        private readonly HttpClient FHttpClient;

        private readonly AzureStorage FAzureStorage;

        private readonly ApplicationPaths FApplicationPaths;

        private readonly ExpirationSettings FExpirationSettings;

        public AddUserCommandHandler(DatabaseContext ADatabaseContext, IDateTimeService ADateTimeService,
            ICipheringService ACipheringService, ISmtpClientService ASmtpClientService, ILogger ALogger,
            ITemplateService ATemplateService, HttpClient AHttpClient, AzureStorage AAzureStorage,
            ApplicationPaths AApplicationPaths, ExpirationSettings AExpirationSettings)
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
            FCipheringService = ACipheringService;
            FSmtpClientService = ASmtpClientService;
            FLogger = ALogger;
            FTemplateService = ATemplateService;
            FHttpClient = AHttpClient;
            FAzureStorage = AAzureStorage;
            FApplicationPaths = AApplicationPaths;
            FExpirationSettings = AExpirationSettings;
        }

        public override async Task<Guid> Handle(AddUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .SingleOrDefaultAsync(ACancellationToken);

            if (LUsers != null && (LUsers.ActivationIdEnds == null || LUsers.ActivationIdEnds > FDateTimeService.Now))
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var LGetNewSalt = FCipheringService.GenerateSalt(Constants.CIPHER_LOG_ROUNDS);
            var LGetHashedPassword = FCipheringService.GetHashedPassword(ARequest.Password, LGetNewSalt);

            var LActivationId = Guid.NewGuid();
            var LActivationIdEnds = FDateTimeService.Now.AddMinutes(FExpirationSettings.ActivationIdExpiresIn);

            if (LUsers != null && (LUsers.ActivationIdEnds != null || LUsers.ActivationIdEnds < FDateTimeService.Now))
            {
                LUsers.LastUpdated = null;
                LUsers.LastLogged = null;
                LUsers.CryptedPassword = LGetHashedPassword;
                LUsers.ResetId = null;
                LUsers.ResetIdEnds = null;
                LUsers.ActivationId = LActivationId;
                LUsers.ActivationIdEnds = LActivationIdEnds;

                await FDatabaseContext.SaveChangesAsync(ACancellationToken);
                await SendNotification(ARequest.EmailAddress, LActivationId, LActivationIdEnds, ACancellationToken);

                FLogger.LogInformation($"Re-registering new user after ActivationId expired, user id: {LUsers.Id}.");
                return await Task.FromResult(LUsers.Id);
            }

            var LNewUser = new Domain.Entities.Users
            {
                EmailAddress = ARequest.EmailAddress,
                IsActivated = false,
                UserAlias = ARequest.UserAlias.ToLower(),
                FirstName = ARequest.FirstName,
                LastName = ARequest.LastName,
                Registered = FDateTimeService.Now,
                LastUpdated = null,
                LastLogged = null,
                AvatarName = Constants.Defaults.AVATAR_NAME,
                CryptedPassword = LGetHashedPassword,
                ResetId = null,
                ResetIdEnds = null,
                ActivationId = LActivationId,
                ActivationIdEnds = LActivationIdEnds
            };

            await FDatabaseContext.Users.AddAsync(LNewUser, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            await SendNotification(ARequest.EmailAddress, LActivationId, LActivationIdEnds, ACancellationToken);

            FLogger.LogInformation($"Registering new user account, user id: {LNewUser.Id}.");
            return await Task.FromResult(LNewUser.Id);
        }

        private async Task SendNotification(string AEmailAddress, Guid AActivationId, DateTime AActivationIdEnds, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { AEmailAddress };
            FSmtpClientService.Subject = "New account registration";

            var LActivationLink = $"{FApplicationPaths.DeploymentOrigin}{FApplicationPaths.ActivationPath}{AActivationId}";

            var LNewValues = new List<TemplateItem>
            {
                new() { Tag = "{ACTIVATION_LINK}", Value = LActivationLink },
                new() { Tag = "{EXPIRATION}", Value = $"{AActivationIdEnds}" }
            };

            var LUrl = $"{FAzureStorage.BaseUrl}{Constants.Emails.Templates.REGISTER_FORM}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LTemplateFromUrl = await FHttpClient.GetAsync(LUrl, ACancellationToken);
            var LTemplate = await LTemplateFromUrl.Content.ReadAsStringAsync(ACancellationToken);
            FSmtpClientService.HtmlBody = FTemplateService.MakeBody(LTemplate, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(
                    nameof(ErrorCodes.CANNOT_SEND_EMAIL), 
                    $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");
        }
    }
}