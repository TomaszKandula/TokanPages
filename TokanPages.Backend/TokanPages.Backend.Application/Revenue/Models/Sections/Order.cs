namespace TokanPages.Backend.Application.Revenue.Models.Sections;

public class Order
{
    public string? OrderId { get; set; }

    public string? ExtOrderId { get; set; }

    public DateTime? OrderCreateDate { get; set; }

    public string? NotifyUrl { get; set; }

    public string? CustomerIp { get; set; }

    public string? MerchantPosId { get; set; }

    public string? Description { get; set; }

    public string? CurrencyCode { get; set; }

    public string? TotalAmount { get; set; }

    public Buyer? Buyer { get; set; }

    public PayMethod? PayMethod { get; set; }

    public IEnumerable<Product>? Products { get; set; }

    public string? Status { get; set; } = "";
}