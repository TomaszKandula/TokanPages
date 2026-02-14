using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Invoicing.Models;

[ExcludeFromCodeCoverage]
public class BatchInvoiceProcessingDto
{
    public required Guid ProcessingId { get; init; }

    public TimeSpan? ProcessingTime { get; init; }

    public required ProcessingStatus ProcessingStatus { get; init; }
}