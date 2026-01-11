using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.CookieAccessorService;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;

namespace TokanPages.Backend.Application.Users.Commands;

public class ReAuthenticateUserCommandHandler : RequestHandler<ReAuthenticateUserCommand, ReAuthenticateUserCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private readonly ICookieAccessor _cookieAccessor;

    private readonly IConfiguration _configuration;

    public ReAuthenticateUserCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, IDateTimeService dateTimeService, 
        IUserService userService, IConfiguration configuration, ICookieAccessor cookieAccessor) : base(operationsDbContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
        _configuration = configuration;
        _cookieAccessor = cookieAccessor;
    }

    public override async Task<ReAuthenticateUserCommandResult> Handle(ReAuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, false, cancellationToken);
        var csrfToken = _cookieAccessor.Get("X-CSRF-Token");
        if (csrfToken is null)
            throw new AccessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        var userRefreshTokens = await OperationsDbContext.UserRefreshTokens
            .AsNoTracking()
            .Where(tokens => tokens.Token == csrfToken)
            .ToListAsync(cancellationToken);

        var savedRefreshToken = userRefreshTokens.SingleOrDefault(tokens => tokens.Token == csrfToken);
        if (savedRefreshToken == null) 
            throw new AccessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        var requesterIpAddress = _userService.GetRequestIpAddress();
        if (_userService.IsRefreshTokenRevoked(savedRefreshToken))
        {
            var reason = $"Attempted reuse of revoked ancestor token: {csrfToken}";
            var tokensInput = new RevokeRefreshTokensInput
            {
                UserRefreshTokens = userRefreshTokens, 
                SavedUserRefreshTokens = savedRefreshToken, 
                RequesterIpAddress = requesterIpAddress, 
                Reason = reason, 
                SaveImmediately = false
            };

            await _userService.RevokeDescendantRefreshTokens(tokensInput, cancellationToken);
            OperationsDbContext.UserRefreshTokens.Update(savedRefreshToken);
            await OperationsDbContext.SaveChangesAsync(cancellationToken);
        }

        if (!_userService.IsRefreshTokenActive(savedRefreshToken)) 
            throw new AccessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        var tokenInput = new ReplaceRefreshTokenInput
        {
            UserId = user.Id, 
            SavedUserRefreshTokens = savedRefreshToken, 
            RequesterIpAddress = requesterIpAddress, 
            SaveImmediately = true
        };
        
        var newRefreshToken = await _userService.ReplaceRefreshToken(tokenInput, cancellationToken);
        await _userService.DeleteOutdatedRefreshTokens(user.Id, false, cancellationToken);
        await OperationsDbContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);

        var currentDateTime = _dateTimeService.Now;
        var ipAddress = _userService.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_configuration.GetValue<int>("Ids_WebToken_Maturity"));
        var userToken = await _userService.GenerateUserToken(user, tokenExpires, cancellationToken);

        var roles = await _userService.GetUserRoles(user.Id, cancellationToken) ?? new List<GetUserRolesOutput>();
        var permissions = await _userService.GetUserPermissions(user.Id, cancellationToken) ?? new List<GetUserPermissionsOutput>();

        var newUserToken = new UserToken
        {
            UserId = user.Id,
            Token = userToken,
            Expires = tokenExpires,
            Created = currentDateTime,
            CreatedByIp = ipAddress,
            Command = nameof(ReAuthenticateUserCommand)
        };

        await OperationsDbContext.UserTokens.AddAsync(newUserToken, cancellationToken);
        await OperationsDbContext.SaveChangesAsync(cancellationToken);

        var userInfo = await TryGetUserInfo(user.Id, cancellationToken);
        var expiresIn = _configuration.GetValue<int>("Ids_RefreshToken_Maturity");

        _cookieAccessor.Set("X-CSRF-Token", newRefreshToken.Token, maxAge: TimeSpan.FromMinutes(expiresIn));

        return new ReAuthenticateUserCommandResult
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
            Roles = roles,
            Permissions = permissions
        };
    }

    private async Task<UserInfo> TryGetUserInfo(Guid userId, CancellationToken cancellationToken = default)
    {
        var userInfo = await OperationsDbContext.UserInformation
            .AsNoTracking()
            .Where(info => info.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (userInfo is null) 
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        return userInfo;
    }
}