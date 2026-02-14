using Microsoft.Extensions.Options;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommandHandler : RequestHandler<AddUserNoteCommand, AddUserNoteCommandResult>
{
    private readonly IUserService _userService;

    private readonly IUserRepository _userRepository;

    private readonly IDateTimeService _dateTimeService;

    private readonly AppSettingsModel _appSettings;

    public AddUserNoteCommandHandler(ILoggerService loggerService, IUserService userService, IDateTimeService dateTimeService, 
        IOptions<AppSettingsModel> options, IUserRepository userRepository) : base(loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _userRepository = userRepository;
        _appSettings = options.Value;
    }

    public override async Task<AddUserNoteCommandResult> Handle(AddUserNoteCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var notesCount = (await _userRepository.GetUserNotes(userId)).Count;

        var maxCount = _appSettings.UserNoteMaxCount;
        if (notesCount == maxCount)
            return new AddUserNoteCommandResult
            {
                CurrentNotes = notesCount,
                Result = Domain.Enums.UserNote.NoteRejected
            };

        var noteId = Guid.NewGuid();
        var noteDate = _dateTimeService.Now;
        var compressedNote = request.Note.CompressToBase64();
        await _userRepository.CreateUserNote(userId, compressedNote, noteId, noteDate);

        return new AddUserNoteCommandResult
        {
            Id = noteId,
            CreatedAt = noteDate,
            CreatedBy = userId,
            CurrentNotes = notesCount + 1,
            Result = Domain.Enums.UserNote.NoteAdded
        };
    }
}