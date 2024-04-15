using MediatR;

namespace TokanPages.Backend.Application.Notifications.Web.Command;

public class StatusRequestCommand : IRequest<StatusRequestCommandResult>
{
    public Guid StatusId { get; set; }
}
