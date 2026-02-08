using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNotesQueryHandler : RequestHandler<GetUserNotesQuery, GetUserNotesQueryResult>
{
    private readonly IUserService _userService;

    public GetUserNotesQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService) 
        : base(operationDbContext, loggerService) => _userService = userService;

    public override async Task<GetUserNotesQueryResult> Handle(GetUserNotesQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var notes = await OperationDbContext.UserNotes
            .AsNoTracking()
            .Where(note => note.UserId == userId)
            .ToListAsync(cancellationToken);

        if (notes.Count == 0)
            return new GetUserNotesQueryResult();

        var result = new List<GetUserNoteQueryResult>();
        foreach (var note in notes)
        {
            result.Add(new GetUserNoteQueryResult
            {
                Id = note.Id,
                Note = note.Note.DecompressFromBase64(),
                CreatedAt = note.CreatedAt,
                CreatedBy = note.CreatedBy,
                ModifiedAt = note.ModifiedAt,
                ModifiedBy = note.ModifiedBy
            });
        }

        return new GetUserNotesQueryResult
        {
            Notes = result
        };
    }
}