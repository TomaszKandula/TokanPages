using MediatR;

using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Notifications.Mobile.Commands;

public class SendNotificationCommand : IRequest<SendNotificationCommandResult>
{
    public PlatformType Platform { get; set; }

    public string MessageTitle { get; set; } = "";

    public string MessageBody { get; set; } = "";

    public string[]? Tags { get; set; }
}