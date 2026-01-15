using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Attribute = TokanPages.Backend.Domain.Entities.Soccer.Attribute;

namespace TokanPages.Persistence.Database.Mappings.Soccer;

[ExcludeFromCodeCoverage]
public class AttributeConfiguration : IEntityTypeConfiguration<Attribute>
{
    public void Configure(EntityTypeBuilder<Attribute> builder) 
        => builder.Property(attribute => attribute.Id).ValueGeneratedOnAdd();
}