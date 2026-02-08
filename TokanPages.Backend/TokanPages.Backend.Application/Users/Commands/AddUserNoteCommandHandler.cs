using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommandHandler : RequestHandler<AddUserNoteCommand, AddUserNoteCommandResult>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly AppSettingsModel _appSettings;

    public AddUserNoteCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService, IOptions<AppSettingsModel> options) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _appSettings = options.Value;
    }

    public override async Task<AddUserNoteCommandResult> Handle(AddUserNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var notesCount = await OperationDbContext.UserNotes
            .AsNoTracking()
            .Where(note => note.UserId == userId)
            .CountAsync(cancellationToken);

        var maxCount = _appSettings.UserNoteMaxCount;
        if (notesCount == maxCount)
            return new AddUserNoteCommandResult
            {
                CurrentNotes = notesCount,
                Result = Domain.Enums.UserNote.NoteRejected
            };

        var compressedNote = request.Note.CompressToBase64();
        var noteId = Guid.NewGuid();
        var userNote = new UserNote
        {
            Id = noteId,
            UserId = userId,
            Note = compressedNote,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = userId
        };

        await OperationDbContext.UserNotes.AddAsync(userNote, cancellationToken);
        await OperationDbContext.SaveChangesAsync(cancellationToken);

        return new AddUserNoteCommandResult
        {
            Id = noteId,
            CreatedAt = userNote.CreatedAt,
            CreatedBy = userNote.CreatedBy,
            CurrentNotes = notesCount + 1,
            Result = Domain.Enums.UserNote.NoteAdded
        };
    }
}