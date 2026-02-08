using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserSubscriptions")]
public class UserSubscription : Entity<Guid>, IAuditable
{
    public required Guid UserId { get; set; }

    public required bool IsActive { get; set; }

    public required bool AutoRenewal { get; set; }

    public required TermType Term { get; set; }

    public required decimal TotalAmount { get; set; }

    public required string CurrencyIso { get; set; }

    public required string ExtCustomerId { get; set; }

    public required string ExtOrderId { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CompletedAt { get; set; }

    public DateTime? ExpiresAt { get; set; }
}