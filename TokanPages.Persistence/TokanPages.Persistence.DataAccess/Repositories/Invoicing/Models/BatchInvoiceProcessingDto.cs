using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceProcessingDto
{
    public Guid ProcessingId { get; set; }

    public TimeSpan? ProcessingTime { get; set; }

    public ProcessingStatus ProcessingStatus { get; set; }
}