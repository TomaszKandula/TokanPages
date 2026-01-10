using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class UserBankAccount : Entity<Guid>
{
    public Guid UserId { get; set; }
    [Required]
    [MaxLength(255)]
    public string BankName { get; set; }
    [Required]
    [MaxLength(11)]
    public string SwiftNumber { get; set; }        
    [Required]
    [MaxLength(28)]
    public string AccountNumber { get; set; }
    [Required]
    public CurrencyCode CurrencyCode { get; set; }

    /* Navigation properties */
    public User.User User { get; set; }
    public ICollection<BatchInvoice> BatchInvoices { get; set; } = new HashSet<BatchInvoice>();
}