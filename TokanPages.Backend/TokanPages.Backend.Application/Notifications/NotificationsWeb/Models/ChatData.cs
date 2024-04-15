namespace TokanPages.Backend.Application.NotificationsWeb.Models;

public class ChatData
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string? Initials { get; set; }

    public string? AvatarName { get; set; }

    public DateTime DateTime { get; set; }

    public string Text { get; set; } = "";

    public string ChatKey { get; set; } = "";
}