using MediatR;

namespace TokanPages.Backend.Application.Templates.Commands;

public class RemoveInvoiceTemplateCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}