using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Invoicing.Dto.Batches;

/// <summary>
/// 
/// </summary>
[ExcludeFromCodeCoverage]
public class OrderDetailBase<T>
{
    /// <summary>
    /// 
    /// </summary>
    public DateTime? VoucherDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? ValueDate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int PaymentTerms { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public PaymentTypes PaymentType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public PaymentStatuses PaymentStatus { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string CompanyName { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string CompanyVatNumber { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public CountryCodes CountryCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string City { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string StreetAddress { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string PostalCode { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string PostalArea { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public string InvoiceTemplateName { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public CurrencyCodes CurrencyCode { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public IEnumerable<T> InvoiceItems { get; set; } = new List<T>();
}