using MediatR;

namespace TokanPages.Backend.Application.NotificationsWeb.Command;

public class StatusRequestCommand : IRequest<StatusRequestCommandResult>
{
    public Guid StatusId { get; set; }
}
