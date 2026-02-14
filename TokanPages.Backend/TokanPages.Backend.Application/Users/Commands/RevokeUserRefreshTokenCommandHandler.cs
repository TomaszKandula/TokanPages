using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.CookieAccessorService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RevokeUserRefreshTokenCommandHandler : RequestHandler<RevokeUserRefreshTokenCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    private readonly ICookieAccessor _cookieAccessor;

    public RevokeUserRefreshTokenCommandHandler(ILoggerService loggerService, ICookieAccessor cookieAccessor, 
        IUserRepository userRepository) : base(loggerService)
    {
        _cookieAccessor = cookieAccessor;
        _userRepository = userRepository;
    }

    public override async Task<Unit> Handle(RevokeUserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var csrfToken = _cookieAccessor.Get("X-CSRF-Token");
        if (csrfToken is null)
            throw new AccessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

        await _userRepository.RemoveUserRefreshToken(csrfToken);
        return Unit.Value;
    }
}