namespace TokanPages.Backend.Application.Subscriptions.Models;

public class PriceItem
{
    public int Term { get; set; }

    public decimal Price { get; set; }

    public string? CurrencyIso { get; set; }

    public string? LanguageIso { get; set; }
}