using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommandHandler : RequestHandler<AddUserNoteCommand, AddUserNoteCommandResult>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IConfiguration _configuration;

    public AddUserNoteCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService, IConfiguration configuration) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _configuration = configuration;
    }

    public override async Task<AddUserNoteCommandResult> Handle(AddUserNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var notesCount = await DatabaseContext.UserNotes
            .AsNoTracking()
            .Where(note => note.UserId == userId)
            .CountAsync(cancellationToken);

        var maxCount = _configuration.GetValue<int>("UserNote_MaxCount");
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

        await DatabaseContext.UserNotes.AddAsync(userNote, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

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