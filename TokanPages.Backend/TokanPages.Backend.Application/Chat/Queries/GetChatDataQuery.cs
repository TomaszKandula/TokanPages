using MediatR;

namespace TokanPages.Backend.Application.Chat.Queries;

public class GetChatDataQuery : IRequest<GetChatDataQueryResult>
{
    public string ChatKey { get; set; } = "";
}