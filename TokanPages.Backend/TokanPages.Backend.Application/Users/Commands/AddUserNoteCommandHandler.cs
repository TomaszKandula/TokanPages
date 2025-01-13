using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserNoteCommandHandler : RequestHandler<AddUserNoteCommand, Unit>
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

    public override async Task<Unit> Handle(AddUserNoteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var notes = await DatabaseContext.UserNotes
            .AsNoTracking()
            .Where(note => note.UserId == user.Id)
            .CountAsync(cancellationToken);

        var maxCount = _configuration.GetValue<int>("UserNote_MaxCount");
        if (notes == maxCount)
            return Unit.Value;

        var compressedNote = request.Note.CompressToBase64();
        var userNote = new UserNote
        {
            Note = compressedNote,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = user.Id
        };

        await DatabaseContext.UserNotes.AddAsync(userNote, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}