namespace TokanPages.Backend.Application.Users.Commands;

public class ActivateUserCommandResult
{
    public Guid UserId { get; set; }

    public bool HasBusinessLock { get; set; }
}