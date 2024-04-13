using Newtonsoft.Json;

namespace TokanPages.Services.PushNotificationService.Models.FirebaseCloudMessaging;

public class Notification
{
    [JsonProperty("title")]
    public string Title { get; set; } = "";

    [JsonProperty("body")]
    public string Body { get; set; } = "";
}