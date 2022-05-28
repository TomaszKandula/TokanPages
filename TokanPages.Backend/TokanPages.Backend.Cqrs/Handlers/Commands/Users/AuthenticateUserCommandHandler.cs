namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
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
        
    public AuthenticateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, ICipheringService cipheringService, 
        IWebTokenUtility webTokenUtility, IDateTimeService dateTimeService, IUserService userService, 
        IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _cipheringService = cipheringService;
        _webTokenUtility = webTokenUtility;
        _dateTimeService = dateTimeService;
        _userService = userService;
        _applicationSettings = applicationSettings;
    }

    public override async Task<AuthenticateUserCommandResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var users = await DatabaseContext.Users
            .Where(users => users.EmailAddress == request.EmailAddress)
            .ToListAsync(cancellationToken);

        if (!users.Any())
        {
            LoggerService.LogError($"Cannot find user with given email address: '{request.EmailAddress}'.");
            throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
        }

        var currentUser = users.First();
        var isPasswordValid = _cipheringService.VerifyPassword(request.Password, currentUser.CryptedPassword);

        if (!isPasswordValid)
        {
            LoggerService.LogError($"Cannot positively verify given password supplied by user (Id: {currentUser.Id}).");
            throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
        }

        if (!currentUser.IsActivated)
            throw new AccessException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

        var currentDateTime = _dateTimeService.Now;
        var ipAddress = _userService.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_applicationSettings.IdentityServer.WebTokenExpiresIn);
        var userToken = await _userService.GenerateUserToken(currentUser, tokenExpires, cancellationToken);

        var expiresIn = _applicationSettings.IdentityServer.RefreshTokenExpiresIn;
        var refreshToken = _webTokenUtility.GenerateRefreshToken(ipAddress, expiresIn);

        var newUserToken = new UserTokens
        {
            UserId = currentUser.Id,
            Token = userToken,
            Expires = tokenExpires,
            Created = currentDateTime,
            CreatedByIp = ipAddress,
            Command = nameof(AuthenticateUserCommand)
        };

        var newRefreshToken = new UserRefreshTokens
        {
            UserId = currentUser.Id,
            Token = refreshToken.Token,
            Expires = refreshToken.Expires,
            Created = refreshToken.Created,
            CreatedByIp = refreshToken.CreatedByIp
        };

        await _userService.DeleteOutdatedRefreshTokens(currentUser.Id, false, cancellationToken);
        await DatabaseContext.UserTokens.AddAsync(newUserToken, cancellationToken);
        await DatabaseContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        var roles = await _userService.GetUserRoles(currentUser.Id, cancellationToken);
        var permissions = await _userService.GetUserPermissions(currentUser.Id, cancellationToken);

        var userInfo = await DatabaseContext.UserInfo
            .Where(info => info.UserId == currentUser.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return new AuthenticateUserCommandResult
        {
            UserId = currentUser.Id,
            AliasName = currentUser.UserAlias,
            AvatarName = userInfo.UserImageName,
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            Email = currentUser.EmailAddress,
            ShortBio = userInfo.UserAboutText,
            Registered = currentUser.CreatedAt,
            UserToken = userToken,
            RefreshToken = refreshToken.Token,
            Roles = roles,
            Permissions = permissions
        };
    }
}