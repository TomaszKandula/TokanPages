using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserNoteQueryHandler : RequestHandler<GetUserNoteQuery, GetUserNoteQueryResult>
{
    private readonly IUserRepository _userRepository;

    private readonly IUserService _userService;

    public GetUserNoteQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, IUserRepository userRepository) 
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    public override async Task<GetUserNoteQueryResult> Handle(GetUserNoteQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var userNote = await _userRepository.GetUserNote(userId, request.UserNoteId);
        if (userNote is null)
            return new GetUserNoteQueryResult();

        return new GetUserNoteQueryResult
        {
            Id = userNote.Id,
            Note = userNote.Note.DecompressFromBase64(),
            CreatedAt = userNote.CreatedAt,
            ModifiedAt = userNote.ModifiedAt,
            CreatedBy = userNote.CreatedBy,
            ModifiedBy = userNote.ModifiedBy
        };
    }
}