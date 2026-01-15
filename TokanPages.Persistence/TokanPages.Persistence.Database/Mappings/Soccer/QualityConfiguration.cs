using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Soccer;

namespace TokanPages.Persistence.Database.Mappings.Soccer;

[ExcludeFromCodeCoverage]
public class QualityConfiguration : IEntityTypeConfiguration<Quality>
{
    public void Configure(EntityTypeBuilder<Quality> builder) 
        => builder.Property(quality => quality.Id).ValueGeneratedOnAdd();
}