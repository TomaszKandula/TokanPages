using MediatR;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RetrieveChatCacheCommand : IRequest<RetrieveChatCacheCommandResult>
{
    public string[] ChatKey { get; set; } = Array.Empty<string>();
}
