using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace TokanPages.Services.PushNotificationService.Models.AppleNotificationService;

[ExcludeFromCodeCoverage]
public class Aps
{
    [JsonProperty("alert")]
    public Alert Alert { get; set; } = new();
}