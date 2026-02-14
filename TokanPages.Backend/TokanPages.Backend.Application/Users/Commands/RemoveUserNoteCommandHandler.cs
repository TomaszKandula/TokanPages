using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserNoteCommandHandler : RequestHandler<RemoveUserNoteCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    private readonly IUserService _userService;

    public RemoveUserNoteCommandHandler(ILoggerService loggerService, IUserService userService,
        IUserRepository userRepository) : base(loggerService)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public override async Task<Unit> Handle(RemoveUserNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var userNote = await _userRepository.GetUserNote(userId, request.Id);
        if (userNote is null)
            throw new BusinessException(nameof(ErrorCodes.CANNOT_FIND_USER_NOTE), ErrorCodes.CANNOT_FIND_USER_NOTE);

        await _userRepository.RemoveUserNote(userId, request.Id);
        return Unit.Value;
    }
}