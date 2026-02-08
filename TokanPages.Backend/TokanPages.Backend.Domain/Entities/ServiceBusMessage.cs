using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "ServiceBusMessages")]
public class ServiceBusMessage : Entity<Guid>
{
    public required bool IsConsumed { get; set; }
}