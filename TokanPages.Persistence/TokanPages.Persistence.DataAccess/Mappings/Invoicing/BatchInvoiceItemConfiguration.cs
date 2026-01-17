using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.DataAccess.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoiceItemConfiguration : IEntityTypeConfiguration<BatchInvoiceItem>
{
    public void Configure(EntityTypeBuilder<BatchInvoiceItem> builder)
    {
        builder.Property(item => item.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(item => item.BatchInvoice)
            .WithMany(invoice => invoice.BatchInvoiceItems)
            .HasForeignKey(item => item.BatchInvoiceId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}