namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Domain.Entities;
    using Core.Exceptions;
    using Shared.Services;
    using Shared.Resources;
    using Services.CipheringService;
    using Services.UserServiceProvider;
    using Core.Utilities.LoggerService;
    using Core.Utilities.DateTimeService;
    using Identity.Services.JwtUtilityService;

    public class AuthenticateUserCommandHandler : TemplateHandler<AuthenticateUserCommand, AuthenticateUserCommandResult>
    {
        private readonly ICipheringService _cipheringService;

        private readonly IJwtUtilityService _jwtUtilityService;
        
        private readonly IDateTimeService _dateTimeService;

        private readonly IUserServiceProvider _userServiceProvider;

        private readonly IApplicationSettings _applicationSettings;
        
        public AuthenticateUserCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, ICipheringService cipheringService, 
            IJwtUtilityService jwtUtilityService, IDateTimeService dateTimeService, IUserServiceProvider userServiceProvider, 
            IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
        {
            _cipheringService = cipheringService;
            _jwtUtilityService = jwtUtilityService;
            _dateTimeService = dateTimeService;
            _userServiceProvider = userServiceProvider;
            _applicationSettings = applicationSettings;
        }

        public override async Task<AuthenticateUserCommandResult> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            var users = await DatabaseContext.Users
                .Where(users => users.EmailAddress == request.EmailAddress)
                .ToListAsync(cancellationToken);
            
            if (!users.Any())
            {
                LoggerService.LogError($"Cannot find user with given email address: '{request.EmailAddress}'.");
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
            }
            
            var currentUser = users.First();
            var isPasswordValid = _cipheringService.VerifyPassword(request.Password, currentUser.CryptedPassword);

            if (!isPasswordValid)
            {
                LoggerService.LogError($"Cannot positively verify given password supplied by user (Id: {currentUser.Id}).");
                throw new BusinessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
            }
            
            if (!currentUser.IsActivated)
                throw new BusinessException(nameof(ErrorCodes.USER_ACCOUNT_INACTIVE), ErrorCodes.USER_ACCOUNT_INACTIVE);

            var currentDateTime = _dateTimeService.Now;
            var ipAddress = _userServiceProvider.GetRequestIpAddress();
            var tokenExpires = _dateTimeService.Now.AddMinutes(_applicationSettings.IdentityServer.WebTokenExpiresIn);
            var userToken = await _userServiceProvider.GenerateUserToken(currentUser, tokenExpires, cancellationToken);
            var refreshToken = _jwtUtilityService.GenerateRefreshToken(ipAddress, _applicationSettings.IdentityServer.RefreshTokenExpiresIn);

            _userServiceProvider.SetRefreshTokenCookie(refreshToken.Token, _applicationSettings.IdentityServer.RefreshTokenExpiresIn);
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
            await DatabaseContext.UserTokens.AddAsync(newUserToken, cancellationToken);
            await DatabaseContext.UserRefreshTokens.AddAsync(newRefreshToken, cancellationToken);
            await DatabaseContext.SaveChangesAsync(cancellationToken);

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