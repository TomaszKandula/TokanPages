using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserCommandHandler : RequestHandler<RemoveUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    private readonly IUserService _userService;

    public RemoveUserCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IUserRepository userRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public override async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        if (request.IsSoftDelete)
        {
            LoggerService.LogInformation($"Removing user account (user ID: {userId}). You can undo this operation at any time.");
            await _userRepository.UserSoftDelete(userId);
        }
        else
        {
            LoggerService.LogInformation($"Removing permanently user account (user ID: {userId}). You cannot undo this operation.");
            await _userRepository.UserHardDelete(userId);
        }

        LoggerService.LogInformation($"User account (user ID: {userId}) has been removed");
        return Unit.Value;
    }
}