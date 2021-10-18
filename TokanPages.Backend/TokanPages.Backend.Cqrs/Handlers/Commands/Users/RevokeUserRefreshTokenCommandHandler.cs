namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Logger;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserServiceProvider;
    using MediatR;
    
    public class RevokeUserRefreshTokenCommandHandler : TemplateHandler<RevokeUserRefreshTokenCommand, Unit>
    {
        private readonly IUserServiceProvider _userServiceProvider;
        
        public RevokeUserRefreshTokenCommandHandler(DatabaseContext databaseContext, ILogger logger, 
            IUserServiceProvider userServiceProvider) : base(databaseContext, logger)
        {
            _userServiceProvider = userServiceProvider;
        }

        public override async Task<Unit> Handle(RevokeUserRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshTokens = await DatabaseContext.UserRefreshTokens
                .Where(tokens => tokens.Token == request.RefreshToken)
                .SingleOrDefaultAsync(cancellationToken);

            if (refreshTokens == null)
                throw new BusinessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

            var requestIpAddress = _userServiceProvider.GetRequestIpAddress();
            const string reason = "Revoked by Admin";
            
            await _userServiceProvider.RevokeRefreshToken(refreshTokens, requestIpAddress, reason, null, true, cancellationToken);            
            return await Task.FromResult(Unit.Value);
        }
    }
}