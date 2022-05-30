namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;
using MediatR;
using Database;
using Domain.Enums;
using Core.Exceptions;
using Core.Extensions;
using Shared.Resources;
using Services.UserService;
using Services.CipheringService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;

public class UpdateUserPasswordCommandHandler : Cqrs.RequestHandler<UpdateUserPasswordCommand, Unit>
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
        var resetId = request.ResetId != null;
        var userId = request.Id ?? await _userService.GetUserId(cancellationToken) ?? Guid.Empty;
        if (userId == Guid.Empty)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var currentUser = await DatabaseContext.Users
            .Where(users => users.IsActivated)
            .Where(users => !users.IsDeleted)
            .WhereIfElse(!resetId, 
                users => users.Id == userId, 
                users => users.ResetId == request.ResetId) 
            .SingleOrDefaultAsync(cancellationToken);

        if (!resetId)
        {
            if (currentUser is null)
                throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

            var hasRoleEverydayUser = await _userService.HasRoleAssigned($"{Roles.EverydayUser}", cancellationToken) ?? false;
            var hasRoleGodOfAsgard = await _userService.HasRoleAssigned($"{Roles.GodOfAsgard}", cancellationToken) ?? false;

            if (!hasRoleEverydayUser && !hasRoleGodOfAsgard)
                throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
        }

        if (currentUser is null)
            throw new AuthorizationException(nameof(ErrorCodes.INVALID_RESET_ID), ErrorCodes.INVALID_RESET_ID);

        if (request.OldPassword is not null)
        {
            var isPasswordValid = _cipheringService.VerifyPassword(request.OldPassword, currentUser.CryptedPassword);
            if (!isPasswordValid)
            {
                LoggerService.LogError($"Cannot positively verify given password supplied by user (Id: {userId}).");
                throw new AccessException(nameof(ErrorCodes.INVALID_CREDENTIALS), $"{ErrorCodes.INVALID_CREDENTIALS}");
            }
        }

        if (resetId && _dateTimeService.Now > currentUser.ResetIdEnds)
            throw new AuthorizationException(nameof(ErrorCodes.EXPIRED_RESET_ID), ErrorCodes.EXPIRED_RESET_ID);

        var getNewSalt = _cipheringService.GenerateSalt(Constants.CipherLogRounds);
        var getHashedPassword = _cipheringService.GetHashedPassword(request.NewPassword, getNewSalt);

        currentUser.ResetId = null;
        currentUser.ResetIdEnds = null;
        currentUser.CryptedPassword = getHashedPassword;
        currentUser.ModifiedAt = _dateTimeService.Now;
        currentUser.ModifiedBy = userId;
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        LoggerService.LogInformation($"User password has been updated successfully (UserId: {userId}).");
        return Unit.Value;
    }
}
