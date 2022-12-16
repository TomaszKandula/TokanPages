using MediatR;

namespace TokanPages.Backend.Application.NotificationsWeb.Command;

public class NotifyRequestCommand : IRequest<Unit>
{
    public object? Payload { get; set; }

    public string Handler { get; set; } = "";
}