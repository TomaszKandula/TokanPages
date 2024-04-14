using TokanPages.Backend.Application.Revenue.Models.Sections;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderTransactionsQueryResult
{
    public IEnumerable<Transactions>? Transactions { get; set; }
}