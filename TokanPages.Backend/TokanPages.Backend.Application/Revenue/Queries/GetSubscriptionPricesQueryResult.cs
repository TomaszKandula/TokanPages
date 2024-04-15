using TokanPages.Backend.Application.Subscriptions.Models;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionPricesQueryResult
{
    public IList<PriceItem>? Prices { get; set; }
}