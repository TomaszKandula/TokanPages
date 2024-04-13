using TokanPages.Backend.Domain.Enums;
using MediatR;

namespace TokanPages.Backend.Application.NotificationsMobile.Commands;

public class AddInstallationCommand : IRequest<AddInstallationCommandResult>
{
    public string PnsHandle { get; set; } = "";

    public PlatformType Platform { get; set; }

    public string[] Tags { get; set; } = Array.Empty<string>();
}