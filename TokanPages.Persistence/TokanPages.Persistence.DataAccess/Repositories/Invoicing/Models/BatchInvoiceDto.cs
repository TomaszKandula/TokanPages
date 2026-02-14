using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceDto
{
    public Guid? Id { get; init; }

    public required string InvoiceNumber { get; init; }

    public required DateTime VoucherDate { get; init; }

    public required DateTime ValueDate { get; init; }

    public required DateTime DueDate { get; init; }

    public required int PaymentTerms { get; init; }

    public required PaymentType PaymentType { get; init; }

    public required PaymentStatus PaymentStatus { get; init; }

    public required string CustomerName { get; init; }

    public required string CustomerVatNumber { get; init; }

    public required CountryCode CountryCode { get; init; }

    public required string City { get; init; }

    public required string StreetAddress { get; init; }

    public required string PostalCode { get; init; }

    public required string PostalArea { get; init; }

    public required Guid ProcessBatchKey { get; init; }

    public required Guid CreatedBy { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required string InvoiceTemplateName { get; init; }

    public required Guid UserId { get; init; }

    public required Guid UserCompanyId { get; init; }

    public required Guid UserBankAccountId { get; init; }
}