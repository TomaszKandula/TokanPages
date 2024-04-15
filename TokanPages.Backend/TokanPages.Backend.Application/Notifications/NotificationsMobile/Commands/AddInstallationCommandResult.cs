namespace TokanPages.Backend.Application.NotificationsMobile.Commands;

public class AddInstallationCommandResult
{
    public Guid InstallationId { get; set; }

    public string? RegistrationId { get; set; }

    public bool IsVerified { get; set; }
}