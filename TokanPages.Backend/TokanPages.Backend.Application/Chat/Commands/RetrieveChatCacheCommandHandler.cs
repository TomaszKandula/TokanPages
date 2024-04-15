using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RetrieveChatCacheCommandHandler : RequestHandler<RetrieveChatCacheCommand, RetrieveChatCacheCommandResult>
{
    public RetrieveChatCacheCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService)
        : base(databaseContext, loggerService) { }

    public override async Task<RetrieveChatCacheCommandResult> Handle(RetrieveChatCacheCommand request, CancellationToken cancellationToken)
    {
        var keys = new HashSet<string>(request.ChatKey);
        var notifications = await DatabaseContext.UserMessagesCache
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