using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PushNotificationService.Models;

[ExcludeFromCodeCoverage]
public class MessageInput
{
    public string Title { get; set; } = "";

    public string Body { get; set; } = "";
}