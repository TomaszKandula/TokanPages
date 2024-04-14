using System.Diagnostics.CodeAnalysis;
using TokanPages.Revenue.Dto.Payments.Sections;

namespace TokanPages.Revenue.Dto.Payments;

/// <summary>
/// Use it when you want to create payment for a product/service.
/// </summary>
[ExcludeFromCodeCoverage]
public class CreatePaymentDto
{
    /// <summary>
    /// Optional user ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Notification URL.
    /// </summary>
    public string? NotifyUrl { get; set; }

    /// <summary>
    /// Continue Url used to redirect user after payment process.
    /// </summary>
    public string? ContinueUrl { get; set; }

    /// <summary>
    /// Customer IP.
    /// </summary>
    public string? CustomerIp { get; set; }

    /// <summary>
    /// Merchant POS ID.
    /// </summary>
    public string? MerchantPosId { get; set; }

    /// <summary>
    /// Indicates if it is first payment or next one using card on file.
    /// </summary>
    /// <remarks>
    /// Possible options: FIRST, STANDARD_CARDHOLDER, STANDARD_MERCHANT.
    /// </remarks>>
    public string? CardOnFile { get; set; }

    /// <summary>
    /// Indicates if it is first payment or next one (recurring payment).
    /// </summary>
    /// <remarks>
    /// Possible options: FIRST and STANDARD.
    /// </remarks>>
    public string? Recurring { get; set; }

    /// <summary>
    /// Description.
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Charge amount.
    /// </summary>
    public string? TotalAmount { get; set; }

    /// <summary>
    /// Currency code (ISO).
    /// </summary>
    public string? CurrencyCode { get; set; }

    /// <summary>
    /// External order ID.
    /// </summary>
    public string? ExtOrderId { get; set; }

    /// <summary>
    /// Products details.
    /// </summary>
    public IEnumerable<ProductDto>? Products { get; set; }

    /// <summary>
    /// Buyer details.
    /// </summary>
    public BuyerDto? Buyer { get; set; }

    /// <summary>
    /// Payment method details.
    /// </summary>
    public PayMethodsDto? PayMethods { get; set; }

    /// <summary>
    /// 3DS authentication.
    /// </summary>
    public Authentication? ThreeDsAuthentication { get; set; }
}