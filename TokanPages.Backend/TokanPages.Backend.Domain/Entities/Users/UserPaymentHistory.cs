using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserPaymentsHistory")]
public class UserPaymentHistory : Entity<Guid>
{
    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public string CurrencyIso { get; set; }

    public TermType Term { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }
}