using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoiceItems : Entity<Guid>
{
    public Guid BatchInvoiceId { get; set; }
    [Required]
    [MaxLength(255)]
    public string ItemText { get; set; }
    [Required] 
    public int ItemQuantity { get; set; }
    [Required]
    [MaxLength(10)]
    public string ItemQuantityUnit { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ItemAmount { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? ItemDiscountRate { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ValueAmount { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal? VatRate { get; set; }
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal GrossAmount { get; set; }
    [Required]
    public CurrencyCodes CurrencyCode { get; set; }

    /* Navigation properties */
    public BatchInvoices BatchInvoices { get; set; }
}