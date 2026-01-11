using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class UpdateUserNoteCommandHandler : RequestHandler<UpdateUserNoteCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    public UpdateUserNoteCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateUserNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var note = await DatabaseContext.UserNotes
            .Where(userNote => userNote.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (note is null)
            throw new BusinessException(nameof(ErrorCodes.CANNOT_FIND_USER_NOTE), ErrorCodes.CANNOT_FIND_USER_NOTE);

        note.Note = request.Note.CompressToBase64();
        note.ModifiedAt = _dateTimeService.Now;
        note.ModifiedBy = userId;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}