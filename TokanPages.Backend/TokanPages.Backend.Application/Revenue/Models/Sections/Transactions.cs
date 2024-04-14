using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Revenue.Models.Sections;

[ExcludeFromCodeCoverage]
public class Transactions
{
    public PayMethod? PayMethod { get; set; }
    
    public string? PaymentFlow { get; set; }
    
    public Card? Card { get; set; }

    public BankAccount? BankAccount { get; set; }
}