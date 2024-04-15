using MediatR;

namespace TokanPages.Backend.Application.Subscriptions.Queries;

public class GetSubscriptionPricesQuery : IRequest<GetSubscriptionPricesQueryResult>
{
    public string? LanguageIso { get; set; }
}
