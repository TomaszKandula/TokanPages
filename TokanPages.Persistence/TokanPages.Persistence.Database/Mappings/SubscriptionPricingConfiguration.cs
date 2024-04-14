using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class SubscriptionPricingConfiguration : IEntityTypeConfiguration<SubscriptionPricing>
{
    public void Configure(EntityTypeBuilder<SubscriptionPricing> builder) 
        => builder.Property(pricing => pricing.Id).ValueGeneratedOnAdd();
}