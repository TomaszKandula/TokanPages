namespace TokanPages.Backend.Application.Users.Commands;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Services.UserService.Models;
using Core.Utilities.LoggerService;
using MediatR;

public class RevokeUserRefreshTokenCommandHandler : Application.RequestHandler<RevokeUserRefreshTokenCommand, Unit>
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