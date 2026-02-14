using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceDto
{
    public Guid? Id { get; init; }

    public string InvoiceNumber { get; init; } = string.Empty;

    public DateTime VoucherDate { get; init; }

    public DateTime ValueDate { get; init; }

    public DateTime DueDate { get; init; }

    public int PaymentTerms { get; init; }

    public PaymentType PaymentType { get; init; }

    public PaymentStatus PaymentStatus { get; init; }

    public string CustomerName { get; init; } = string.Empty;

    public string CustomerVatNumber { get; init; } = string.Empty;

    public CountryCode CountryCode { get; init; }

    public string City { get; init; } = string.Empty;

    public string StreetAddress { get; init; } = string.Empty;

    public string PostalCode { get; init; } = string.Empty;

    public string PostalArea { get; init; } = string.Empty;

    public Guid ProcessBatchKey { get; init; }

    public Guid CreatedBy { get; init; }

    public DateTime CreatedAt { get; init; }

    public string InvoiceTemplateName { get; init; } = string.Empty;

    public Guid UserId { get; init; }

    public Guid UserCompanyId { get; init; }

    public Guid UserBankAccountId { get; init; }
}