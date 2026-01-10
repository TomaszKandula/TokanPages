using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class VatNumberPatternsConfiguration : IEntityTypeConfiguration<VatNumberPattern>
{
    public void Configure(EntityTypeBuilder<VatNumberPattern> builder) 
        => builder.Property(patterns => patterns.Id).ValueGeneratedOnAdd();
}