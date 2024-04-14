using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class NewslettersConfiguration : IEntityTypeConfiguration<Newsletters>
{
    public void Configure(EntityTypeBuilder<Newsletters> builder)
        => builder.Property(subscribers => subscribers.Id).ValueGeneratedOnAdd();
}