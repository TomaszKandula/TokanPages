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
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Services.CipheringService;
    using System.Collections.Generic;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class AddUserCommandHandler : TemplateHandler<AddUserCommand, Guid>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IDateTimeService FDateTimeService;

        private readonly ICipheringService FCipheringService;

        private readonly ISmtpClientService FSmtpClientService;

        private readonly ILogger FLogger;

        private readonly ITemplateService FTemplateService;

        private readonly ICustomHttpClient FCustomHttpClient;

        private readonly AzureStorage FAzureStorage;

        private readonly ApplicationPaths FApplicationPaths;

        private readonly ExpirationSettings FExpirationSettings;

        public AddUserCommandHandler(DatabaseContext ADatabaseContext, IDateTimeService ADateTimeService,
            ICipheringService ACipheringService, ISmtpClientService ASmtpClientService, ILogger ALogger,
            ITemplateService ATemplateService, ICustomHttpClient ACustomHttpClient, AzureStorage AAzureStorage,
            ApplicationPaths AApplicationPaths, ExpirationSettings AExpirationSettings)
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
            FCipheringService = ACipheringService;
            FSmtpClientService = ASmtpClientService;
            FLogger = ALogger;
            FTemplateService = ATemplateService;
            FCustomHttpClient = ACustomHttpClient;
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
                LUsers.CryptedPassword = LGetHashedPassword;
                LUsers.ActivationId = LActivationId;
                LUsers.ActivationIdEnds = LActivationIdEnds;

                await FDatabaseContext.SaveChangesAsync(ACancellationToken);
                await SendNotification(ARequest.EmailAddress, LActivationId, LActivationIdEnds, ACancellationToken);

                FLogger.LogInformation($"Re-registering new user after ActivationId expired, user id: {LUsers.Id}.");
                return await Task.FromResult(LUsers.Id);
            }

            var LNewUser = new Users
            {
                EmailAddress = ARequest.EmailAddress,
                UserAlias = ARequest.UserAlias.ToLower(),
                FirstName = ARequest.FirstName,
                LastName = ARequest.LastName,
                Registered = FDateTimeService.Now,
                AvatarName = Constants.Defaults.AVATAR_NAME,
                CryptedPassword = LGetHashedPassword,
                ActivationId = LActivationId,
                ActivationIdEnds = LActivationIdEnds
            };

            await FDatabaseContext.Users.AddAsync(LNewUser, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            await SetupDefaultPermissions(LNewUser.Id, ACancellationToken);
            await SendNotification(ARequest.EmailAddress, LActivationId, LActivationIdEnds, ACancellationToken);

            FLogger.LogInformation($"Registering new user account, user id: {LNewUser.Id}.");
            return await Task.FromResult(LNewUser.Id);
        }

        private async Task SetupDefaultPermissions(Guid AUserId, CancellationToken ACancellationToken)
        {
            var LUserRoleName = Identity.Authorization.Roles.EverydayUser.ToString();
            var LDefaultPermissions = await FDatabaseContext.DefaultPermissions
                .AsNoTracking()
                .Include(ADefaultPermissions => ADefaultPermissions.Role)
                .Include(ADefaultPermissions => ADefaultPermissions.Permission)
                .Where(ADefaultPermissions => ADefaultPermissions.Role.Name == LUserRoleName)
                .Select(AFields => new DefaultPermissions
                {
                    Id = AFields.Id,
                    RoleId = AFields.RoleId,
                    Role = AFields.Role,
                    PermissionId = AFields.PermissionId,
                    Permission = AFields.Permission
                })
                .ToListAsync(ACancellationToken);

            var LEverydayUserRoleId = LDefaultPermissions
                .Select(ADefaultPermissions => ADefaultPermissions.RoleId)
                .First(); 
            
            var LUserPermissions = LDefaultPermissions
                .Select(ADefaultPermissions => ADefaultPermissions.PermissionId)
                .ToList();

            var LNewRole = new UserRoles
            {
                UserId = AUserId,
                RoleId = LEverydayUserRoleId
            };

            var LNewPermissions = LUserPermissions.Select(AItem => new UserPermissions
            {
                UserId = AUserId, 
                PermissionId = AItem
            }).ToList();

            await FDatabaseContext.UserRoles.AddAsync(LNewRole, ACancellationToken);
            await FDatabaseContext.UserPermissions.AddRangeAsync(LNewPermissions, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
        }

        private async Task SendNotification(string AEmailAddress, Guid AActivationId, DateTime AActivationIdEnds, CancellationToken ACancellationToken)
        {
            FSmtpClientService.From = Constants.Emails.Addresses.CONTACT;
            FSmtpClientService.Tos = new List<string> { AEmailAddress };
            FSmtpClientService.Subject = "New account registration";

            var LActivationLink = $"{FApplicationPaths.DeploymentOrigin}{FApplicationPaths.ActivationPath}{AActivationId}";

            var LNewValues = new Dictionary<string, string>
            {
                { "{ACTIVATION_LINK}", LActivationLink },
                { "{EXPIRATION}", $"{AActivationIdEnds}" }
            };

            var LUrl = $"{FAzureStorage.BaseUrl}{Constants.Emails.Templates.REGISTER_FORM}";
            FLogger.LogInformation($"Getting email template from URL: {LUrl}.");

            var LConfiguration = new Configuration { Url = LUrl, Method = "GET" };
            var LResults = await FCustomHttpClient.Execute(LConfiguration, ACancellationToken);

            if (LResults.Content == null)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

            var LTemplate = System.Text.Encoding.Default.GetString(LResults.Content);
            FSmtpClientService.HtmlBody = FTemplateService.MakeBody(LTemplate, LNewValues);

            var LResult = await FSmtpClientService.Send(ACancellationToken);
            if (!LResult.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {LResult.ErrorDesc}");
        }
    }
}