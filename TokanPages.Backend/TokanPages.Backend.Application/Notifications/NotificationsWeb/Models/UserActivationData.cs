namespace TokanPages.Backend.Application.NotificationsWeb.Models;

public class UserActivationData
{
    public bool IsActivated { get; set; }

    public bool IsVerified { get; set; }

    public bool HasBusinessLock { get; set; }
}