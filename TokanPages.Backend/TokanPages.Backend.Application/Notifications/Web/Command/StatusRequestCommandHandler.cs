using TokanPages.Backend.Application.Notifications.Web.Models.Base;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application.Notifications.Web.Command;

public class StatusRequestCommandHandler : RequestHandler<StatusRequestCommand, StatusRequestCommandResult>
{
    private readonly IJsonSerializer _jsonSerializer;

    public StatusRequestCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IJsonSerializer jsonSerializer) : base(operationDbContext, loggerService) => _jsonSerializer = jsonSerializer;

    public override async Task<StatusRequestCommandResult> Handle(StatusRequestCommand request, CancellationToken cancellationToken)
    {
        var webNotification = await OperationDbContext.WebNotifications
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

        OperationDbContext.Remove(webNotification);
        await OperationDbContext.SaveChangesAsync(cancellationToken);
        return result;
    }
}