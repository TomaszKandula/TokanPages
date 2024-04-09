using MediatR;

namespace TokanPages.Backend.Application.Templates.Commands;

public class AddInvoiceTemplateCommand : IRequest<AddInvoiceTemplateCommandResult>
{
    public string Name { get; set; }

    public byte[] Data { get; set; }

    public string DataType { get; set; }

    public string Description { get; set; }
}