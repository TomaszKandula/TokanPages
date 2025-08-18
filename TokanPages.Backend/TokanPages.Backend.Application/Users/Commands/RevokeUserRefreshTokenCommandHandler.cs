using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.CookieAccessorService;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserRefreshTokenCommandHandler : RequestHandler<RevokeUserRefreshTokenCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ICookieAccessor _cookieAccessor;

    public RevokeUserRefreshTokenCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, ICookieAccessor cookieAccessor) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _cookieAccessor = cookieAccessor;
    }

    public override async Task<Unit> Handle(RevokeUserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(null, false, cancellationToken);
        var csrfToken = _cookieAccessor.Get("X-CSRF-Token");
        if (csrfToken is null)
            throw new AccessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        var refreshTokens = await DatabaseContext.UserRefreshTokens
            .Where(tokens => tokens.UserId == user.Id)
            .Where(tokens => tokens.Token == csrfToken)
            .SingleOrDefaultAsync(cancellationToken);

        if (refreshTokens == null)
            throw new AuthorizationException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        var requestIpAddress = _userService.GetRequestIpAddress();
        var reason = $"Revoked by user ID: {user.Id}";

        var input = new RevokeRefreshTokenInput
        {
            UserRefreshTokens = refreshTokens, 
            RequesterIpAddress = requestIpAddress, 
            Reason = reason, 
            ReplacedByToken = null, 
            SaveImmediately = true
        };

        await _userService.RevokeRefreshToken(input, cancellationToken);            
        return Unit.Value;
    }
}