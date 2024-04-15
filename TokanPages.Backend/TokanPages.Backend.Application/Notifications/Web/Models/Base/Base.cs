namespace TokanPages.Backend.Application.Notifications.Web.Models.Base;

public class Base
{
    public Guid? UserId { get; set; }

    public string Handler { get; set; } = "";
}