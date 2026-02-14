using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UserSubscriptionBaseDto
{
    public required TermType Term { get; init; }

    public required decimal TotalAmount { get; init; }

    public required string CurrencyIso { get; init; }

    public required string ExtCustomerId { get; init; }

    public required string ExtOrderId { get; init; }
}