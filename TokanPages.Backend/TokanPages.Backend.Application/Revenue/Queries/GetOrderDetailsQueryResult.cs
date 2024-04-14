using TokanPages.Backend.Application.Revenue.Models.Sections;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderDetailsQueryResult
{
    public IEnumerable<Order>? Orders { get; set; }

    public Status? Status { get; set; }

    public IEnumerable<Property>? Properties { get; set; }
}