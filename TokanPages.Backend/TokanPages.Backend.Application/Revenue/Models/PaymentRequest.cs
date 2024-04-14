using TokanPages.Backend.Application.Revenue.Models.Sections;

namespace TokanPages.Backend.Application.Revenue.Models;

public class PaymentRequest
{
    public string? NotifyUrl { get; set; }

    public string? ContinueUrl { get; set; }

    public string? CustomerIp { get; set; }

    public string? MerchantPosId { get; set; }

    public string? CardOnFile { get; set; }

    public string? Recurring { get; set; }

    public string? Description { get; set; }
    
    public string? TotalAmount { get; set; }

    public string? CurrencyCode { get; set; }

    public string? ExtOrderId { get; set; }

    public IEnumerable<Product>? Products { get; set; }

    public Buyer? Buyer { get; set; }

    public PayMethods? PayMethods { get; set; }

    public Authentication? ThreeDsAuthentication { get; set; }
}