using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.PayUService.Models.Sections;

[ExcludeFromCodeCoverage]
public class Card
{
    public string? Number { get; set; }

    public string? ExpirationMonth { get; set; }

    public string? ExpirationYear { get; set; }

    public string? Cvv { get; set; }

    public object? CardData { get; set; }

    public object? CardInstallmentProposal { get; set; }

    public string? FirstTransactionId { get; set; }
}