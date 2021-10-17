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
        private readonly DatabaseContext _databaseContext;

        private readonly IDateTimeService _dateTimeService;

        private readonly ICipheringService _cipheringService;

        private readonly ISmtpClientService _smtpClientService;

        private readonly ILogger _logger;

        private readonly ITemplateService _templateService;

        private readonly ICustomHttpClient _customHttpClient;

        private readonly AzureStorage _azureStorage;

        private readonly ApplicationPaths _applicationPaths;

        private readonly ExpirationSettings _expirationSettings;

        public AddUserCommandHandler(DatabaseContext databaseContext, IDateTimeService dateTimeService,
            ICipheringService cipheringService, ISmtpClientService smtpClientService, ILogger logger,
            ITemplateService templateService, ICustomHttpClient customHttpClient, AzureStorage azureStorage,
            ApplicationPaths applicationPaths, ExpirationSettings expirationSettings)
        {
            _databaseContext = databaseContext;
            _dateTimeService = dateTimeService;
            _cipheringService = cipheringService;
            _smtpClientService = smtpClientService;
            _logger = logger;
            _templateService = templateService;
            _customHttpClient = customHttpClient;
            _azureStorage = azureStorage;
            _applicationPaths = applicationPaths;
            _expirationSettings = expirationSettings;
        }

        public override async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var users = await _databaseContext.Users
                .Where(users => users.EmailAddress == request.EmailAddress)
                .SingleOrDefaultAsync(cancellationToken);

            if (users != null && (users.ActivationIdEnds == null || users.ActivationIdEnds > _dateTimeService.Now))
                throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

            var getNewSalt = _cipheringService.GenerateSalt(Constants.CIPHER_LOG_ROUNDS);
            var getHashedPassword = _cipheringService.GetHashedPassword(request.Password, getNewSalt);

            var activationId = Guid.NewGuid();
            var activationIdEnds = _dateTimeService.Now.AddMinutes(_expirationSettings.ActivationIdExpiresIn);

            if (users != null && (users.ActivationIdEnds != null || users.ActivationIdEnds < _dateTimeService.Now))
            {
                users.CryptedPassword = getHashedPassword;
                users.ActivationId = activationId;
                users.ActivationIdEnds = activationIdEnds;

                await _databaseContext.SaveChangesAsync(cancellationToken);
                await SendNotification(request.EmailAddress, activationId, activationIdEnds, cancellationToken);

                _logger.LogInformation($"Re-registering new user after ActivationId expired, user id: {users.Id}.");
                return await Task.FromResult(users.Id);
            }

            var newUser = new Users
            {
                EmailAddress = request.EmailAddress,
                UserAlias = request.UserAlias.ToLower(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Registered = _dateTimeService.Now,
                AvatarName = Constants.Defaults.AVATAR_NAME,
                CryptedPassword = getHashedPassword,
                ActivationId = activationId,
                ActivationIdEnds = activationIdEnds
            };

            await _databaseContext.Users.AddAsync(newUser, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
            await SetupDefaultPermissions(newUser.Id, cancellationToken);
            await SendNotification(request.EmailAddress, activationId, activationIdEnds, cancellationToken);

            _logger.LogInformation($"Registering new user account, user id: {newUser.Id}.");
            return await Task.FromResult(newUser.Id);
        }

        private async Task SetupDefaultPermissions(Guid userId, CancellationToken cancellationToken)
        {
            var userRoleName = Identity.Authorization.Roles.EverydayUser.ToString();
            var defaultPermissions = await _databaseContext.DefaultPermissions
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

            await _databaseContext.UserRoles.AddAsync(newRole, cancellationToken);
            await _databaseContext.UserPermissions.AddRangeAsync(newPermissions, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);
        }

        private async Task SendNotification(string emailAddress, Guid activationId, DateTime activationIdEnds, CancellationToken cancellationToken)
        {
            _smtpClientService.From = Constants.Emails.Addresses.CONTACT;
            _smtpClientService.Tos = new List<string> { emailAddress };
            _smtpClientService.Subject = "New account registration";

            var activationLink = $"{_applicationPaths.DeploymentOrigin}{_applicationPaths.ActivationPath}{activationId}";

            var newValues = new Dictionary<string, string>
            {
                { "{ACTIVATION_LINK}", activationLink },
                { "{EXPIRATION}", $"{activationIdEnds}" }
            };

            var url = $"{_azureStorage.BaseUrl}{Constants.Emails.Templates.REGISTER_FORM}";
            _logger.LogInformation($"Getting email template from URL: {url}.");

            var configuration = new Configuration { Url = url, Method = "GET" };
            var results = await _customHttpClient.Execute(configuration, cancellationToken);

            if (results.Content == null)
                throw new BusinessException(nameof(ErrorCodes.EMAIL_TEMPLATE_EMPTY), ErrorCodes.EMAIL_TEMPLATE_EMPTY);

            var template = System.Text.Encoding.Default.GetString(results.Content);
            _smtpClientService.HtmlBody = _templateService.MakeBody(template, newValues);

            var result = await _smtpClientService.Send(cancellationToken);
            if (!result.IsSucceeded)
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SEND_EMAIL), $"{ErrorCodes.CANNOT_SEND_EMAIL}. {result.ErrorDesc}");
        }
    }
}