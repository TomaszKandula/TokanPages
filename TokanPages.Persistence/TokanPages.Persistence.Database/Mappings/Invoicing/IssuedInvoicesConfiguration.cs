using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class IssuedInvoicesConfiguration : IEntityTypeConfiguration<IssuedInvoice>
{
    public void Configure(EntityTypeBuilder<IssuedInvoice> builder)
    { 
        builder.Property(invoice => invoice.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(invoice => invoice.User)
            .WithMany(user => user.IssuedInvoices)
            .HasForeignKey(invoice => invoice.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}