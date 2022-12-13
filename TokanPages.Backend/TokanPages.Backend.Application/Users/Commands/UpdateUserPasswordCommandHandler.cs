using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.CipheringService;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserPasswordCommandHandler : RequestHandler<UpdateUserPasswordCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly ICipheringService _cipheringService;

    private readonly IDateTimeService _dateTimeService;
        
    public UpdateUserPasswordCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, ICipheringService cipheringService, 
        IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _cipheringService = cipheringService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = request.Id;
        if (request.Id == null && request.ResetId == null)
        {
            var activeUser = await _userService.GetActiveUser(cancellationToken: cancellationToken);
            userId = activeUser.Id;
        }

        var hasResetId = request.ResetId != null;
        var user = await DatabaseContext.Users
            .Where(users => users.IsActivated)
            .Where(users => !users.IsDeleted)
            .WhereIfElse(!hasResetId, 
                users => users.Id == userId, 
                users => users.ResetId == request.ResetId) 
            .SingleOrDefaultAsync(cancellationToken);

        if (!hasResetId)
        {
            if (user is null)
                throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            var hasRoleEverydayUser = await _userService.HasRoleAssigned($"{Roles.EverydayUser}", cancellationToken: cancellationToken) ?? false;
            var hasRoleGodOfAsgard = await _userService.HasRoleAssigned($"{Roles.GodOfAsgard}", cancellationToken: cancellationToken) ?? false;

            if (!hasRoleEverydayUser && !hasRoleGodOfAsgard)
                throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
        }

        if (user is null)
            throw new AuthorizationException(nameof(ErrorCodes.INVALID_RESET_ID), ErrorCodes.INVALID_RESET_ID);

        if (request.OldPassword is not null)
        {
            var isPasswordValid = _cipheringService.VerifyPassword(request.OldPassword, user.CryptedPassword);
            if (!isPasswordValid)
            {
                LoggerService.LogError($"Cannot positively verify given password supplied by user (Id: {user.Id}).");
                throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
            }
        }

        if (hasResetId && _dateTimeService.Now > user.ResetIdEnds)
            throw new AuthorizationException(nameof(ErrorCodes.EXPIRED_RESET_ID), ErrorCodes.EXPIRED_RESET_ID);

        var getNewSalt = _cipheringService.GenerateSalt(12);
        var getHashedPassword = _cipheringService.GetHashedPassword(request.NewPassword!, getNewSalt);

        user.ResetId = null;
        user.ResetIdEnds = null;
        user.CryptedPassword = getHashedPassword;
        user.ModifiedAt = _dateTimeService.Now;
        user.ModifiedBy = user.Id;
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        LoggerService.LogInformation($"User password has been updated successfully (UserId: {user.Id}).");
        return Unit.Value;
    }
}
