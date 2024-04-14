using MediatR;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class RemoveInvoiceTemplateCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}