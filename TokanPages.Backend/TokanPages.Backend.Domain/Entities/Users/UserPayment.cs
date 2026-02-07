using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "UserPayments")]
public class UserPayment : Entity<Guid>, IAuditable
{
    public Guid UserId { get; set; }
    [MaxLength(100)]
    public string ExtOrderId { get; set; }
    [MaxLength(100)]
    public string PmtOrderId { get; set; }
    [MaxLength(30)]
    public string PmtStatus { get; set; }
    [MaxLength(100)]
    public string PmtType { get; set; }
    [MaxLength(100)]
    public string PmtToken { get; set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }
}