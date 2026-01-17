using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.DataAccess.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoicesConfiguration : IEntityTypeConfiguration<BatchInvoice>
{
    public void Configure(EntityTypeBuilder<BatchInvoice> builder)
    {
        builder.Property(invoice => invoice.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(invoice => invoice.BatchInvoiceProcessing)
            .WithMany(processing => processing.BatchInvoices)
            .HasForeignKey(invoice => invoice.ProcessBatchKey)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(invoice => invoice.User)
            .WithMany(user => user.BatchInvoices)
            .HasForeignKey(invoice => invoice.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(invoice => invoice.UserCompany)
            .WithMany(company => company.BatchInvoices)
            .HasForeignKey(invoice => invoice.UserCompanyId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(invoice => invoice.UserBankAccount)
            .WithMany(data => data.BatchInvoices)
            .HasForeignKey(invoice => invoice.UserBankAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}