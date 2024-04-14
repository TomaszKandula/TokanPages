using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class AddInvoiceTemplateCommand : IRequest<AddInvoiceTemplateCommandResult>
{
    public string Description { get; set; } = "";

    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}