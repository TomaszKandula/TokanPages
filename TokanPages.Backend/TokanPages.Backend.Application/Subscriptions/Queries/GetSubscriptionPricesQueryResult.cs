using TokanPages.Backend.Application.Subscriptions.Models;

namespace TokanPages.Backend.Application.Subscriptions.Queries;

public class GetSubscriptionPricesQueryResult
{
    public IList<PriceItem>? Prices { get; set; }
}