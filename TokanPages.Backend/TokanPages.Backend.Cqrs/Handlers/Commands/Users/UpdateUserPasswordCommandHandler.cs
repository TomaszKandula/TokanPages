namespace TokanPages.Backend.Cqrs.Handlers.Commands.Users;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;
using MediatR;
using Database;
using Core.Exceptions;
using Core.Extensions;
using Shared.Resources;
using Identity.Authorization;
using Services.CipheringService;
using Core.Utilities.LoggerService;
using Services.UserServiceProvider;
using Core.Utilities.DateTimeService;

public class UpdateUserPasswordCommandHandler : Cqrs.RequestHandler<UpdateUserPasswordCommand, Unit>
{
    private readonly IUserServiceProvider _userServiceProvider;

    private readonly ICipheringService _cipheringService;

    private readonly IDateTimeService _dateTimeService;
        
    public UpdateUserPasswordCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserServiceProvider userServiceProvider, ICipheringService cipheringService, 
        IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _userServiceProvider = userServiceProvider;
        _cipheringService = cipheringService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var resetId = request.ResetId != null;
        var users = await DatabaseContext.Users
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
                throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);
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
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        LoggerService.LogInformation($"User password has been updated successfully (UserId: {currentUser.Id}).");
        return Unit.Value;
    }
}