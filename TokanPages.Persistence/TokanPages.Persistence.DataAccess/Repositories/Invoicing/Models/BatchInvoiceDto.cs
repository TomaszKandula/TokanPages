using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceDto
{
    public Guid Id { get; set; }

    public string InvoiceNumber { get; set; } = string.Empty;

    public DateTime VoucherDate { get; set; }

    public DateTime ValueDate { get; set; }

    public DateTime DueDate { get; set; }

    public int PaymentTerms { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public string CustomerName { get; set; } = string.Empty;

    public string CustomerVatNumber { get; set; } = string.Empty;

    public CountryCode CountryCode { get; set; }

    public string City { get; set; } = string.Empty;

    public string StreetAddress { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public string PostalArea { get; set; } = string.Empty;

    public Guid ProcessBatchKey { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public string InvoiceTemplateName { get; set; } = string.Empty;

    public Guid UserId { get; set; }

    public Guid UserCompanyId { get; set; }

    public Guid UserBankAccountId { get; set; }
}