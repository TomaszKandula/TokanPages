namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserServiceProvider;
    using MediatR;
    
    public class RevokeUserRefreshTokenCommandHandler : TemplateHandler<RevokeUserRefreshTokenCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IUserServiceProvider FUserServiceProvider;
        
        public RevokeUserRefreshTokenCommandHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
        }

        public override async Task<Unit> Handle(RevokeUserRefreshTokenCommand ARequest, CancellationToken ACancellationToken)
        {
            var LToken = await FDatabaseContext.UserRefreshTokens
                .Where(AUserRefreshToken => AUserRefreshToken.Token == ARequest.RefreshToken)
                .SingleOrDefaultAsync(ACancellationToken);

            if (LToken == null)
                throw new BusinessException(nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);

            var LRequestIpAddress = FUserServiceProvider.GetRequestIpAddress();
            const string REASON = "Revoked by Admin";
            
            await FUserServiceProvider.RevokeRefreshToken(LToken, LRequestIpAddress, REASON, null, true, ACancellationToken);            
            return await Task.FromResult(Unit.Value);
        }
    }
}