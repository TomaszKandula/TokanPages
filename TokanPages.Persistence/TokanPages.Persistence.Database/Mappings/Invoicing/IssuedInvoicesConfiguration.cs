using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class IssuedInvoicesConfiguration : IEntityTypeConfiguration<IssuedInvoices>
{
    public void Configure(EntityTypeBuilder<IssuedInvoices> builder)
    { 
        builder.Property(invoices => invoices.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(invoices => invoices.Users)
            .WithMany(users => users.IssuedInvoices)
            .HasForeignKey(invoices => invoices.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_IssuedInvoices_Users");
    }
}