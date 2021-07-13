namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class SubscribersConfiguration : IEntityTypeConfiguration<Subscribers>
    {
        public void Configure(EntityTypeBuilder<Subscribers> ABuilder)
            => ABuilder.Property(ASubscribers => ASubscribers.Id).ValueGeneratedOnAdd();
    }
}