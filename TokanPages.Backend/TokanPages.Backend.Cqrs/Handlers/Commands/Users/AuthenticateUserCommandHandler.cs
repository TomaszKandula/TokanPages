namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Security.Claims;
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
        private const int TOKEN_EXPIRES_IN = 60;
        
        private const int REFRESH_TOKEN_EXPIRES_IN = 90; 
        
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
            var LEmailCollection = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);
            
            if (!LEmailCollection.Any()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);

            var LUserData = LEmailCollection.First();
            var LIsPasswordValid = FCipheringService.VerifyPassword(ARequest.Password, LUserData.CryptedPassword);

            if (!LIsPasswordValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);

            var LTokenExpires = FDateTimeService.Now.AddMinutes(TOKEN_EXPIRES_IN);
            var LGetValidClaims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, LUserData.UserAlias),
                new Claim(ClaimTypes.Role, nameof(Identity.Authorization.Roles.EverydayUser)),
                new Claim(ClaimTypes.NameIdentifier, LUserData.Id.ToString()),
                new Claim(ClaimTypes.GivenName, LUserData.FirstName),
                new Claim(ClaimTypes.Surname, LUserData.LastName),
                new Claim(ClaimTypes.Email, LUserData.EmailAddress)
            });

            var LJwt = FJwtUtilityService.GenerateJwt(
                LTokenExpires, 
                LGetValidClaims, 
                FIdentityServer.WebSecret, 
                FIdentityServer.Issuer, 
                FIdentityServer.Audience);

            var LRefreshToken = FJwtUtilityService.GenerateRefreshToken(
                FUserServiceProvider.GetRequestIpAddress(), REFRESH_TOKEN_EXPIRES_IN);
            FUserServiceProvider.SetRefreshTokenCookie(LRefreshToken.Token, REFRESH_TOKEN_EXPIRES_IN);

            var LNewRefreshToken = new UserRefreshTokens
            {
                UserId = LUserData.Id,
                Token = LRefreshToken.Token,
                Expires = LRefreshToken.Expires,
                Created = LRefreshToken.Created,
                CreatedByIp = LRefreshToken.CreatedByIp,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };
            
            await FDatabaseContext.UserRefreshTokens.AddAsync(LNewRefreshToken, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            return new AuthenticateUserCommandResult
            {
                UserId = LUserData.Id,
                AliasName = LUserData.UserAlias,
                AvatarName = LUserData.AvatarName,
                FirstName = LUserData.FirstName,
                LastName = LUserData.LastName,
                ShortBio = LUserData.ShortBio,
                Registered = LUserData.Registered,
                Jwt = LJwt
            };
        }
    }
}