using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.CookieAccessorService.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class ReAuthenticateUserCommandHandler : RequestHandler<ReAuthenticateUserCommand, ReAuthenticateUserCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private readonly ICookieAccessor _cookieAccessor;

    private readonly IUserRepository _userRepository;

    private readonly IWebTokenUtility _webTokenUtility;

    private readonly AppSettingsModel _appSettings;

    public ReAuthenticateUserCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IDateTimeService dateTimeService, 
        IUserService userService, IOptions<AppSettingsModel> options, ICookieAccessor cookieAccessor, IUserRepository userRepository, 
        IWebTokenUtility webTokenUtility) : base(operationDbContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
        _appSettings = options.Value;
        _cookieAccessor = cookieAccessor;
        _userRepository = userRepository;
        _webTokenUtility = webTokenUtility;
    }

    public override async Task<ReAuthenticateUserCommandResult> Handle(ReAuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, false, cancellationToken);
        var ipAddress = _userService.GetRequestIpAddress();

        var csrfToken = _cookieAccessor.Get("X-CSRF-Token");
        if (csrfToken is null)
            throw new AccessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        var existingRefreshToken = await _userRepository.GetUserRefreshToken(csrfToken);
        if (existingRefreshToken == null) 
            throw new AccessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        var newRefreshToken = _webTokenUtility.GenerateRefreshToken(ipAddress, _appSettings.IdsRefreshTokenMaturity);
        var expiresIn = _appSettings.IdsRefreshTokenMaturity;
        _cookieAccessor.Set("X-CSRF-Token", newRefreshToken.Token, maxAge: TimeSpan.FromMinutes(expiresIn));

        await _userRepository.InsertUserRefreshToken(user.Id, newRefreshToken.Token, newRefreshToken.Expires, newRefreshToken.Created, newRefreshToken.CreatedByIp);
        await _userRepository.DeleteUserRefreshToken(existingRefreshToken.Id);

        var currentDateTime = _dateTimeService.Now;
        var tokenExpires = _dateTimeService.Now.AddMinutes(_appSettings.IdsWebTokenMaturity);
        var userToken = await _userService.GenerateUserToken(user, tokenExpires);
        await _userRepository.InsertUserToken(user.Id, userToken, tokenExpires, currentDateTime, ipAddress);

        var userDetails = await _userRepository.GetUserDetails(user.Id);
        var roles = await _userRepository.GetUserRoles(user.Id);
        var permissions = await _userRepository.GetUserPermissions(user.Id);

        var firstName = !string.IsNullOrWhiteSpace(userDetails?.FirstName) ? userDetails.FirstName : user.UserAlias;
        var lastName = !string.IsNullOrWhiteSpace(userDetails?.LastName) ? userDetails.LastName : user.UserAlias[..3];

        return new ReAuthenticateUserCommandResult
        {
            UserId = user.Id,
            IsVerified = user.IsVerified,
            AliasName = user.UserAlias,
            AvatarName = userDetails?.UserImageName,
            FirstName = firstName,
            LastName = lastName,
            Email = user.EmailAddress,
            ShortBio = userDetails?.UserAboutText,
            Registered = user.CreatedAt,
            UserToken = userToken,
            Roles = roles,
            Permissions = permissions
        };
    }
}