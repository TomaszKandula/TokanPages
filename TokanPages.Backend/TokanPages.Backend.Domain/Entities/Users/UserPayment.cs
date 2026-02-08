using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserPayments")]
public class UserPayment : Entity<Guid>, IAuditable
{
    public required Guid UserId { get; set; }

    public required string ExtOrderId { get; set; }

    public required string PmtOrderId { get; set; }

    public required string PmtStatus { get; set; }

    public required string PmtType { get; set; }

    public required string PmtToken { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}