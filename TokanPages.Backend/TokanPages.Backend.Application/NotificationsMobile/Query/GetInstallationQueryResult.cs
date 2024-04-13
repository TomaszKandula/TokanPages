namespace TokanPages.Backend.Application.NotificationsMobile.Query;

public class GetInstallationQueryResult
{
    public string InstallationId { get; set; } = "";

    public DateTime? ExpirationTime { get; set; }

    public string Platform { get; set; } = "";

    public IList<string>? Tags { get; set; }
}
