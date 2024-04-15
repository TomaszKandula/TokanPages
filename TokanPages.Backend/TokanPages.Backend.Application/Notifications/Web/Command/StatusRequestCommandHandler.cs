using TokanPages.Backend.Application.Notifications.Web.Models.Base;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Notifications.Web.Command;

public class StatusRequestCommandHandler : RequestHandler<StatusRequestCommand, StatusRequestCommandResult>
{
    private readonly IJsonSerializer _jsonSerializer;

    public StatusRequestCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IJsonSerializer jsonSerializer) : base(databaseContext, loggerService) => _jsonSerializer = jsonSerializer;

    public override async Task<StatusRequestCommandResult> Handle(StatusRequestCommand request, CancellationToken cancellationToken)
    {
        var webNotification = await DatabaseContext.WebNotifications
            .Where(notifications => notifications.Id == request.StatusId)
            .SingleOrDefaultAsync(cancellationToken);

        if (webNotification is null)
            return new StatusRequestCommandResult();

        var data = _jsonSerializer.Deserialize<StatusBase>(webNotification.Value);
        var result = new StatusRequestCommandResult
        {
            UserId = data.UserId,
            Handler = data.Handler,
            Payload = data.Payload
        };

        DatabaseContext.Remove(webNotification);
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}