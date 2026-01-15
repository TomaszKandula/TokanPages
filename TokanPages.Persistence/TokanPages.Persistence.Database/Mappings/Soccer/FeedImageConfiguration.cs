using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Soccer;

namespace TokanPages.Persistence.Database.Mappings.Soccer;

[ExcludeFromCodeCoverage]
public class FeedImageConfiguration : IEntityTypeConfiguration<FeedImage>
{
    public void Configure(EntityTypeBuilder<FeedImage> builder) 
        => builder.Property(feed => feed.Id).ValueGeneratedOnAdd();
}