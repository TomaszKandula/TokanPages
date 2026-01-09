using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserFileCommandHandler : RequestHandler<AddUserFileCommand, AddUserFileCommandResult>
{
    public AddUserFileCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<AddUserFileCommandResult> Handle(AddUserFileCommand request, CancellationToken cancellationToken)
    {
        //TODO: add implementation
        throw new NotImplementedException();
    }
}