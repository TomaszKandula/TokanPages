namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Shared.Models;
    using Domain.Entities;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.CipheringService;
    using Services.UserServiceProvider;
    using Shared.Services.DateTimeService;
    using Identity.Services.JwtUtilityService;

    public class AuthenticateUserCommandHandler : TemplateHandler<AuthenticateUserCommand, AuthenticateUserCommandResult>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly ICipheringService FCipheringService;

        private readonly IJwtUtilityService FJwtUtilityService;
        
        private readonly IDateTimeService FDateTimeService;

        private readonly IUserServiceProvider FUserServiceProvider;

        private readonly IdentityServer FIdentityServer;
        
        public AuthenticateUserCommandHandler(DatabaseContext ADatabaseContext, ICipheringService ACipheringService, 
            IJwtUtilityService AJwtUtilityService, IDateTimeService ADateTimeService, IUserServiceProvider AUserServiceProvider, 
            IdentityServer AIdentityServer)
        {
            FDatabaseContext = ADatabaseContext;
            FCipheringService = ACipheringService;
            FJwtUtilityService = AJwtUtilityService;
            FDateTimeService = ADateTimeService;
            FUserServiceProvider = AUserServiceProvider;
            FIdentityServer = AIdentityServer;
        }

        public override async Task<AuthenticateUserCommandResult> Handle(AuthenticateUserCommand ARequest, CancellationToken ACancellationToken)
        {
            var LUsers = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);
            
            if (!LUsers.Any()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);

            var LUser = LUsers.First();
            var LIsPasswordValid = FCipheringService.VerifyPassword(ARequest.Password, LUser.CryptedPassword);

            if (!LIsPasswordValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);

            var LTokenExpires = FDateTimeService.Now.AddMinutes(FIdentityServer.WebTokenExpiresIn);
            var LUserToken = await FUserServiceProvider.GenerateUserToken(LUser, LTokenExpires, ACancellationToken);

            var LIpAddress = FUserServiceProvider.GetRequestIpAddress();
            var LRefreshToken = FJwtUtilityService.GenerateRefreshToken(LIpAddress, FIdentityServer.RefreshTokenExpiresIn);
            FUserServiceProvider.SetRefreshTokenCookie(LRefreshToken.Token, FIdentityServer.RefreshTokenExpiresIn);

            var LNewRefreshToken = new UserRefreshTokens
            {
                UserId = LUser.Id,
                Token = LRefreshToken.Token,
                Expires = LRefreshToken.Expires,
                Created = LRefreshToken.Created,
                CreatedByIp = LRefreshToken.CreatedByIp,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };

            await FUserServiceProvider.DeleteOutdatedRefreshTokens(LUser.Id, false, ACancellationToken);
            await FDatabaseContext.UserRefreshTokens.AddAsync(LNewRefreshToken, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            return new AuthenticateUserCommandResult
            {
                UserId = LUser.Id,
                AliasName = LUser.UserAlias,
                AvatarName = LUser.AvatarName,
                FirstName = LUser.FirstName,
                LastName = LUser.LastName,
                ShortBio = LUser.ShortBio,
                Registered = LUser.Registered,
                UserToken = LUserToken
            };
        }
    }
}