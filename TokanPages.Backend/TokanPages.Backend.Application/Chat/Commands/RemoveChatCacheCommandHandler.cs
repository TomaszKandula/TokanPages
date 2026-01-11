using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RemoveChatCacheCommandHandler : RequestHandler<RemoveChatCacheCommand, Unit>
{
    public RemoveChatCacheCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService) : base(operationsDbContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveChatCacheCommand request, CancellationToken cancellationToken)
    {
        if (request.ChatId is not null)
        {
            var cache = await OperationsDbContext.UserMessagesCache
                .Where(cache => cache.Id == request.ChatId)
                .SingleOrDefaultAsync(cancellationToken);

            if (cache is null)
                return Unit.Value;

            OperationsDbContext.Remove(cache);
            await OperationsDbContext.SaveChangesAsync(cancellationToken);
        }
        else if (request.ChatKey is not null)
        {
            var cacheList = await OperationsDbContext.UserMessagesCache
                .Where(cache => cache.ChatKey == request.ChatKey)
                .ToListAsync(cancellationToken);

            if (cacheList.Count == 0)
                return Unit.Value;

            OperationsDbContext.RemoveRange(cacheList);
            await OperationsDbContext.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;        
    }
}