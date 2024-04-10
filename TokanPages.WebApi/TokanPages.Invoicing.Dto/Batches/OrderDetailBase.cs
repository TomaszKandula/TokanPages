using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Invoicing.Dto.Batches;

/// <summary>
/// Use it when you want to issue an invoice.
/// </summary>
[ExcludeFromCodeCoverage]
public class OrderDetailBase<T>
{
    /// <summary>
    /// Voucher Date.
    /// </summary>
    public DateTime? VoucherDate { get; set; }

    /// <summary>
    /// Value Date.
    /// </summary>
    public DateTime? ValueDate { get; set; }

    /// <summary>
    /// Payment Terms.
    /// </summary>
    public int PaymentTerms { get; set; }

    /// <summary>
    /// Payment Type.
    /// </summary>
    public PaymentTypes PaymentType { get; set; }

    /// <summary>
    /// Payment Status.
    /// </summary>
    public PaymentStatuses PaymentStatus { get; set; }

    /// <summary>
    /// Company Name.
    /// </summary>
    public string CompanyName { get; set; } = "";

    /// <summary>
    /// Company VAT Number.
    /// </summary>
    public string CompanyVatNumber { get; set; } = "";

    /// <summary>
    /// Country Code.
    /// </summary>
    public CountryCodes CountryCode { get; set; }

    /// <summary>
    /// City.
    /// </summary>
    public string City { get; set; } = "";

    /// <summary>
    /// Street Address.
    /// </summary>
    public string StreetAddress { get; set; } = "";

    /// <summary>
    /// Postal Code.
    /// </summary>
    public string PostalCode { get; set; } = "";

    /// <summary>
    /// Postal Area.
    /// </summary>
    public string PostalArea { get; set; } = "";

    /// <summary>
    /// Invoice Template Name.
    /// </summary>
    public string InvoiceTemplateName { get; set; } = "";

    /// <summary>
    /// Currency Code.
    /// </summary>
    public CurrencyCodes CurrencyCode { get; set; }

    /// <summary>
    /// Invoice Items.
    /// </summary>
    public IEnumerable<T> InvoiceItems { get; set; } = new List<T>();
}