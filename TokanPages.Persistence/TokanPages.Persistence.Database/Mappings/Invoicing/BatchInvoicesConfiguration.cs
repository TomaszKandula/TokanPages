using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class BatchInvoicesConfiguration : IEntityTypeConfiguration<BatchInvoices>
{
    public void Configure(EntityTypeBuilder<BatchInvoices> builder)
    {
        builder.Property(invoices => invoices.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(invoices => invoices.BatchInvoicesProcessing)
            .WithMany(processing => processing.BatchInvoices)
            .HasForeignKey(invoices => invoices.ProcessBatchKey)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BatchInvoices_BatchInvoicesProcessing");

        builder
            .HasOne(invoices => invoices.Users)
            .WithMany(users => users.BatchInvoices)
            .HasForeignKey(invoices => invoices.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BatchInvoices_Users");

        builder
            .HasOne(invoices => invoices.UserCompanies)
            .WithMany(details => details.BatchInvoices)
            .HasForeignKey(invoices => invoices.UserCompanyId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BatchInvoices_UserCompanies");

        builder
            .HasOne(invoices => invoices.UserBankAccounts)
            .WithMany(bankData => bankData.BatchInvoices)
            .HasForeignKey(invoices => invoices.UserBankAccountId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_BatchInvoices_UserBankAccount");
    }
}