using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.DataAccess.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class InvoiceTemplatesConfiguration : IEntityTypeConfiguration<InvoiceTemplate>
{
    public void Configure(EntityTypeBuilder<InvoiceTemplate> builder) 
        => builder.Property(template => template.Id).ValueGeneratedOnAdd();
}