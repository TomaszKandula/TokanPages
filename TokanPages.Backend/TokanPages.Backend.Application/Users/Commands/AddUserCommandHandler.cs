using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Application.Users.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserCommandHandler : RequestHandler<AddUserCommand, Guid>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly ICipheringService _cipheringService;

    private readonly IEmailSenderService _emailSenderService;

    private readonly IConfiguration _configuration;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IUserService _userService;

    public AddUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IDateTimeService dateTimeService,
        ICipheringService cipheringService, IEmailSenderService emailSenderService, IConfiguration configuration, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IUserService userService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _cipheringService = cipheringService;
        _emailSenderService = emailSenderService;
        _configuration = configuration;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
    }

    public override async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var adminUser = await _userService.GetUser(cancellationToken);
        var users = await DatabaseContext.Users
            .Where(users => !users.IsDeleted)
            .Where(users => users.EmailAddress == request.EmailAddress)
            .SingleOrDefaultAsync(cancellationToken);

        if (users != null && (users.ActivationIdEnds == null || users.ActivationIdEnds > _dateTimeService.Now))
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var getNewSalt = _cipheringService.GenerateSalt(12);
        var getHashedPassword = _cipheringService.GetHashedPassword(request.Password, getNewSalt);
        LoggerService.LogInformation($"New hashed password has been generated. Requested by: {request.EmailAddress}.");

        var expiresIn = _configuration.GetValue<int>("Limit_Activation_Maturity");
        var activationId = Guid.NewGuid();
        var activationIdEnds = _dateTimeService.Now.AddMinutes(expiresIn);

        if (users != null && (users.ActivationIdEnds != null || users.ActivationIdEnds < _dateTimeService.Now))
        {
            users.CryptedPassword = getHashedPassword;
            users.ActivationId = activationId;
            users.ActivationIdEnds = activationIdEnds;

            var id = await PrepareNotificationUncommitted(cancellationToken);
            await CommitAllChanges(cancellationToken);

            await SendNotification(id, request.EmailAddress, activationId, activationIdEnds, cancellationToken);
            LoggerService.LogInformation($"Re-registering new user after ActivationId expired, user id: {users.Id}.");
            return users.Id;
        }

        var newUserId = Guid.NewGuid();
        var defaultAvatarName = await UploadDefaultAvatar(newUserId, cancellationToken);

        var userAlias = newUserId.ToString()[..8];
        if (request is not { FirstName: "", LastName: "" })
            userAlias = $"{request.FirstName[..2]}{request.LastName[..3]}".ToLower();

        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var expirationDate = baseDateTime.AddMinutes(expiresIn);
        var input = new UserDataInput { UserId = newUserId, Command = request };

        var messageId = await PrepareNotificationUncommitted(cancellationToken);
        await CreateUserUncommitted(input, adminUser, userAlias, getHashedPassword, activationId, activationIdEnds, cancellationToken);
        await CreateUserInfoUncommitted(input, defaultAvatarName, adminUser, cancellationToken);
        await SetupDefaultPermissionsUncommitted(newUserId, adminUser?.UserId, cancellationToken);
        await CommitAllChanges(cancellationToken);

        await SendNotification(messageId, request.EmailAddress, activationId, expirationDate, cancellationToken);

        var info = adminUser is not null ? $"Admin (ID: {adminUser.UserId})" : $"System (ID: {Guid.Empty})";
        LoggerService.LogInformation($"Registering new user account, user id: {newUserId}.");
        LoggerService.LogInformation($"Registered by the {info}");

        return newUserId;
    }

    private async Task CommitAllChanges(CancellationToken cancellationToken = default) 
        => await DatabaseContext.SaveChangesAsync(cancellationToken);

    private async Task<string?> UploadDefaultAvatar(Guid newUserId, CancellationToken cancellationToken = default)
    {
        const string defaultAvatarPath = "content/assets/images/avatars/";
        const string defaultAvatarName = "avatar-default-288.jpeg";
        const string sourceAvatarPath = $"{defaultAvatarPath}{defaultAvatarName}";

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var destinationAvatarPath = $"content/users/{newUserId}/{defaultAvatarName}";

        var defaultAvatar = await azureBlob.OpenRead(sourceAvatarPath, cancellationToken);
        if (defaultAvatar is not null)
            await azureBlob.UploadFile(defaultAvatar.Content!, destinationAvatarPath, cancellationToken: cancellationToken);

        return defaultAvatar != null ? defaultAvatarName : null;
    }

    private async Task CreateUserUncommitted(
        UserDataInput input, 
        GetUserOutput? admin,
        string alias,
        string password,
        Guid? activationId,
        DateTime? activationIdEnds,
        CancellationToken cancellationToken = default)
    {
        var newUser = new Domain.Entities.User.Users
        {
            Id = input.UserId,
            EmailAddress = input.Command.EmailAddress,
            UserAlias = alias,
            CryptedPassword = password,
            ActivationId = activationId,
            ActivationIdEnds = activationIdEnds,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = admin?.UserId ?? Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await DatabaseContext.Users.AddAsync(newUser, cancellationToken);
    }

    private async Task CreateUserInfoUncommitted(
        UserDataInput input, 
        string? avatar,
        GetUserOutput? admin,
        CancellationToken cancellationToken = default)
    {
        var newUserInfo = new UserInfo
        {
            UserId = input.UserId,
            FirstName = input.Command.FirstName,
            LastName = input.Command.LastName,
            UserImageName = avatar,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = admin?.UserId ?? Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await DatabaseContext.UserInfo.AddAsync(newUserInfo, cancellationToken);
    }

    private async Task SetupDefaultPermissionsUncommitted(Guid userId, Guid? adminUserId, CancellationToken cancellationToken)
    {
        var userRoleName = Domain.Enums.Roles.EverydayUser.ToString();
        var defaultPermissions = await DatabaseContext.DefaultPermissions
            .AsNoTracking()
            .Include(permissions => permissions.Roles)
            .Include(permissions => permissions.Permissions)
            .Where(permissions => permissions.Roles.Name == userRoleName)
            .Select(permissions => new DefaultPermissions
            {
                Id = permissions.Id,
                RoleId = permissions.RoleId,
                Roles = permissions.Roles,
                PermissionId = permissions.PermissionId,
                Permissions = permissions.Permissions
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
            RoleId = everydayUserRoleId,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = adminUserId ?? Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        var newPermissions = userPermissions.Select(item => new UserPermissions
        {
            UserId = userId, 
            PermissionId = item,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = adminUserId ?? Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        }).ToList();

        await DatabaseContext.UserRoles.AddAsync(newRole, cancellationToken);
        await DatabaseContext.UserPermissions.AddRangeAsync(newPermissions, cancellationToken);
    }

    private async Task<Guid> PrepareNotificationUncommitted(CancellationToken cancellationToken)
    {
        var messageId = Guid.NewGuid();
        var serviceBusMessage = new ServiceBusMessage
        {
            Id = messageId,
            IsConsumed = false
        };

        await DatabaseContext.ServiceBusMessages.AddAsync(serviceBusMessage, cancellationToken);
        return messageId;
    }

    private async Task SendNotification(Guid messageId, string emailAddress, Guid activationId, DateTime activationIdEnds, CancellationToken cancellationToken)
    {
        var configuration = new CreateUserConfiguration
        {
            MessageId = messageId,
            EmailAddress = emailAddress,
            ActivationId = activationId,
            ActivationIdEnds = activationIdEnds
        };

        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);
    }
}