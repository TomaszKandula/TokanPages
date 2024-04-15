using MediatR;

namespace TokanPages.Backend.Application.Chat.Commands;

public class PostChatMessageCommand : IRequest<PostChatMessageCommandResult>
{
    public string ChatKey { get; set; } = "";

    public string Message { get; set; } = "";
}