namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Core.Utilities.DateTimeService;
    using Services.UserServiceProvider;
    using Services.CipheringService;
    using Identity.Authorization;
    using Shared.Resources;
    using Core.Exceptions;
    using Core.Extensions;
    using Core.Logger;
    using Database;
    using MediatR;
    using Shared;
    
    public class UpdateUserPasswordCommandHandler : TemplateHandler<UpdateUserPasswordCommand, Unit>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IUserServiceProvider _userServiceProvider;

        private readonly ICipheringService _cipheringService;

        private readonly IDateTimeService _dateTimeService;
        
        private readonly ILogger _logger;
        
        public UpdateUserPasswordCommandHandler(DatabaseContext databaseContext, IUserServiceProvider userServiceProvider, 
            ICipheringService cipheringService, IDateTimeService dateTimeService, ILogger logger)
        {
            _databaseContext = databaseContext;
            _userServiceProvider = userServiceProvider;
            _cipheringService = cipheringService;
            _dateTimeService = dateTimeService;
            _logger = logger;
        }

        public override async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
        {
            var resetId = request.ResetId != null;
            var users = await _databaseContext.Users
                .WhereIfElse(!resetId, 
                    users => users.Id == request.Id, 
                    users => users.ResetId == request.ResetId) 
                .ToListAsync(cancellationToken);

            if (!resetId)
            {
                if (!users.Any())
                    throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

                var hasRoleEverydayUser = await _userServiceProvider.HasRoleAssigned($"{Roles.EverydayUser}") ?? false;
                var hasRoleGodOfAsgard = await _userServiceProvider.HasRoleAssigned($"{Roles.GodOfAsgard}") ?? false;

                if (!hasRoleEverydayUser && !hasRoleGodOfAsgard)
                    throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
            }

            if (!users.Any())
                throw new BusinessException(nameof(ErrorCodes.INVALID_RESET_ID), ErrorCodes.INVALID_RESET_ID);

            var currentUser = users.First();
            if (resetId && _dateTimeService.Now > currentUser.ResetIdEnds)
                throw new BusinessException(nameof(ErrorCodes.EXPIRED_RESET_ID), ErrorCodes.EXPIRED_RESET_ID);
            
            var getNewSalt = _cipheringService.GenerateSalt(Constants.CipherLogRounds);
            var getHashedPassword = _cipheringService.GetHashedPassword(request.NewPassword, getNewSalt);
            
            currentUser.ResetId = null;
            currentUser.ResetIdEnds = null;
            currentUser.CryptedPassword = getHashedPassword;
            currentUser.LastUpdated = _dateTimeService.Now;
            await _databaseContext.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"User password has been updated successfully (UserId: {currentUser.Id}).");
            return await Task.FromResult(Unit.Value);
        }
    }
}