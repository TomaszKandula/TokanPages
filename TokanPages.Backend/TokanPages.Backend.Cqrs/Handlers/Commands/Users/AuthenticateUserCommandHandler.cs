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
    using Core.Utilities.DateTimeService;
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
                .Where(AUsers => AUsers.EmailAddress == ARequest.EmailAddress)
                .ToListAsync(ACancellationToken);
            
            if (!LUsers.Any()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS} (1004)");

            var LCurrentUser = LUsers.First();
            var LIsPasswordValid = FCipheringService.VerifyPassword(ARequest.Password, LCurrentUser.CryptedPassword);

            if (!LIsPasswordValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS} (1006)");

            if (!LCurrentUser.IsActivated)
                throw new BusinessException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

            var LCurrentDateTime = FDateTimeService.Now;
            var LIpAddress = FUserServiceProvider.GetRequestIpAddress();
            var LTokenExpires = FDateTimeService.Now.AddMinutes(FIdentityServer.WebTokenExpiresIn);
            var LUserToken = await FUserServiceProvider.GenerateUserToken(LCurrentUser, LTokenExpires, ACancellationToken);
            var LRefreshToken = FJwtUtilityService.GenerateRefreshToken(LIpAddress, FIdentityServer.RefreshTokenExpiresIn);

            FUserServiceProvider.SetRefreshTokenCookie(LRefreshToken.Token, FIdentityServer.RefreshTokenExpiresIn);
            LCurrentUser.LastLogged = LCurrentDateTime;
            
            var LNewUserToken = new UserTokens
            {
                UserId = LCurrentUser.Id,
                Token = LUserToken,
                Expires = LTokenExpires,
                Created = LCurrentDateTime,
                CreatedByIp = LIpAddress,
                Command = nameof(AuthenticateUserCommand)
            };

            var LNewRefreshToken = new UserRefreshTokens
            {
                UserId = LCurrentUser.Id,
                Token = LRefreshToken.Token,
                Expires = LRefreshToken.Expires,
                Created = LRefreshToken.Created,
                CreatedByIp = LRefreshToken.CreatedByIp,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };

            await FUserServiceProvider.DeleteOutdatedRefreshTokens(LCurrentUser.Id, false, ACancellationToken);
            await FDatabaseContext.UserTokens.AddAsync(LNewUserToken, ACancellationToken);
            await FDatabaseContext.UserRefreshTokens.AddAsync(LNewRefreshToken, ACancellationToken);
            await FDatabaseContext.SaveChangesAsync(ACancellationToken);

            var LRoles = await FUserServiceProvider.GetUserRoles(LCurrentUser.Id);
            var LPermissions = await FUserServiceProvider.GetUserPermissions(LCurrentUser.Id);

            return new AuthenticateUserCommandResult
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
    }
}