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
        private readonly DatabaseContext _databaseContext;
        
        private readonly ICipheringService _cipheringService;

        private readonly IJwtUtilityService _jwtUtilityService;
        
        private readonly IDateTimeService _dateTimeService;

        private readonly IUserServiceProvider _userServiceProvider;

        private readonly IdentityServer _identityServer;
        
        public AuthenticateUserCommandHandler(DatabaseContext databaseContext, ICipheringService cipheringService, 
            IJwtUtilityService jwtUtilityService, IDateTimeService dateTimeService, IUserServiceProvider userServiceProvider, 
            IdentityServer identityServer)
        {
            _databaseContext = databaseContext;
            _cipheringService = cipheringService;
            _jwtUtilityService = jwtUtilityService;
            _dateTimeService = dateTimeService;
            _userServiceProvider = userServiceProvider;
            _identityServer = identityServer;
        }

        public override async Task<AuthenticateUserCommandResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var users = await _databaseContext.Users
                .Where(users => users.EmailAddress == request.EmailAddress)
                .ToListAsync(cancellationToken);
            
            if (!users.Any()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS} (1004)");

            var currentUser = users.First();
            var isPasswordValid = _cipheringService.VerifyPassword(request.Password, currentUser.CryptedPassword);

            if (!isPasswordValid)
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS} (1006)");

            if (!currentUser.IsActivated)
                throw new BusinessException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

            var currentDateTime = _dateTimeService.Now;
            var ipAddress = _userServiceProvider.GetRequestIpAddress();
            var tokenExpires = _dateTimeService.Now.AddMinutes(_identityServer.WebTokenExpiresIn);
            var userToken = await _userServiceProvider.GenerateUserToken(currentUser, tokenExpires, cancellationToken);
            var refreshToken = _jwtUtilityService.GenerateRefreshToken(ipAddress, _identityServer.RefreshTokenExpiresIn);

            _userServiceProvider.SetRefreshTokenCookie(refreshToken.Token, _identityServer.RefreshTokenExpiresIn);
            currentUser.LastLogged = currentDateTime;
            
            var newUserToken = new UserTokens
            {
                UserId = currentUser.Id,
                Token = userToken,
                Expires = tokenExpires,
                Created = currentDateTime,
                CreatedByIp = ipAddress,
                Command = nameof(AuthenticateUserCommand)
            };

            var newRefreshToken = new UserRefreshTokens
            {
                UserId = currentUser.Id,
                Token = refreshToken.Token,
                Expires = refreshToken.Expires,
                Created = refreshToken.Created,
                CreatedByIp = refreshToken.CreatedByIp,
                Revoked = null,
                RevokedByIp = null,
                ReplacedByToken = null,
                ReasonRevoked = null
            };

            await _userServiceProvider.DeleteOutdatedRefreshTokens(currentUser.Id, false, cancellationToken);
            await _databaseContext.UserTokens.AddAsync(newUserToken, cancellationToken);
            await _databaseContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var roles = await _userServiceProvider.GetUserRoles(currentUser.Id);
            var permissions = await _userServiceProvider.GetUserPermissions(currentUser.Id);

            return new AuthenticateUserCommandResult
            {
                UserId = currentUser.Id,
                AliasName = currentUser.UserAlias,
                AvatarName = currentUser.AvatarName,
                FirstName = currentUser.FirstName,
                LastName = currentUser.LastName,
                ShortBio = currentUser.ShortBio,
                Registered = currentUser.Registered,
                UserToken = userToken,
                Roles = roles,
                Permissions = permissions
            };
        }
    }
}