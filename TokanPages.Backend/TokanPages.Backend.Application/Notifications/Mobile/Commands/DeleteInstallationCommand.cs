using MediatR;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class DeleteInstallationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}