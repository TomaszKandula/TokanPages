using MediatR;
using TokanPages.Backend.Application.Templates.Queries.Models;

namespace TokanPages.Backend.Application.Templates.Queries;

public class GetInvoiceTemplatesQuery : IRequest<IEnumerable<InvoiceTemplateInfo>> { }