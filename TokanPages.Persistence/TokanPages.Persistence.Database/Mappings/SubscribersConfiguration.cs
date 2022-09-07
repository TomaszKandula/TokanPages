using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class SubscribersConfiguration : IEntityTypeConfiguration<Subscribers>
{
    public void Configure(EntityTypeBuilder<Subscribers> builder)
        => builder.Property(subscribers => subscribers.Id).ValueGeneratedOnAdd();
}