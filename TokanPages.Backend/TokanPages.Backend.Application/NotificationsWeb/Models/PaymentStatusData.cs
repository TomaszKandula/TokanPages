namespace TokanPages.Backend.Application.NotificationsWeb.Models;

public class PaymentStatusData
{
    public string? PaymentStatus { get; set; }
    
    public bool IsActive { get; set; }

    public bool AutoRenewal { get; set; }

    public decimal TotalAmount { get; set; }

    public string? CurrencyIso { get; set; }

    public string? ExtCustomerId { get; set; }

    public string? ExtOrderId { get; set; }

    public DateTime? ExpiresAt { get; set; }
}