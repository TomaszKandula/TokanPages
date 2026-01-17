using TokanPages.Backend.Core.Utilities.LoggerService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RemoveChatCacheCommandHandler : RequestHandler<RemoveChatCacheCommand, Unit>
{
    public RemoveChatCacheCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService) : base(operationDbContext, loggerService) { }

    public override async Task<Unit> Handle(RemoveChatCacheCommand request, CancellationToken cancellationToken)
    {
        if (request.ChatId is not null)
        {
            var cache = await OperationDbContext.UserMessagesCache
                .Where(cache => cache.Id == request.ChatId)
                .SingleOrDefaultAsync(cancellationToken);

            if (cache is null)
                return Unit.Value;

            OperationDbContext.Remove(cache);
            await OperationDbContext.SaveChangesAsync(cancellationToken);
        }
        else if (request.ChatKey is not null)
        {
            var cacheList = await OperationDbContext.UserMessagesCache
                .Where(cache => cache.ChatKey == request.ChatKey)
                .ToListAsync(cancellationToken);

            if (cacheList.Count == 0)
                return Unit.Value;

            OperationDbContext.RemoveRange(cacheList);
            await OperationDbContext.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;        
    }
}