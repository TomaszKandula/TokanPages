using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserNoteCommandHandler : RequestHandler<RemoveUserNoteCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveUserNoteCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService) 
        : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveUserNoteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var userNote = await DatabaseContext.UserNotes
            .Where(note => note.Id == request.UserNoteId)
            .Where(note => note.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (userNote is null)
            throw new BusinessException(nameof(ErrorCodes.CANNOT_FIND_USER_NOTE), ErrorCodes.CANNOT_FIND_USER_NOTE);

        DatabaseContext.UserNotes.Remove(userNote);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}