using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Backend.Application.Invoicing.Templates.Commands;

public class ReplaceInvoiceTemplateCommand : IRequest<Unit>
{
    public Guid Id { get; set; }

    public string Description { get; set; } = "";

    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}