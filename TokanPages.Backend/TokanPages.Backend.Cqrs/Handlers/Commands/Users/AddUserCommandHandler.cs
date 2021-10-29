namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{   
    using System;
    using System.Net;
    using System.Text;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using Shared.Models;
    using Storage.Models;
    using Core.Exceptions;
    using Domain.Entities;
    using Shared.Resources;
    using Services.CipheringService;
    using System.Collections.Generic;
    using Core.Utilities.LoggerService;
    using Core.Utilities.DateTimeService;
    using Core.Utilities.TemplateService;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class AddUserCommandHandler : TemplateHandler<AddUserCommand, Guid>
    {
        private readonly IDateTimeService _dateTimeService;

        private readonly ICipheringService _cipheringService;

        private readonly ITemplateService _templateService;

        private readonly ICustomHttpClient _customHttpClient;

        private readonly AzureStorage _azureStorage;

        private readonly ApplicationPaths _applicationPaths;

        private readonly ExpirationSettings _expirationSettings;

        private readonly EmailSender _emailSender;

        private Configuration _configuration;

        public AddUserCommandHandler(DatabaseContext databaseContext, IDateTimeService dateTimeService,
            ICipheringService cipheringService, ILoggerService loggerService, ITemplateService templateService, 
            ICustomHttpClient customHttpClient, AzureStorage azureStorage, ApplicationPaths applicationPaths, 
            ExpirationSettings expirationSettings, EmailSender emailSender) : base(databaseContext, loggerService)
        {
            _dateTimeService = dateTimeService;
            _cipheringService = cipheringService;
            _templateService = templateService;
            _customHttpClient = customHttpClient;
            _azureStorage = azureStorage;
            _applicationPaths = applicationPaths;
            _expirationSettings = expirationSettings;
            _emailSender = emailSender;
        }

        public override async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var users = await DatabaseContext.Users
                .Where(users => users.EmailAddress == request.EmailAddress)
                .SingleOrDefaultAsync(cancellationToken);

            if (users != null && (users.ActivationIdEnds == null || users.ActivationIdEnds > _dateTimeService.Now))
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var getNewSalt = _cipheringService.GenerateSalt(Constants.CipherLogRounds);
            var getHashedPassword = _cipheringService.GetHashedPassword(request.Password, getNewSalt);

            var activationId = Guid.NewGuid();
            var activationIdEnds = _dateTimeService.Now.AddMinutes(_expirationSettings.ActivationIdExpiresIn);

            if (users != null && (users.ActivationIdEnds != null || users.ActivationIdEnds < _dateTimeService.Now))
            {
                users.CryptedPassword = getHashedPassword;
                users.ActivationId = activationId;
                users.ActivationIdEnds = activationIdEnds;

                await DatabaseContext.SaveChangesAsync(cancellationToken);
                await SendNotification(request.EmailAddress, activationId, activationIdEnds, cancellationToken);

                LoggerService.LogInformation($"Re-registering new user after ActivationId expired, user id: {users.Id}.");
                return await Task.FromResult(users.Id);
            }

            var newUser = new Users
            {
                EmailAddress = request.EmailAddress,
                UserAlias = request.UserAlias.ToLower(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Registered = _dateTimeService.Now,
                AvatarName = Constants.Defaults.AvatarName,
                CryptedPassword = getHashedPassword,
                ActivationId = activationId,
                ActivationIdEnds = activationIdEnds
            };

            await DatabaseContext.Users.AddAsync(newUser, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
            await SetupDefaultPermissions(newUser.Id, cancellationToken);
            await SendNotification(request.EmailAddress, activationId, activationIdEnds, cancellationToken);

            LoggerService.LogInformation($"Registering new user account, user id: {newUser.Id}.");
            return await Task.FromResult(newUser.Id);
        }

        private async Task SetupDefaultPermissions(Guid userId, CancellationToken cancellationToken)
        {
            var userRoleName = Identity.Authorization.Roles.EverydayUser.ToString();
            var defaultPermissions = await DatabaseContext.DefaultPermissions
                .AsNoTracking()
                .Include(permissions => permissions.Role)
                .Include(permissions => permissions.Permission)
                .Where(permissions => permissions.Role.Name == userRoleName)
                .Select(permissions => new DefaultPermissions
                {
                    Id = permissions.Id,
                    RoleId = permissions.RoleId,
                    Role = permissions.Role,
                    PermissionId = permissions.PermissionId,
                    Permission = permissions.Permission
                })
                .ToListAsync(cancellationToken);

            var everydayUserRoleId = defaultPermissions
                .Select(permissions => permissions.RoleId)
                .First(); 
            
            var userPermissions = defaultPermissions
                .Select(permissions => permissions.PermissionId)
                .ToList();

            var newRole = new UserRoles
            {
                UserId = userId,
                RoleId = everydayUserRoleId
            };

            var newPermissions = userPermissions.Select(item => new UserPermissions
            {
                UserId = userId, 
                PermissionId = item
            }).ToList();

            await DatabaseContext.UserRoles.AddAsync(newRole, cancellationToken);
            await DatabaseContext.UserPermissions.AddRangeAsync(newPermissions, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        private async Task SendNotification(string emailAddress, Guid activationId, DateTime activationIdEnds, CancellationToken cancellationToken)
        {
            var activationLink = $"{_applicationPaths.DeploymentOrigin}{_applicationPaths.ActivationPath}{activationId}";
            var newValues = new Dictionary<string, string>
            {
                { "{ACTIVATION_LINK}", activationLink },
                { "{EXPIRATION}", $"{activationIdEnds}" }
            };

            var url = $"{_azureStorage.BaseUrl}{Constants.Emails.Templates.RegisterForm}";
            LoggerService.LogInformation($"Getting email template from URL: {url}.");

            _configuration = new Configuration { Url = url, Method = "GET" };
            var getTemplate = await _customHttpClient.Execute(_configuration, cancellationToken);

            if (getTemplate.Content == null)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

            var template = Encoding.Default.GetString(getTemplate.Content);
            var payload = new EmailSenderPayload
            {
                PrivateKey = _emailSender.PrivateKey,
                From = Constants.Emails.Addresses.Contact,
                To = new List<string> { emailAddress },
                Subject = "New account registration",
                Body = _templateService.MakeBody(template, newValues)
            };

            _configuration = new Configuration { Url = _emailSender.BaseUrl, Method = "POST", StringContent = 
                new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(payload), Encoding.Default, "application/json") };

            var sendEmail = await _customHttpClient.Execute(_configuration, cancellationToken);
            if (sendEmail.StatusCode != HttpStatusCode.OK)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}");
        }
    }
}