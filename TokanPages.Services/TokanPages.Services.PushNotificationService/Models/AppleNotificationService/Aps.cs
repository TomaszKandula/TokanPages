using Newtonsoft.Json;

namespace TokanPages.Services.PushNotificationService.Models.AppleNotificationService;

public class Aps
{
    [JsonProperty("alert")]
    public Alert Alert { get; set; } = new();
}