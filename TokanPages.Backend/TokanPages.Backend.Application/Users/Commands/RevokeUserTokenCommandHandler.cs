using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.WebTokenService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserTokenCommandHandler : RequestHandler<RevokeUserTokenCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IUserRepository _userRepository;

    private readonly IWebTokenValidation _webTokenValidation;

    public RevokeUserTokenCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IWebTokenValidation webTokenValidation, IUserRepository userRepository) 
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _webTokenValidation = webTokenValidation;
        _userRepository = userRepository;
    }

    public override async Task<Unit> Handle(RevokeUserTokenCommand request, CancellationToken cancellationToken)
    {
        var token = _webTokenValidation.GetWebTokenFromHeader();
        var userId = _userService.GetLoggedUserId();

        var tokens = await _userRepository.DoesUserTokenExist(userId, token);
        if (!tokens)
            throw new AuthorizationException(nameof(ErrorCodes.INVALID_USER_TOKEN), ErrorCodes.INVALID_USER_TOKEN);

        await _userRepository.DeleteUserToken(userId, token);
        return Unit.Value;
    }
}