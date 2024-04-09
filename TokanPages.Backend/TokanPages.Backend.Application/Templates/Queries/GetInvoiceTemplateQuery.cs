using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Templates.Queries;

public class GetInvoiceTemplateQuery : IRequest<FileContentResult>
{
    public Guid Id { get; set; }        
}