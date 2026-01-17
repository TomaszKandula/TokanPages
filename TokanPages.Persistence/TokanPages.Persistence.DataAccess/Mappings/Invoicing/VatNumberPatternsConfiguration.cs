using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.DataAccess.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class VatNumberPatternsConfiguration : IEntityTypeConfiguration<VatNumberPattern>
{
    public void Configure(EntityTypeBuilder<VatNumberPattern> builder) 
        => builder.Property(pattern => pattern.Id).ValueGeneratedOnAdd();
}