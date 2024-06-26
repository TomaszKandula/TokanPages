using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TokanPages.Backend.Application.Invoicing.Batches.Queries;

public class GetIssuedBatchInvoiceQuery : IRequest<FileContentResult>
{
    public string InvoiceNumber { get; set; } = "";
}