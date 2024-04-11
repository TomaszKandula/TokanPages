using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class InvoiceTemplatesConfiguration : IEntityTypeConfiguration<InvoiceTemplates>
{
    public void Configure(EntityTypeBuilder<InvoiceTemplates> builder) 
        => builder.Property(templates => templates.Id).ValueGeneratedOnAdd();
}