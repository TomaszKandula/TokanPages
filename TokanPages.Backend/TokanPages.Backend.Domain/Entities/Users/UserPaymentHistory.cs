using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserPaymentsHistory")]
public class UserPaymentHistory : Entity<Guid>
{
    public required Guid UserId { get; set; }

    public required decimal Amount { get; set; }

    public required string CurrencyIso { get; set; }

    public required TermType Term { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }
}