using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Chat;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RetrieveChatCacheCommandHandler : RequestHandler<RetrieveChatCacheCommand, RetrieveChatCacheCommandResult>
{
    private readonly IChatRepository _chatRepository;
    
    public RetrieveChatCacheCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IChatRepository chatRepository)
        : base(operationDbContext, loggerService) => _chatRepository = chatRepository;

    public override async Task<RetrieveChatCacheCommandResult> Handle(RetrieveChatCacheCommand request, CancellationToken cancellationToken)
    {
        var notifications = await _chatRepository.GetChatCache(request.ChatKey);
        if (notifications.Length == 0)
            return new RetrieveChatCacheCommandResult();

        return new RetrieveChatCacheCommandResult
        {
            Notifications = notifications
        };
    }
}