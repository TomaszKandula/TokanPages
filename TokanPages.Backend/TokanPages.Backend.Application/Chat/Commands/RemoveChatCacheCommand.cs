using MediatR;

namespace TokanPages.Backend.Application.Chat.Commands;

public class RemoveChatCacheCommand : IRequest<Unit>
{
    public string? ChatKey { get; set; }

    public Guid? ChatId { get; set; }
}