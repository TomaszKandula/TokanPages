using MediatR;

namespace TokanPages.Backend.Application.Revenue.Queries;

public class GetOrderDetailsQuery : IRequest<GetOrderDetailsQueryResult>
{
    public string OrderId { get; set; } = "";
}