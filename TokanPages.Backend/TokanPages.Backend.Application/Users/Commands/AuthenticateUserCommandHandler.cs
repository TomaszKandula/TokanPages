using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AuthenticateUserCommandHandler : RequestHandler<AuthenticateUserCommand, AuthenticateUserCommandResult>
{
    private readonly ICipheringService _cipheringService;

    private readonly IWebTokenUtility _webTokenUtility;
        
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private readonly IConfiguration _configuration;
        
    public AuthenticateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        ICipheringService cipheringService, IWebTokenUtility webTokenUtility, IDateTimeService dateTimeService, 
        IUserService userService, IConfiguration configuration) : base(databaseContext, loggerService)
    {
        _cipheringService = cipheringService;
        _webTokenUtility = webTokenUtility;
        _dateTimeService = dateTimeService;
        _userService = userService;
        _configuration = configuration;
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

        if (user.HasBusinessLock)
            throw new AuthorizationException(nameof(ErrorCodes.BUSINESS_LOCK_ENABLED), ErrorCodes.BUSINESS_LOCK_ENABLED);

        var isPasswordValid = _cipheringService.VerifyPassword(request.Password!, user.CryptedPassword);
        if (!isPasswordValid)
        {
            LoggerService.LogError($"Cannot positively verify given password supplied by user ({user.Id}) for email address: '{request.EmailAddress}'.");
            throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
        }

        var currentDateTime = _dateTimeService.Now;
        var ipAddress = _userService.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_configuration.GetValue<int>("Ids_WebToken_Maturity"));
        var userToken = await _userService.GenerateUserToken(user, tokenExpires, cancellationToken);

        var expiresIn = _configuration.GetValue<int>("Ids_RefreshToken_Maturity");
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

        var roles = await _userService.GetUserRoles(user.Id, cancellationToken) ?? new List<GetUserRolesOutput>();
        var permissions = await _userService.GetUserPermissions(user.Id, cancellationToken) ?? new List<GetUserPermissionsOutput>();

        var userInfo = await TryGetUserInfo(user.Id, cancellationToken);
        return new AuthenticateUserCommandResult
        {
            UserId = user.Id,
            IsVerified = user.IsVerified,
            AliasName = user.UserAlias,
            AvatarName = userInfo.UserImageName,
            FirstName = !string.IsNullOrWhiteSpace(userInfo.FirstName) 
                ? userInfo.FirstName 
                : user.UserAlias,
            LastName = !string.IsNullOrWhiteSpace(userInfo.LastName) 
                ? userInfo.LastName 
                : user.UserAlias[..3],
            Email = user.EmailAddress,
            ShortBio = userInfo.UserAboutText,
            Registered = user.CreatedAt,
            UserToken = userToken,
            RefreshToken = refreshToken.Token,
            Roles = roles,
            Permissions = permissions
        };
    }

    private async Task<UserInfo> TryGetUserInfo(Guid userId, CancellationToken cancellationToken = default)
    {
        var userInfo = await DatabaseContext.UserInfo
            .AsNoTracking()
            .Where(info => info.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (userInfo is null) 
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        return userInfo;
    }
}