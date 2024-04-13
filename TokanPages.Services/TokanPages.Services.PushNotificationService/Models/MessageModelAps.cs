using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PushNotificationService.Models.AppleNotificationService;
using Newtonsoft.Json;

namespace TokanPages.Services.PushNotificationService.Models;

[ExcludeFromCodeCoverage]
public class MessageModelAps
{
    [JsonProperty("aps")]
    public Aps Aps { get; set; } = new();
}