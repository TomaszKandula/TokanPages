using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PushNotificationService.Models;

[ExcludeFromCodeCoverage]
public class TokenInput : ConnectionStringData
{
    public string Uri { get; set; } = "";

    public int MinUntilExpire { get; set; }
}