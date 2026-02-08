using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Attributes;
using TokanPages.Backend.Domain.Contracts;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Domain.Entities;

[ExcludeFromCodeCoverage]
[DatabaseTable(Schema = "operation", TableName = "SubscriptionsPricing")]
public class SubscriptionPricing : Entity<Guid>, IAuditable
{
    public required TermType Term { get; set; }

    public required decimal Price { get; set; }

    public required string CurrencyIso { get; set; }

    public required string LanguageIso { get; set; }

    public required Guid CreatedBy { get; set; }

    public required DateTime CreatedAt { get; set; }

    public Guid? ModifiedBy { get; set; }

    public DateTime? ModifiedAt { get; set; }
}