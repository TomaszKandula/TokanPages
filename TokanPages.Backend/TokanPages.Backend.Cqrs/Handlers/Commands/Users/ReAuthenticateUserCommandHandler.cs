namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using Shared.Models;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserServiceProvider;
    using Shared.Services.DateTimeService;

    public class ReAuthenticateUserCommandHandler : TemplateHandler<ReAuthenticateUserCommand, ReAuthenticateUserCommandResult>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IDateTimeService FDateTimeService;

        private readonly IUserServiceProvider FUserServiceProvider;

        private readonly IdentityServer FIdentityServer;

        public ReAuthenticateUserCommandHandler(DatabaseContext ADatabaseContext, IDateTimeService ADateTimeService, 
            IUserServiceProvider AUserServiceProvider, IdentityServer AIdentityServer)
        {
            FDatabaseContext = ADatabaseContext;
            FDateTimeService = ADateTimeService;
            FUserServiceProvider = AUserServiceProvider;
            FIdentityServer = AIdentityServer;
        }

        public override async Task<ReAuthenticateUserCommandResult> Handle(ReAuthenticateUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LRefreshTokenFromRequest = FUserServiceProvider.GetRefreshTokenCookie(Constants.CookieNames.REFRESH_TOKEN);
            if (string.IsNullOrEmpty(LRefreshTokenFromRequest))
                throw MissingRefreshTokenException;

            var LUserRefreshTokens = await FDatabaseContext.UserRefreshTokens
                .AsNoTracking()
                .Where(ARefreshTokens => ARefreshTokens.UserId == ARequest.Id)
                .ToListAsync(ACancellationToken);

            var LSavedRefreshToken = LUserRefreshTokens.SingleOrDefault(ARefreshTokens => ARefreshTokens.Token == LRefreshTokenFromRequest);
            if (LSavedRefreshToken == null)
                throw InvalidTokenException;
            
            var LRequesterIpAddress = FUserServiceProvider.GetRequestIpAddress();

            if (FUserServiceProvider.IsRefreshTokenRevoked(LSavedRefreshToken))
            {
                var LReason = $"Attempted reuse of revoked ancestor token: {LRefreshTokenFromRequest}";
                await FUserServiceProvider.RevokeDescendantRefreshTokens(LUserRefreshTokens, LSavedRefreshToken, LRequesterIpAddress, 
                    LReason, false, ACancellationToken);
                
                FDatabaseContext.UserRefreshTokens.Update(LSavedRefreshToken);
                await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            }

            if (!FUserServiceProvider.IsRefreshTokenActive(LSavedRefreshToken))
                throw InvalidTokenException;

            var LNewRefreshToken = await FUserServiceProvider.ReplaceRefreshToken(ARequest.Id, LSavedRefreshToken, 
                LRequesterIpAddress, true, ACancellationToken);

            await FUserServiceProvider.DeleteOutdatedRefreshTokens(ARequest.Id, false, ACancellationToken);
            await FDatabaseContext.UserRefreshTokens.AddAsync(LNewRefreshToken, ACancellationToken);

            var LCurrentUser = await FDatabaseContext.Users.SingleAsync(AUsers => AUsers.Id == ARequest.Id, ACancellationToken);
            LCurrentUser.LastLogged = FDateTimeService.Now;
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            
            var LTokenExpires = FDateTimeService.Now.AddMinutes(FIdentityServer.WebTokenExpiresIn);
            var LUserToken = await FUserServiceProvider.GenerateUserToken(LCurrentUser, LTokenExpires, ACancellationToken);
            FUserServiceProvider.SetRefreshTokenCookie(LNewRefreshToken.Token, FIdentityServer.RefreshTokenExpiresIn);

            var LRoles = await FUserServiceProvider.GetUserRoles(LCurrentUser.Id);
            var LPermissions = await FUserServiceProvider.GetUserPermissions(LCurrentUser.Id);
            
            return new ReAuthenticateUserCommandResult
            {
                UserId = LCurrentUser.Id,
                AliasName = LCurrentUser.UserAlias,
                AvatarName = LCurrentUser.AvatarName,
                FirstName = LCurrentUser.FirstName,
                LastName = LCurrentUser.LastName,
                ShortBio = LCurrentUser.ShortBio,
                Registered = LCurrentUser.Registered,
                UserToken = LUserToken,
                Roles = LRoles,
                Permissions = LPermissions
            };
        }

        private static BusinessException MissingRefreshTokenException 
            => new (nameof(ErrorCodes.MISSING_REFRESH_TOKEN), ErrorCodes.MISSING_REFRESH_TOKEN);

        private static BusinessException InvalidTokenException 
            => new (nameof(ErrorCodes.INVALID_REFRESH_TOKEN), ErrorCodes.INVALID_REFRESH_TOKEN);
    }
}