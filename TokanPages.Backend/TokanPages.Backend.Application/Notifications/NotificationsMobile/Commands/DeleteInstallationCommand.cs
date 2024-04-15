using MediatR;

namespace TokanPages.Backend.Application.NotificationsMobile.Commands;

public class DeleteInstallationCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}