using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "SubscriptionsPricing")]
public class SubscriptionPricing : Entity<Guid>, IAuditable
{
    public TermType Term { get; set; }

    public decimal Price { get; set; }

    public string CurrencyIso { get; set; }

    public string LanguageIso { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}