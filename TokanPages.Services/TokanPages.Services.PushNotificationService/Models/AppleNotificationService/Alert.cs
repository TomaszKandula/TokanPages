using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.Services.PushNotificationService.Models.AppleNotificationService;

[ExcludeFromCodeCoverage]
public class Alert
{
    [JsonProperty("title")]
    public string Title { get; set; } = "";

    [JsonProperty("body")]
    public string Body { get; set; } = "";
}