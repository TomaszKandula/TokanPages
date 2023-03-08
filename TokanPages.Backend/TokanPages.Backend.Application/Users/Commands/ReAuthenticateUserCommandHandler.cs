using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;

namespace TokanPages.Backend.Application.Users.Commands;

public class ReAuthenticateUserCommandHandler : RequestHandler<ReAuthenticateUserCommand, ReAuthenticateUserCommandResult>
{
    private readonly IDateTimeService _dateTimeService;

    private readonly IUserService _userService;

    private readonly IConfiguration _configuration;

    public ReAuthenticateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IDateTimeService dateTimeService, 
        IUserService userService, IConfiguration configuration) : base(databaseContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userService = userService;
        _configuration = configuration;
    }

    public override async Task<ReAuthenticateUserCommandResult> Handle(ReAuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, false, cancellationToken);
        var userRefreshTokens = await DatabaseContext.UserRefreshTokens
            .AsNoTracking()
            .Where(tokens => tokens.Token == request.RefreshToken)
            .ToListAsync(cancellationToken);

        var savedRefreshToken = userRefreshTokens.SingleOrDefault(tokens => tokens.Token == request.RefreshToken);
        if (savedRefreshToken == null) throw InvalidTokenException;

        var requesterIpAddress = _userService.GetRequestIpAddress();
        if (_userService.IsRefreshTokenRevoked(savedRefreshToken))
        {
            var reason = $"Attempted reuse of revoked ancestor token: {request.RefreshToken}";
            var tokensInput = new RevokeRefreshTokensInput
            {
                UserRefreshTokens = userRefreshTokens, 
                SavedUserRefreshTokens = savedRefreshToken, 
                RequesterIpAddress = requesterIpAddress, 
                Reason = reason, 
                SaveImmediately = false
            };

            await _userService.RevokeDescendantRefreshTokens(tokensInput, cancellationToken);
            DatabaseContext.UserRefreshTokens.Update(savedRefreshToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        if (!_userService.IsRefreshTokenActive(savedRefreshToken)) throw InvalidTokenException;
        var tokenInput = new ReplaceRefreshTokenInput
        {
            UserId = user.Id, 
            SavedUserRefreshTokens = savedRefreshToken, 
            RequesterIpAddress = requesterIpAddress, 
            SaveImmediately = true
        };
        
        var newRefreshToken = await _userService.ReplaceRefreshToken(tokenInput, cancellationToken);
        await _userService.DeleteOutdatedRefreshTokens(user.Id, false, cancellationToken);
        await DatabaseContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);

        var currentDateTime = _dateTimeService.Now;
        var ipAddress = _userService.GetRequestIpAddress();
        var tokenExpires = _dateTimeService.Now.AddMinutes(_configuration.GetValue<int>("Ids_WebToken_Maturity"));
        var userToken = await _userService.GenerateUserToken(user, tokenExpires, cancellationToken);

        var roles = await _userService.GetUserRoles(user.Id, cancellationToken) ?? new List<GetUserRolesOutput>();
        var permissions = await _userService.GetUserPermissions(user.Id, cancellationToken) ?? new List<GetUserPermissionsOutput>();

        var newUserToken = new UserTokens
        {
            UserId = user.Id,
            Token = userToken,
            Expires = tokenExpires,
            Created = currentDateTime,
            CreatedByIp = ipAddress,
            Command = nameof(ReAuthenticateUserCommand)
        };

        await DatabaseContext.UserTokens.AddAsync(newUserToken, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        var userInfo = await DatabaseContext.UserInfo
            .Where(info => info.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        return new ReAuthenticateUserCommandResult
        {
            UserId = user.Id,
            AliasName = user.UserAlias,
            AvatarName = userInfo.UserImageName,
            FirstName = userInfo.FirstName,
            LastName = userInfo.LastName,
            Email = user.EmailAddress,
            ShortBio = userInfo.UserAboutText,
            Registered = user.CreatedAt,
            TokenExpires = tokenExpires,
            RefreshTokenExpires = newRefreshToken.Expires,
            UserToken = userToken,
            RefreshToken = newRefreshToken.Token,
            Roles = roles,
            Permissions = permissions
        };
    }

    private static AccessException InvalidTokenException 
        => new (nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);
}