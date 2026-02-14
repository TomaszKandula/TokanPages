using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Messaging;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Persistence.DataAccess.Repositories.User.Models;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.EmailSenderService.Abstractions;
using TokanPages.Services.EmailSenderService.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserCommandHandler : RequestHandler<AddUserCommand, Guid>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly ICipheringService _cipheringService;

    private readonly IEmailSenderService _emailSenderService;

    private readonly AppSettingsModel _appSettings;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IUserRepository _userRepository;

    private readonly IMessagingRepository _messagingRepository;

    private readonly IUserService _userService;

    public AddUserCommandHandler(ILoggerService loggerService, IDateTimeService dateTimeService, ICipheringService cipheringService, 
        IEmailSenderService emailSenderService, IOptions<AppSettingsModel> options, IAzureBlobStorageFactory azureBlobStorageFactory, 
        IUserService userService, IUserRepository userRepository, IMessagingRepository messagingRepository) : base(loggerService)
    {
        _dateTimeService = dateTimeService;
        _cipheringService = cipheringService;
        _emailSenderService = emailSenderService;
        _appSettings = options.Value;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
        _userRepository = userRepository;
        _messagingRepository = messagingRepository;
    }

    public override async Task<Guid> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var userDetails = await _userRepository.GetUserDetails(request.EmailAddress);
        if (userDetails != null && (userDetails.ActivationIdEnds == null || userDetails.ActivationIdEnds > _dateTimeService.Now))
            throw new BusinessException(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS), ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS);

        var getNewSalt = _cipheringService.GenerateSalt(12);
        var getHashedPassword = _cipheringService.GetHashedPassword(request.Password, getNewSalt);
        LoggerService.LogInformation($"New hashed password has been generated. Requested by: {request.EmailAddress}.");

        var messageId = Guid.NewGuid();
        var expiresIn = _appSettings.LimitActivationMaturity;
        var activationId = Guid.NewGuid();
        var activationIdEnds = _dateTimeService.Now.AddMinutes(expiresIn);

        if (userDetails != null && (userDetails.ActivationIdEnds != null || userDetails.ActivationIdEnds < _dateTimeService.Now))
        {
            var update = new UpdateSignupDetailsDto
            {
                UserId = userDetails.UserId,
                CryptedPassword = getHashedPassword,
                ActivationId = activationId,
                ActivationIdEnds = activationIdEnds
            };

            await _userRepository.UpdateSignupDetails(update);
            await _messagingRepository.CreateServiceBusMessage(messageId);
            await SendNotification(messageId, request.LanguageId, request.EmailAddress, activationId, activationIdEnds, cancellationToken);

            LoggerService.LogInformation($"Re-registering new user after ActivationId expired, user id: {userDetails.UserId}.");
            return userDetails.UserId;
        }

        var newUserId = Guid.NewGuid();
        var defaultAvatarName = await UploadDefaultAvatar(newUserId, cancellationToken) ?? string.Empty;

        var userAlias = newUserId.ToString()[..8];
        if (request is not { FirstName: "", LastName: "" })
            userAlias = $"{request.FirstName[..2]}{request.LastName[..3]}".ToLower();

        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var expirationDate = baseDateTime.AddMinutes(expiresIn);

        var userData = new CreateUserDto
        {
            UserId = newUserId,
            UserAlias = userAlias,
            EmailAddress = request.EmailAddress,
            CryptedPassword = getHashedPassword,
            ActivationId = activationId,
            ActivationIdEnds = activationIdEnds
        };

        await _userRepository.CreateUser(userData);
        await _userRepository.CreateUserInformation(newUserId, request.FirstName, request.LastName, defaultAvatarName);
        await CreateUserRoleAndPermissions(newUserId);

        await _messagingRepository.CreateServiceBusMessage(messageId);
        await SendNotification(messageId, request.LanguageId, request.EmailAddress, activationId, expirationDate, cancellationToken);

        LoggerService.LogInformation($"Registering new user account, user id: {newUserId}.");
        return newUserId;
    }

    private async Task CreateUserRoleAndPermissions(Guid userId)
    {
        const string userRoleName = nameof(Domain.Enums.Role.EverydayUser);
        var defaultPermissions = await _userRepository.GetDefaultPermissions(userRoleName);

        var userRoleId = defaultPermissions
            .Select(permissions => permissions.RoleId)
            .First(); 

        var userPermissionIds = defaultPermissions
            .Select(permissions => permissions.PermissionId)
            .ToList();

        var newRole = new CreateUserRoleDto
        {
            UserId = userId,
            RoleId = userRoleId,
        };

        var newPermissions = userPermissionIds
            .Select(item => new CreateUserPermissionDto
            {
                UserId = userId,
                PermissionId = item
            })
            .ToList();

        await _userRepository.CreateUserRole(newRole);
        await _userRepository.CreateUserPermissions(newPermissions);
    }

    private async Task<string?> UploadDefaultAvatar(Guid newUserId, CancellationToken cancellationToken = default)
    {
        const string defaultAvatarPath = "content/assets/images/avatars/";
        const string defaultAvatarName = "avatar-default-288.webp";
        const string sourceAvatarPath = $"{defaultAvatarPath}{defaultAvatarName}";

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var destinationAvatarPath = $"content/users/{newUserId}/{defaultAvatarName}";

        var defaultAvatar = await azureBlob.OpenRead(sourceAvatarPath, cancellationToken);
        if (defaultAvatar is not null)
            await azureBlob.UploadFile(defaultAvatar.Content!, destinationAvatarPath, cancellationToken: cancellationToken);

        return defaultAvatar != null ? defaultAvatarName : null;
    }

    private async Task SendNotification(Guid messageId, string languageId, string emailAddress, Guid activationId, DateTime activationIdEnds, CancellationToken cancellationToken)
    {
        var configuration = new CreateUserConfiguration
        {
            LanguageId = languageId,
            MessageId = messageId,
            EmailAddress = emailAddress,
            ActivationId = activationId,
            ActivationIdEnds = activationIdEnds
        };

        await _emailSenderService.SendToServiceBus(configuration, cancellationToken);
    }
}