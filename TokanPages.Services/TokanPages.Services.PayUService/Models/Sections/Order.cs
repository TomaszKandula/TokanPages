using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class Order
{
    public string? OrderId { get; set; }

    public string? ExtOrderId { get; set; }

    public DateTime? OrderCreateDate { get; set; }

    public string? NotifyUrl { get; set; }

    public string? CustomerIp { get; set; }

    public string? MerchantPosId { get; set; }

    public string? ValidityTime { get; set; }
    
    public string? Description { get; set; }

    public string? AdditionalDescription { get; set; }
    
    public string? CurrencyCode { get; set; }

    public string? TotalAmount { get; set; }

    public string? Status { get; set; }

    public Buyer? Buyer { get; set; }

    public List<Product>? Products { get; set; }
}