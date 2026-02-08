using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserNoteCommandHandler : RequestHandler<RemoveUserNoteCommand, Unit>
{
    private readonly IUserService _userService;

    public RemoveUserNoteCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService) 
        : base(operationDbContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(RemoveUserNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var userNote = await OperationDbContext.UserNotes
            .Where(note => note.Id == request.Id)
            .Where(note => note.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (userNote is null)
            throw new BusinessException(nameof(ErrorCodes.CANNOT_FIND_USER_NOTE), ErrorCodes.CANNOT_FIND_USER_NOTE);

        OperationDbContext.UserNotes.Remove(userNote);
        await OperationDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}