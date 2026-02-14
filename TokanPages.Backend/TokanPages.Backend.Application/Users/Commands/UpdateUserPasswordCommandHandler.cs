using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.CipheringService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserPasswordCommandHandler : RequestHandler<UpdateUserPasswordCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IUserRepository _userRepository;

    private readonly ICipheringService _cipheringService;

    private readonly IDateTimeService _dateTimeService;
        
    public UpdateUserPasswordCommandHandler(ILoggerService loggerService, IUserService userService, 
        ICipheringService cipheringService, IDateTimeService dateTimeService, IUserRepository userRepository) : base(loggerService)
    {
        _userService = userService;
        _cipheringService = cipheringService;
        _dateTimeService = dateTimeService;
        _userRepository = userRepository;
    }

    public override async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = request.Id;
        if (request.Id == null && request.ResetId == null)
        {
            var activeUserId = _userService.GetLoggedUserId();
            userId = activeUserId;
        }

        var hasResetId = request.ResetId != null;
        var user = hasResetId 
            ? await _userRepository.GetUserDetailsByResetId(request.ResetId ?? Guid.Empty) 
            : await _userRepository.GetUserDetails(userId ?? Guid.Empty);

        if (user is null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        if (!hasResetId)
        {
            var hasRoleEverydayUser = await _userService.HasRoleAssigned($"{Role.EverydayUser}") ?? false;
            var hasRoleGodOfAsgard = await _userService.HasRoleAssigned($"{Role.GodOfAsgard}") ?? false;

            if (!hasRoleEverydayUser && !hasRoleGodOfAsgard)
                throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
        }

        if (request.OldPassword is not null)
        {
            var isPasswordValid = _cipheringService.VerifyPassword(request.OldPassword, user.CryptedPassword);
            if (!isPasswordValid)
            {
                LoggerService.LogError($"Cannot positively verify given password supplied by user (Id: {user.UserId}).");
                throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
            }
        }

        if (hasResetId && _dateTimeService.Now > user.ResetIdEnds)
            throw new AuthorizationException(nameof(ErrorCodes.EXPIRED_RESET_ID), ErrorCodes.EXPIRED_RESET_ID);

        var getNewSalt = _cipheringService.GenerateSalt(12);
        var getHashedPassword = _cipheringService.GetHashedPassword(request.NewPassword, getNewSalt);

        await _userRepository.UpdateUserPassword(user.UserId, getHashedPassword);

        LoggerService.LogInformation($"User password has been updated successfully (UserId: {user.UserId}).");
        return Unit.Value;
    }
}
