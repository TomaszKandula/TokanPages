using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Invoicing;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Users;

[ExcludeFromCodeCoverage]//TODO: add attribute, remove EFCore nav props
public class UserBankAccount : Entity<Guid>
{
    public Guid UserId { get; set; }

    public string BankName { get; set; }

    public string SwiftNumber { get; set; }        

    public string AccountNumber { get; set; }

    public CurrencyCode CurrencyCode { get; set; }

    /* Navigation properties */
    public User User { get; set; }
    public ICollection<BatchInvoice> BatchInvoices { get; set; } = new HashSet<BatchInvoice>();
}