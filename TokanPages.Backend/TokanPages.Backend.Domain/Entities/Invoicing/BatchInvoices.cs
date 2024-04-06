using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Entities.User;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoices : Entity<Guid>, IAuditable
{
    [Required]
    [MaxLength(255)]
    public string InvoiceNumber { get; set; }

    [Required]
    public DateTime VoucherDate { get; set; }

    [Required]
    public DateTime ValueDate { get; set; }

    [Required]
    public DateTime DueDate { get; set; }

    public int PaymentTerms { get; set; }

    [Required]
    public PaymentTypes PaymentType { get; set; }

    [Required]
    public PaymentStatuses PaymentStatus { get; set; }

    [Required]
    [MaxLength(255)]
    public string CustomerName { get; set; }

    [MaxLength(25)]
    public string CustomerVatNumber { get; set; }

    [Required]
    public CountryCodes CountryCode { get; set; }

    [Required]
    [MaxLength(255)]
    public string City { get; set; }

    [Required]
    [MaxLength(100)]
    public string StreetAddress { get; set; }

    [Required]
    [MaxLength(25)]
    public string PostalCode { get; set; }

    [Required]
    [MaxLength(150)]
    public string PostalArea { get; set; }

    [Required]
    public Guid ProcessBatchKey { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    [Required]
    [MaxLength(255)]
    public string InvoiceTemplateName { get; set; }

    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid UserCompanyId { get; set; }

    [Required]
    public Guid UserBankAccountId { get; set; }

    public BatchInvoicesProcessing BatchInvoicesProcessing { get; set; }

    public Users Users { get; set; }

    public UserCompanies UserCompanies { get; set; }

    public UserBankAccounts UserBankAccounts { get; set; }

    public ICollection<BatchInvoiceItems> BatchInvoiceItems { get; set; } = new HashSet<BatchInvoiceItems>();
}