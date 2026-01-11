using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RetrieveChatCacheCommandHandler : RequestHandler<RetrieveChatCacheCommand, RetrieveChatCacheCommandResult>
{
    public RetrieveChatCacheCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService)
        : base(operationsDbContext, loggerService) { }

    public override async Task<RetrieveChatCacheCommandResult> Handle(RetrieveChatCacheCommand request, CancellationToken cancellationToken)
    {
        var keys = new HashSet<string>(request.ChatKey);
        var notifications = await OperationsDbContext.UserMessagesCache
            .AsNoTracking()
            .Where(cache => keys.Contains(cache.ChatKey))
            .Select(cache => cache.Notification)
            .ToArrayAsync(cancellationToken);

        return new RetrieveChatCacheCommandResult
        {
            Notifications = notifications
        };
    }
}