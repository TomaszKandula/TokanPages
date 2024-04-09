using MediatR;

namespace TokanPages.Backend.Application.Templates.Commands;

public class ReplaceInvoiceTemplateCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public byte[] Data { get; set; } = Array.Empty<byte>();

    public string DataType { get; set; } = "";

    public string Description { get; set; } = "";
}