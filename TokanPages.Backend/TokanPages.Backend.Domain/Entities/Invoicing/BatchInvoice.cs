using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "BatchInvoices")]
public class BatchInvoice : Entity<Guid>, IAuditable
{
    public required string InvoiceNumber { get; set; }

    public required DateTime VoucherDate { get; set; }

    public required DateTime ValueDate { get; set; }

    public required DateTime DueDate { get; set; }

    public required int PaymentTerms { get; set; }

    public required PaymentType PaymentType { get; set; }

    public required PaymentStatus PaymentStatus { get; set; }

    public required string CustomerName { get; set; }

    public required string CustomerVatNumber { get; set; }

    public required CountryCode CountryCode { get; set; }

    public required string City { get; set; }

    public required string StreetAddress { get; set; }

    public required string PostalCode { get; set; }

    public required string PostalArea { get; set; }

    public required Guid ProcessBatchKey { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public required string InvoiceTemplateName { get; set; }

    public required Guid UserId { get; set; }

    public required Guid UserCompanyId { get; set; }

    public required Guid UserBankAccountId { get; set; }
}