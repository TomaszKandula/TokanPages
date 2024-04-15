using MediatR;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetSubscriptionPricesQuery : IRequest<GetSubscriptionPricesQueryResult>
{
    public string? LanguageIso { get; set; }
}
