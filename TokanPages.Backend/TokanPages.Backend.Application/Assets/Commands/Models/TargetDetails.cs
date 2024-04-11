using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Assets.Commands.Models;

public class TargetDetails
{
    public ProcessingTarget Target { get; set; }

    public Guid? EntityId { get; set; }

    public bool? ShouldCompactVideo { get; set; }
}