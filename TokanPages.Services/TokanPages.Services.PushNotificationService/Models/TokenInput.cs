namespace TokanPages.Services.PushNotificationService.Models;

public class TokenInput : ConnectionStringData
{
    public string Uri { get; set; } = "";

    public int MinUntilExpire { get; set; }
}