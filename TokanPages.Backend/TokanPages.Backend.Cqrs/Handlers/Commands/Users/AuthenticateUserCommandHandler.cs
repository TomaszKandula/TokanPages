namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System;
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
            var LUsersList = await FDatabaseContext.Users
                .AsNoTracking()
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);
            
            if (!LUsersList.Any()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);

            var LUserData = LUsersList.First();
            var LIsPasswordValid = FCipheringService.VerifyPassword(ARequest.Password, LUserData.CryptedPassword);

            if (!LIsPasswordValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), ErrorCodes.INVALID_CREDENTIALS);

            var LUserRoles = await FDatabaseContext.UserRoles
                .AsNoTracking()
                .Include(AUserRole => AUserRole.User)
                .Include(AUserRole => AUserRole.Role)
                .Where(AUserRole => AUserRole.UserId == LUserData.Id)
                .ToListAsync(ACancellationToken);

            var LTokenExpires = FDateTimeService.Now.AddMinutes(TOKEN_EXPIRES_IN);
            var LGetValidClaims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, LUserData.UserAlias),
                new Claim(ClaimTypes.NameIdentifier, LUserData.Id.ToString()),
                new Claim(ClaimTypes.GivenName, LUserData.FirstName),
                new Claim(ClaimTypes.Surname, LUserData.LastName),
                new Claim(ClaimTypes.Email, LUserData.EmailAddress)
            });

            LGetValidClaims.AddClaims(LUserRoles
                .Select(AUserRole => new Claim(ClaimTypes.Role, AUserRole.Role.Name)));
            
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

            await DeletePreviousRefreshTokens(LUserData.Id, ACancellationToken);
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
        
        private async Task DeletePreviousRefreshTokens(Guid AUserId, CancellationToken ACancellationToken)
        {
            var LRefreshTokens = await FDatabaseContext.UserRefreshTokens
                .Where(ATokens => ATokens.UserId == AUserId 
                    && ATokens.Expires <= FDateTimeService.Now 
                    && ATokens.Created <= FDateTimeService.Now
                    && ATokens.Revoked == null)
                .ToListAsync(ACancellationToken);

            FDatabaseContext.UserRefreshTokens.RemoveRange(LRefreshTokens);
        }        
    }
}