namespace TokanPages.Backend.Application.Chat.Models;

public class BaseChatItem
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTime DateTime { get; set; }

    public string Text { get; set; } = "";

    public string ChatKey { get; set; } = "";
}