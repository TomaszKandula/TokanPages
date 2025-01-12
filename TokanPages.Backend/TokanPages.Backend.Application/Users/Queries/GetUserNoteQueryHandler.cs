using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNoteQueryHandler : RequestHandler<GetUserNoteQuery, GetUserNoteQueryResult>
{
    private readonly IUserService _userService;
    
    public GetUserNoteQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService) 
        : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<GetUserNoteQueryResult> Handle(GetUserNoteQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var note = await DatabaseContext.UserNotes
            .Where(userNote => userNote.UserId == user.Id)
            .Where(userNote => userNote.Id == request.UserNoteId)
            .Select(userNote => new GetUserNoteQueryResult
            {
                Note = userNote.Note,
                CreatedAt = userNote.CreatedAt,
                CreatedBy = userNote.CreatedBy,
                ModifiedAt = userNote.ModifiedAt,
                ModifiedBy = userNote.ModifiedBy
            })
            .SingleOrDefaultAsync(cancellationToken);

        if (note is null)
            return new GetUserNoteQueryResult();

        note.Note.DecompressFromBase64();
        return note;
    }
}