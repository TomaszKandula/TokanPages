namespace TokanPages.Backend.Application.Revenue.Models.Sections;

public class PayMethod
{
    public Card? Card { get; set; }

    public string? Type { get; set; }

    public string? Value { get; set; }
}