using TokanPages.Backend.Application.Revenue.Models;
using MediatR;

namespace TokanPages.Backend.Application.Revenue.Commands;

public class CreatePaymentCommand : IRequest<CreatePaymentCommandResult>
{
    public Guid? UserId { get; set; }

    public PaymentRequest Request { get; set; } = new();
}
