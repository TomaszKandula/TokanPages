namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Dto.Users;
using Domain.Entities;
using Core.Exceptions;
using Shared.Services;
using Shared.Resources;
using Services.UserService;
using Services.WebTokenService;
using Services.CipheringService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;

public class AuthenticateUserCommandHandler : RequestHandler<AuthenticateUserCommand, AuthenticateUserCommandResult>
{
    private readonly ICipheringService _cipheringService;

    private readonly IWebTokenUtility _webTokenUtility;
        
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private readonly IApplicationSettings _applicationSettings;
        
    public AuthenticateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ICipheringService cipheringService, IWebTokenUtility webTokenUtility, IDateTimeService dateTimeService, 
        IUserService userService, IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _cipheringService = cipheringService;
        _webTokenUtility = webTokenUtility;
        _dateTimeService = dateTimeService;
        _userService = userService;
        _applicationSettings = applicationSettings;
    }

    public override async Task<AuthenticateUserCommandResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await DatabaseContext.Users
            .Where(users => !users.IsDeleted)
            .Where(users => users.EmailAddress == request.EmailAddress)
            .SingleOrDefaultAsync(cancellationToken);

        if (user is null)
        {
            LoggerService.LogError($"Cannot find user with given email address: '{request.EmailAddress}', or it has been removed.");
            throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
        }

        if (!user.IsActivated)
            throw new AccessException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

        var isPasswordValid = _cipheringService.VerifyPassword(request.Password!, user.CryptedPassword);
        if (!isPasswordValid)
        {
            LoggerService.LogError($"Cannot positively verify given password supplied by user ({user.Id}) for email address: '{request.EmailAddress}'.");
            throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
        }

        var currentDateTime = _dateTimeService.Now;
        var ipAddress = _userService.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_applicationSettings.IdentityServer.WebTokenExpiresIn);
        var userToken = await _userService.GenerateUserToken(user, tokenExpires, cancellationToken);

        var expiresIn = _applicationSettings.IdentityServer.RefreshTokenExpiresIn;
        var refreshToken = _webTokenUtility.GenerateRefreshToken(ipAddress, expiresIn);

        var newUserToken = new UserTokens
        {
            UserId = user.Id,
            Token = userToken,
            Expires = tokenExpires,
            Created = currentDateTime,
            CreatedByIp = ipAddress,
            Command = nameof(AuthenticateUserCommand)
        };

        var newRefreshToken = new UserRefreshTokens
        {
            UserId = user.Id,
            Token = refreshToken.Token,
            Expires = refreshToken.Expires,
            Created = refreshToken.Created,
            CreatedByIp = refreshToken.CreatedByIp
        };

        await _userService.DeleteOutdatedRefreshTokens(user.Id, false, cancellationToken);
        await DatabaseContext.UserTokens.AddAsync(newUserToken, cancellationToken);
        await DatabaseContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        var roles = await _userService.GetUserRoles(user.Id, cancellationToken) ?? new List<GetUserRoleDto>();
        var permissions = await _userService.GetUserPermissions(user.Id, cancellationToken) ?? new List<GetUserPermissionDto>();

        var userInfo = await DatabaseContext.UserInfo
            .Where(info => info.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return new AuthenticateUserCommandResult
        {
            UserId = user.Id,
            AliasName = user.UserAlias,
            AvatarName = userInfo.UserImageName,
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            Email = user.EmailAddress,
            ShortBio = userInfo.UserAboutText,
            Registered = user.CreatedAt,
            UserToken = userToken,
            RefreshToken = refreshToken.Token,
            Roles = roles,
            Permissions = permissions
        };
    }
}