namespace TokanPages.Backend.Application.Users.Commands;

public class AddUserFileCommandResult
{
    public bool IsBeingProcessed { get; set; }

    public Guid? TicketId { get; set; }
}