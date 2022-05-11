namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
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
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;

public class ReAuthenticateUserCommandHandler : RequestHandler<ReAuthenticateUserCommand, ReAuthenticateUserCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private readonly IApplicationSettings _applicationSettings;

    public ReAuthenticateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IDateTimeService dateTimeService, 
        IUserService userService, IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
        _applicationSettings = applicationSettings;
    }

    public override async Task<ReAuthenticateUserCommandResult> Handle(ReAuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserId() ?? Guid.Empty;
        var userRefreshTokens = await DatabaseContext.UserRefreshTokens
            .AsNoTracking()
            .Where(tokens => tokens.Token == request.RefreshToken)
            .ToListAsync(cancellationToken);

        var savedRefreshToken = userRefreshTokens.SingleOrDefault(tokens => tokens.Token == request.RefreshToken);
        if (savedRefreshToken == null)
            throw InvalidTokenException;

        var requesterIpAddress = _userService.GetRequestIpAddress();

        if (_userService.IsRefreshTokenRevoked(savedRefreshToken))
        {
            var reason = $"Attempted reuse of revoked ancestor token: {request.RefreshToken}";
            await _userService.RevokeDescendantRefreshTokens(userRefreshTokens, savedRefreshToken, requesterIpAddress, 
                reason, false, cancellationToken);
                
            DatabaseContext.UserRefreshTokens.Update(savedRefreshToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        if (!_userService.IsRefreshTokenActive(savedRefreshToken))
            throw InvalidTokenException;

        var newRefreshToken = await _userService.ReplaceRefreshToken(userId, savedRefreshToken, 
            requesterIpAddress, true, cancellationToken);

        await _userService.DeleteOutdatedRefreshTokens(userId, false, cancellationToken);
        await DatabaseContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);

        var currentDateTime = _dateTimeService.Now;
        var currentUser = await DatabaseContext.Users.SingleAsync(users => users.Id == userId, cancellationToken);
        var ipAddress = _userService.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_applicationSettings.IdentityServer.WebTokenExpiresIn);
        var userToken = await _userService.GenerateUserToken(currentUser, tokenExpires, cancellationToken);

        currentUser.LastLogged = currentDateTime;
        var roles = await _userService.GetUserRoles(currentUser.Id);
        var permissions = await _userService.GetUserPermissions(currentUser.Id);

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
            Email = currentUser.EmailAddress,
            ShortBio = currentUser.ShortBio,
            Registered = currentUser.Registered,
            UserToken = userToken,
            RefreshToken = newRefreshToken.Token,
            Roles = roles,
            Permissions = permissions
        };
    }

    private static AccessException InvalidTokenException 
        => new (nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);
}