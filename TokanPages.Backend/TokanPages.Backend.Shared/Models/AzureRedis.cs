namespace TokanPages.Backend.Shared.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AzureRedis
{
    public string InstanceName { get; set; } = "";

    public string ConnectionString { get; set; } = "";

    public int ExpirationMinute { get; set; }

    public int ExpirationSecond { get; set; }
}