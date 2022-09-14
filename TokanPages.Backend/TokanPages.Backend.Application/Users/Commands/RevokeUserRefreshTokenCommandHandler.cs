using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService;
using TokanPages.Services.UserService.Models;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserRefreshTokenCommandHandler : RequestHandler<RevokeUserRefreshTokenCommand, Unit>
{
    private readonly IUserService _userService;

    public RevokeUserRefreshTokenCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RevokeUserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(null, false, cancellationToken);
        var refreshTokens = await DatabaseContext.UserRefreshTokens
            .Where(tokens => tokens.UserId == user.Id)
            .Where(tokens => tokens.Token == request.RefreshToken)
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