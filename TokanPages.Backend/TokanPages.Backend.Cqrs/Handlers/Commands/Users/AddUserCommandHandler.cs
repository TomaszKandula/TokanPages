﻿namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;
using Database;
using Core.Exceptions;
using Domain.Entities;
using Shared.Services;
using Shared.Resources;
using Services.UserService;
using Services.CipheringService;
using Services.EmailSenderService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using Services.EmailSenderService.Models;
using Services.AzureStorageService.Factory;

public class AddUserCommandHandler : RequestHandler<AddUserCommand, Guid>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly ICipheringService _cipheringService;

    private readonly IEmailSenderService _emailSenderService;

    private readonly IApplicationSettings _applicationSettings;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IUserService _userService;

    public AddUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IDateTimeService dateTimeService,
        ICipheringService cipheringService, IEmailSenderService emailSenderService, IApplicationSettings applicationSettings, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IUserService userService) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _cipheringService = cipheringService;
        _emailSenderService = emailSenderService;
        _applicationSettings = applicationSettings;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
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
        LoggerService.LogInformation($"New hashed password has been generated. Requested by: {request.EmailAddress}.");

        var expiresIn = _applicationSettings.ExpirationSettings.ActivationIdExpiresIn;
        var activationId = Guid.NewGuid();
        var activationIdEnds = _dateTimeService.Now.AddMinutes(expiresIn);

        if (users != null && (users.ActivationIdEnds != null || users.ActivationIdEnds < _dateTimeService.Now))
        {
            users.CryptedPassword = getHashedPassword;
            users.ActivationId = activationId;
            users.ActivationIdEnds = activationIdEnds;

            await DatabaseContext.SaveChangesAsync(cancellationToken);
            await SendNotification(request.EmailAddress, activationId, activationIdEnds, cancellationToken);

            LoggerService.LogInformation($"Re-registering new user after ActivationId expired, user id: {users.Id}.");
            return users.Id;
        }

        var newUserId = Guid.NewGuid();
        const string defaultAvatarPath = "content/assets/images/avatars/";
        const string defaultAvatarName = "avatar-default-288.jpeg";
        const string sourceAvatarPath = $"{defaultAvatarPath}{defaultAvatarName}";

        var azureBlob = _azureBlobStorageFactory.Create();
        var destinationAvatarPath = $"content/users/{newUserId}/{defaultAvatarName}";

        var defaultAvatar = await azureBlob.OpenRead(sourceAvatarPath, cancellationToken);
        if (defaultAvatar is not null)
            await azureBlob.UploadFile(defaultAvatar.Content, destinationAvatarPath, cancellationToken: cancellationToken);

        var newUser = new Users
        {
            Id = newUserId,
            EmailAddress = request.EmailAddress,
            UserAlias = request.UserAlias.ToLower(),
            CryptedPassword = getHashedPassword,
            ActivationId = activationId,
            ActivationIdEnds = activationIdEnds
        };

        var newUserInfo = new UserInfo
        {
            UserId = newUserId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserImageName = defaultAvatar != null ? defaultAvatarName : null,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = newUserId,
        };

        var timezoneOffset = _userService.GetRequestUserTimezoneOffset();
        var baseDateTime = _dateTimeService.Now.AddMinutes(-timezoneOffset);
        var expirationDate = baseDateTime.AddMinutes(expiresIn);

        await DatabaseContext.Users.AddAsync(newUser, cancellationToken);
        await DatabaseContext.UserInfo.AddAsync(newUserInfo, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        await SetupDefaultPermissions(newUser.Id, cancellationToken);
        await SendNotification(request.EmailAddress, activationId, expirationDate, cancellationToken);

        LoggerService.LogInformation($"Registering new user account, user id: {newUser.Id}.");
        return newUser.Id;
    }

    private async Task SetupDefaultPermissions(Guid userId, CancellationToken cancellationToken)
    {
        var userRoleName = Domain.Enums.Roles.EverydayUser.ToString();
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
        var configuration = new CreateUserConfiguration
        {
            EmailAddress = emailAddress,
            ActivationId = activationId,
            ActivationIdEnds = activationIdEnds
        };

        await _emailSenderService.SendNotification(configuration, cancellationToken);
    }
}