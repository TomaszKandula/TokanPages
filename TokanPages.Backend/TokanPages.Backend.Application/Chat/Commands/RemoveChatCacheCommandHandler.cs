using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RemoveChatCacheCommandHandler : RequestHandler<RemoveChatCacheCommand, Unit>
{
    public RemoveChatCacheCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService) : base(databaseContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveChatCacheCommand request, CancellationToken cancellationToken)
    {
        if (request.ChatId is not null)
        {
            var cache = await DatabaseContext.UserMessageCache
                .Where(cache => cache.Id == request.ChatId)
                .SingleOrDefaultAsync(cancellationToken);

            if (cache is null)
                return Unit.Value;

            DatabaseContext.Remove(cache);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }
        else if (request.ChatKey is not null)
        {
            var cache = await DatabaseContext.UserMessageCache
                .Where(cache => cache.ChatKey == request.ChatKey)
                .ToListAsync(cancellationToken);

            if (cache is null)
                return Unit.Value;

            DatabaseContext.RemoveRange(cache);
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;        
    }
}