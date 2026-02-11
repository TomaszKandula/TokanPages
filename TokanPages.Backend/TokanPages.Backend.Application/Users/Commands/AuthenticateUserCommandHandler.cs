using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.CookieAccessorService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AuthenticateUserCommandHandler : RequestHandler<AuthenticateUserCommand, AuthenticateUserCommandResult>
{
    private readonly ICipheringService _cipheringService;

    private readonly IWebTokenUtility _webTokenUtility;
        
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private readonly IUserRepository _userRepository;

    private readonly ICookieAccessor _cookieAccessor;

    private readonly AppSettingsModel _appSettings;
        
    public AuthenticateUserCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        ICipheringService cipheringService, IWebTokenUtility webTokenUtility, IDateTimeService dateTimeService, 
        IUserService userService, IOptions<AppSettingsModel> options, ICookieAccessor cookieAccessor, IUserRepository userRepository) : base(operationDbContext, loggerService)
    {
        _cipheringService = cipheringService;
        _webTokenUtility = webTokenUtility;
        _dateTimeService = dateTimeService;
        _userService = userService;
        _appSettings = options.Value;
        _cookieAccessor = cookieAccessor;
        _userRepository = userRepository;
    }

    public override async Task<AuthenticateUserCommandResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var userDetails = await _userRepository.GetUserDetails(request.EmailAddress);
        if (userDetails is null)
        {
            LoggerService.LogError($"Cannot find user with given email address: '{request.EmailAddress}', or it has been removed.");
            throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);
        }

        if (!userDetails.IsActivated)
            throw new AccessException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

        if (userDetails.HasBusinessLock)
            throw new AuthorizationException(nameof(ErrorCodes.BUSINESS_LOCK_ENABLED), ErrorCodes.BUSINESS_LOCK_ENABLED);

        var isPasswordValid = _cipheringService.VerifyPassword(request.Password, userDetails.CryptedPassword);
        if (!isPasswordValid)
        {
            LoggerService.LogError($"Cannot positively verify given password supplied by user ({userDetails.UserId}) for email address: '{request.EmailAddress}'.");
            throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);
        }

        var currentDateTime = _dateTimeService.Now;
        var ipAddress = _userService.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_appSettings.IdsWebTokenMaturity);
        var userToken = await _userService.GenerateUserToken(userDetails.UserId, tokenExpires);

        var expiresIn = _appSettings.IdsRefreshTokenMaturity;
        var refreshToken = _webTokenUtility.GenerateRefreshToken(ipAddress, expiresIn);

        await _userRepository.CreateUserToken(userDetails.UserId, userToken, tokenExpires, currentDateTime, ipAddress);
        await _userRepository.CreateUserRefreshToken(userDetails.UserId, refreshToken.Token, tokenExpires, currentDateTime, ipAddress);
        _cookieAccessor.Set("X-CSRF-Token", refreshToken.Token, maxAge: TimeSpan.FromMinutes(expiresIn));

        var roles = await _userRepository.GetUserRoles(userDetails.UserId);
        var permissions = await _userRepository.GetUserPermissions(userDetails.UserId);

        var firstName = !string.IsNullOrWhiteSpace(userDetails.FirstName) ? userDetails.FirstName : userDetails.UserAlias;
        var lastName = !string.IsNullOrWhiteSpace(userDetails.LastName) ? userDetails.LastName : userDetails.UserAlias[..3];

        return new AuthenticateUserCommandResult
        {
            UserId = userDetails.UserId,
            IsVerified = userDetails.IsVerified,
            AliasName = userDetails.UserAlias,
            AvatarName = userDetails.UserImageName,
            FirstName = firstName,
            LastName = lastName,
            Email = userDetails.EmailAddress,
            ShortBio = userDetails.UserAboutText,
            Registered = userDetails.Registered,
            UserToken = userToken,
            Roles = roles,
            Permissions = permissions
        };
    }
}