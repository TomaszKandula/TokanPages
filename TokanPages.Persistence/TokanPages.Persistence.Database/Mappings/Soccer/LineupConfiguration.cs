using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Soccer;

namespace TokanPages.Persistence.Database.Mappings.Soccer;

[ExcludeFromCodeCoverage]
public class LineupConfiguration : IEntityTypeConfiguration<Lineup>
{
    public void Configure(EntityTypeBuilder<Lineup> builder) 
        => builder.Property(lineup => lineup.Id).ValueGeneratedOnAdd();
}