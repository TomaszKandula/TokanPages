using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Revenue.Dto.Payments.Sections;

/// <summary>
/// Order details.
/// </summary>
[ExcludeFromCodeCoverage]
public class OrderDto
{
    /// <summary>
    /// OrderId.
    /// </summary>
    public string? OrderId { get; set; }

    /// <summary>
    /// ExtOrderId.
    /// </summary>
    public string? ExtOrderId { get; set; }

    /// <summary>
    /// OrderCreateDate.
    /// </summary>
    public DateTime? OrderCreateDate { get; set; }

    /// <summary>
    /// NotifyUrl.
    /// </summary>
    public string? NotifyUrl { get; set; }

    /// <summary>
    /// CustomerIp.
    /// </summary>
    public string? CustomerIp { get; set; }

    /// <summary>
    /// MerchantPosId.
    /// </summary>
    public string? MerchantPosId { get; set; } = "";

    /// <summary>
    /// Description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// CurrencyCode.
    /// </summary>
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// TotalAmount.
    /// </summary>
    public string? TotalAmount { get; set; }

    /// <summary>
    /// Buyer.
    /// </summary>
    public BuyerDto? Buyer { get; set; }

    /// <summary>
    /// PayMethod.
    /// </summary>
    public PayMethodDto? PayMethod { get; set; }

    /// <summary>
    /// Products.
    /// </summary>
    public List<ProductDto>? Products { get; set; }

    /// <summary>
    /// Status.
    /// </summary>
    public string? Status { get; set; }
}