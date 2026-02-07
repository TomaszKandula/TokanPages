using TokanPages.Backend.Core.Utilities.LoggerService;
using MediatR;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Chat;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RemoveChatCacheCommandHandler : RequestHandler<RemoveChatCacheCommand, Unit>
{
    private readonly IChatRepository _chatRepository;

    public RemoveChatCacheCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IChatRepository chatRepository) 
        : base(operationDbContext, loggerService) => _chatRepository = chatRepository;

    public override async Task<Unit> Handle(RemoveChatCacheCommand request, CancellationToken cancellationToken)
    {
        if (request.ChatId is not null)
        {
            await _chatRepository.DeleteChatUserCacheById((Guid)request.ChatId);
        }
        else if (request.ChatKey is not null)
        {
            await _chatRepository.DeleteChatUserCacheByKey(request.ChatKey);
        }

        return Unit.Value;        
    }
}