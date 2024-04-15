namespace TokanPages.Backend.Application.Notifications.Web.Models;

public class UserActivationData
{
    public bool IsActivated { get; set; }

    public bool IsVerified { get; set; }

    public bool HasBusinessLock { get; set; }
}