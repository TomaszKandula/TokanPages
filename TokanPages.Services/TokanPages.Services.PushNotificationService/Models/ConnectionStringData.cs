using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PushNotificationService.Models;

[ExcludeFromCodeCoverage]
public class ConnectionStringData
{
    public string Endpoint { get; set; } = "";

    public string SasKeyName { get; set; } = "";

    public string SasKeyValue { get; set; } = "";
}