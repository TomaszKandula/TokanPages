using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetAllUserNotesQueryHandler : RequestHandler<GetAllUserNotesQuery, GetAllUserNotesQueryResult>
{
    private readonly IUserService _userService;

    public GetAllUserNotesQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService) 
        : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<GetAllUserNotesQueryResult> Handle(GetAllUserNotesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var notes = await DatabaseContext.UserNotes
            .AsNoTracking()
            .Where(note => note.UserId == user.Id)
            .ToListAsync(cancellationToken);

        if (notes.Count == 0)
            return new GetAllUserNotesQueryResult();

        var result = new List<GetUserNoteQueryResult>();
        foreach (var note in notes)
        {
            result.Add(new GetUserNoteQueryResult
            {
                Note = note.Note.DecompressFromBase64(),
                CreatedAt = note.CreatedAt,
                CreatedBy = note.CreatedBy,
                ModifiedAt = note.ModifiedAt,
                ModifiedBy = note.ModifiedBy
            });
        }

        return new GetAllUserNotesQueryResult
        {
            UserNotes = result
        };
    }
}