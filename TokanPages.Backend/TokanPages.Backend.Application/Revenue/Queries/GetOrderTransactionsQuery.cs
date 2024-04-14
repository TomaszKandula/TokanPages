using MediatR;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderTransactionsQuery : IRequest<GetOrderTransactionsQueryResult>
{
    public string OrderId { get; set; } = "";
}