using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
public class ServiceBusMessage : Entity<Guid>
{
    public bool IsConsumed { get; set; }
}