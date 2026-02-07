using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities.Invoicing;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "BatchInvoices")]
public class BatchInvoice : Entity<Guid>, IAuditable
{
    public string InvoiceNumber { get; set; }

    public DateTime VoucherDate { get; set; }

    public DateTime ValueDate { get; set; }

    public DateTime DueDate { get; set; }
    public int PaymentTerms { get; set; }

    public PaymentType PaymentType { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public string CustomerName { get; set; }

    public string CustomerVatNumber { get; set; }

    public CountryCode CountryCode { get; set; }

    public string City { get; set; }

    public string StreetAddress { get; set; }

    public string PostalCode { get; set; }

    public string PostalArea { get; set; }

    public Guid ProcessBatchKey { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public string InvoiceTemplateName { get; set; }

    public Guid UserId { get; set; }

    public Guid UserCompanyId { get; set; }

    public Guid UserBankAccountId { get; set; }
}