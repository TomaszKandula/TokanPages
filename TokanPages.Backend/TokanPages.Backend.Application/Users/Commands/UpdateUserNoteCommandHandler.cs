using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserNoteCommandHandler : RequestHandler<UpdateUserNoteCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IUserRepository _userRepository;

    public UpdateUserNoteCommandHandler(ILoggerService loggerService, IUserService userService, 
        IUserRepository userRepository) : base(loggerService)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public override async Task<Unit> Handle(UpdateUserNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var hasNote = await _userRepository.GetUserNote(userId, request.Id) != null;
        if (!hasNote)
            throw new BusinessException(nameof(ErrorCodes.CANNOT_FIND_USER_NOTE), ErrorCodes.CANNOT_FIND_USER_NOTE);

        var note = request.Note.CompressToBase64();
        await _userRepository.UpdateUserNote(userId, note);

        return Unit.Value;
    }
}