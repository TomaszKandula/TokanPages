using MediatR;
using TokanPages.Backend.Application.Invoicing.Models;

namespace TokanPages.Backend.Application.Invoicing.Templates.Queries;

public class GetInvoiceTemplatesQuery : IRequest<IList<InvoiceTemplateInfo>> { }