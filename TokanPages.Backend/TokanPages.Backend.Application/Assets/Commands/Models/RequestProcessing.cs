namespace TokanPages.Backend.Application.Assets.Commands.Models;

public class RequestProcessing
{
    public Guid MessageId { get; set; }

    public Guid TicketId { get; set; }

    public Guid UserId { get; set; }

    public string? TargetEnv { get; set; }

    public TargetDetails? Details { get; set; }
}