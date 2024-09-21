using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.Services.PushNotificationService.Models.FirebaseCloudMessaging;

[ExcludeFromCodeCoverage]
public class Notification
{
    [JsonProperty("title")]
    public string Title { get; set; } = "";

    [JsonProperty("body")]
    public string Body { get; set; } = "";
}