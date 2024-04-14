using TokanPages.Backend.Application.Revenue.Models.Sections;
using MediatR;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class PostNotificationCommand : IRequest<Unit>
{
    public Order? Order { get; set; }

    public DateTime? LocalReceiptDateTime { get; set; }

    public IEnumerable<Property>? Properties { get; set; }
}