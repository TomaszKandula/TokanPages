using MediatR;

namespace TokanPages.Backend.Application.NotificationsWeb.Command;

public class NotifyRequestCommand : IRequest<NotifyRequestCommandResult>
{
    public Guid UserId { get; set; }

    public bool CanSkipPreservation { get; set; }

    public string? ExternalPayload { get; set; }

    public string Handler { get; set; } = "";
}