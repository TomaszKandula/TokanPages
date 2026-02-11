using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;

namespace TokanPages.Backend.Application.Users.Commands;

public class ActivateUserCommandHandler : RequestHandler<ActivateUserCommand, ActivateUserCommandResult>
{
    private readonly IUserRepository _userRepository;

    private readonly IDateTimeService _dateTimeService;

    public ActivateUserCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IDateTimeService dateTimeService, IUserRepository userRepository) : base(operationDbContext, loggerService)
    {
        _dateTimeService = dateTimeService;
        _userRepository = userRepository;
    }

    public override async Task<ActivateUserCommandResult> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var userDetails = await _userRepository.GetUserDetailsByActivationId(request.ActivationId);
        if (userDetails is null)
            throw new BusinessException(nameof(ErrorCodes.INVALID_ACTIVATION_ID), ErrorCodes.INVALID_ACTIVATION_ID);

        if (userDetails.ActivationIdEnds < _dateTimeService.Now)
            throw new BusinessException(nameof(ErrorCodes.EXPIRED_ACTIVATION_ID), ErrorCodes.EXPIRED_ACTIVATION_ID);

        await _userRepository.ActivateUser(userDetails.UserId);
        if (userDetails.HasBusinessLock)
            LoggerService.LogWarning("The user is activated but has the business lock and thus will require administrator action.");

        LoggerService.LogInformation($"User account has been activated, user ID: {userDetails.UserId}");
        return new ActivateUserCommandResult
        {
            UserId = userDetails.UserId,
            HasBusinessLock = userDetails.HasBusinessLock
        };
    }
}