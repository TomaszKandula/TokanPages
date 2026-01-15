using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Soccer;

namespace TokanPages.Persistence.Database.Mappings.Soccer;

[ExcludeFromCodeCoverage]
public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder) 
        => builder.Property(position => position.Id).ValueGeneratedOnAdd();
}