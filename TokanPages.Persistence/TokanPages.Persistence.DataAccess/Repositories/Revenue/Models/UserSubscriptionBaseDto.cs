using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Revenue.Models;

[ExcludeFromCodeCoverage]
public class UserSubscriptionBaseDto
{
    public TermType Term { get; set; }

    public decimal TotalAmount { get; set; }

    public string CurrencyIso { get; set; } = string.Empty;

    public string ExtCustomerId { get; set; } = string.Empty;

    public string ExtOrderId { get; set; } = string.Empty;
}