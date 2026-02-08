using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserTokenCommandHandler : RequestHandler<RevokeUserTokenCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IWebTokenValidation _webTokenValidation;

    private readonly IDateTimeService _dateTimeService;

    public RevokeUserTokenCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IWebTokenValidation webTokenValidation, IDateTimeService dateTimeService) 
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _webTokenValidation = webTokenValidation;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(RevokeUserTokenCommand request, CancellationToken cancellationToken)
    {
        var token = _webTokenValidation.GetWebTokenFromHeader();
        var userId = _userService.GetLoggedUserId();
        var tokens = await OperationDbContext.UserTokens
            .Where(userTokens => userTokens.Token == token)
            .FirstOrDefaultAsync(cancellationToken);

        if (tokens == null)
            throw new AuthorizationException(nameof(ErrorCodes.INVALID_USER_TOKEN), ErrorCodes.INVALID_USER_TOKEN);

        var requestIpAddress = _userService.GetRequestIpAddress();
        var reason = $"Revoked by user ID: {userId}";

        tokens.Revoked = _dateTimeService.Now;
        tokens.RevokedByIp = requestIpAddress; 
        tokens.ReasonRevoked = reason;

        await OperationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}