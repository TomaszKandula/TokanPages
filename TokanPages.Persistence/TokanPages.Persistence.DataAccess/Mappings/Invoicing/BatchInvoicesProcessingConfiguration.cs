using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.DataAccess.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoicesProcessingConfiguration : IEntityTypeConfiguration<BatchInvoiceProcessing>
{
    public void Configure(EntityTypeBuilder<BatchInvoiceProcessing> builder)
        => builder.Property(processing => processing.Id).ValueGeneratedOnAdd();
}