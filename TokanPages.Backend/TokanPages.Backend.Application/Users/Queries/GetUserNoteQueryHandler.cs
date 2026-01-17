using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNoteQueryHandler : RequestHandler<GetUserNoteQuery, GetUserNoteQueryResult>
{
    private readonly IUserService _userService;

    public GetUserNoteQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService) 
        : base(operationDbContext, loggerService) => _userService = userService;

    public override async Task<GetUserNoteQueryResult> Handle(GetUserNoteQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var note = await OperationDbContext.UserNotes
            .AsNoTracking()
            .Where(userNote => userNote.UserId == userId)
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

        note.Note = note.Note.DecompressFromBase64();
        return note;
    }
}