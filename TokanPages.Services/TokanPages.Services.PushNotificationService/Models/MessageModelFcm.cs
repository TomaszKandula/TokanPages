using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.PushNotificationService.Models.FirebaseCloudMessaging;
using Newtonsoft.Json;

namespace TokanPages.Services.PushNotificationService.Models;

[ExcludeFromCodeCoverage]
public class MessageModelFcm
{
    [JsonProperty("notification")] 
    public Notification Notification { get; set; } = new();
}