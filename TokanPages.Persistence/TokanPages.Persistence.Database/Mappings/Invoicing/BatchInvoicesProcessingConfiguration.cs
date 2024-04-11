using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoicesProcessingConfiguration : IEntityTypeConfiguration<BatchInvoicesProcessing>
{
    public void Configure(EntityTypeBuilder<BatchInvoicesProcessing> builder)
        => builder.Property(processing => processing.Id).ValueGeneratedOnAdd();
}