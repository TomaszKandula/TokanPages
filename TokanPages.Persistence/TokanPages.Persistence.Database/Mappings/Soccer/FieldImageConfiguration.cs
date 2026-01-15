using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Soccer;

namespace TokanPages.Persistence.Database.Mappings.Soccer;

[ExcludeFromCodeCoverage]
public class FieldImageConfiguration : IEntityTypeConfiguration<FieldImage>
{
    public void Configure(EntityTypeBuilder<FieldImage> builder) 
        => builder.Property(field => field.Id).ValueGeneratedOnAdd();
}