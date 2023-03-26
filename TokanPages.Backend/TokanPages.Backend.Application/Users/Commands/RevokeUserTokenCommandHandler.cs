using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserTokenCommandHandler : RequestHandler<RevokeUserTokenCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IWebTokenValidation _webTokenValidation;

    private readonly IDateTimeService _dateTimeService;

    public RevokeUserTokenCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IWebTokenValidation webTokenValidation, IDateTimeService dateTimeService) 
        : base(databaseContext, loggerService)
    {
        _userService = userService;
        _webTokenValidation = webTokenValidation;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(RevokeUserTokenCommand request, CancellationToken cancellationToken)
    {
        var token = _webTokenValidation.GetWebTokenFromHeader();
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var tokens = await DatabaseContext.UserTokens
            .Where(userTokens => userTokens.Token == token)
            .FirstOrDefaultAsync(cancellationToken);

        if (tokens == null)
            throw new AuthorizationException(nameof(ErrorCodes.INVALID_USER_TOKEN), ErrorCodes.INVALID_USER_TOKEN);

        var requestIpAddress = _userService.GetRequestIpAddress();
        var reason = $"Revoked by user ID: {user.Id}";

        tokens.Revoked = _dateTimeService.Now;
        tokens.RevokedByIp = requestIpAddress; 
        tokens.ReasonRevoked = reason;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}