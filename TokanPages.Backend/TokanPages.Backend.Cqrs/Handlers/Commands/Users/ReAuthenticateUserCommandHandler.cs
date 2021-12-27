namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;
using Database;
using Domain.Entities;
using Core.Exceptions;
using Shared.Services;
using Shared.Resources;
using Core.Utilities.LoggerService;
using Services.UserServiceProvider;
using Core.Utilities.DateTimeService;

public class ReAuthenticateUserCommandHandler : TemplateHandler<ReAuthenticateUserCommand, ReAuthenticateUserCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserServiceProvider _userServiceProvider;

    private readonly IApplicationSettings _applicationSettings;

    public ReAuthenticateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IDateTimeService dateTimeService, 
        IUserServiceProvider userServiceProvider, IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userServiceProvider = userServiceProvider;
        _applicationSettings = applicationSettings;
    }

    public override async Task<ReAuthenticateUserCommandResult> Handle(ReAuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var refreshTokenFromRequest = _userServiceProvider.GetRefreshTokenCookie(Constants.CookieNames.RefreshToken);
        if (string.IsNullOrEmpty(refreshTokenFromRequest))
            throw MissingRefreshTokenException;

        var userRefreshTokens = await DatabaseContext.UserRefreshTokens
            .AsNoTracking()
            .Where(tokens => tokens.UserId == request.Id)
            .ToListAsync(cancellationToken);

        var savedRefreshToken = userRefreshTokens.SingleOrDefault(tokens => tokens.Token == refreshTokenFromRequest);
        if (savedRefreshToken == null)
            throw InvalidTokenException;
            
        var requesterIpAddress = _userServiceProvider.GetRequestIpAddress();

        if (_userServiceProvider.IsRefreshTokenRevoked(savedRefreshToken))
        {
            var reason = $"Attempted reuse of revoked ancestor token: {refreshTokenFromRequest}";
            await _userServiceProvider.RevokeDescendantRefreshTokens(userRefreshTokens, savedRefreshToken, requesterIpAddress, 
                reason, false, cancellationToken);
                
            DatabaseContext.UserRefreshTokens.Update(savedRefreshToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        if (!_userServiceProvider.IsRefreshTokenActive(savedRefreshToken))
            throw InvalidTokenException;

        var newRefreshToken = await _userServiceProvider.ReplaceRefreshToken(request.Id, savedRefreshToken, 
            requesterIpAddress, true, cancellationToken);

        await _userServiceProvider.DeleteOutdatedRefreshTokens(request.Id, false, cancellationToken);
        await DatabaseContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);

        var currentDateTime = _dateTimeService.Now;
        var currentUser = await DatabaseContext.Users.SingleAsync(users => users.Id == request.Id, cancellationToken);
        var ipAddress = _userServiceProvider.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_applicationSettings.IdentityServer.WebTokenExpiresIn);
        var userToken = await _userServiceProvider.GenerateUserToken(currentUser, tokenExpires, cancellationToken);

        _userServiceProvider.SetRefreshTokenCookie(newRefreshToken.Token, _applicationSettings.IdentityServer.RefreshTokenExpiresIn);
        currentUser.LastLogged = currentDateTime;

        var roles = await _userServiceProvider.GetUserRoles(currentUser.Id);
        var permissions = await _userServiceProvider.GetUserPermissions(currentUser.Id);

        var newUserToken = new UserTokens
        {
            UserId = currentUser.Id,
            Token = userToken,
            Expires = tokenExpires,
            Created = currentDateTime,
            CreatedByIp = ipAddress,
            Command = nameof(ReAuthenticateUserCommand)
        };

        await DatabaseContext.UserTokens.AddAsync(newUserToken, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return new ReAuthenticateUserCommandResult
        {
            UserId = currentUser.Id,
            AliasName = currentUser.UserAlias,
            AvatarName = currentUser.AvatarName,
            FirstName = currentUser.FirstName,
            LastName = currentUser.LastName,
            ShortBio = currentUser.ShortBio,
            Registered = currentUser.Registered,
            UserToken = userToken,
            Roles = roles,
            Permissions = permissions
        };
    }

    private static AccessException MissingRefreshTokenException 
        => new (nameof(ErrorCodes.MISSING_REFRESH_TOKEN), ErrorCodes.MISSING_REFRESH_TOKEN);

    private static AccessException InvalidTokenException 
        => new (nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);
}