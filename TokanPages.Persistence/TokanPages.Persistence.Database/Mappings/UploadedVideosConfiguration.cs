using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class UploadedVideosConfiguration : IEntityTypeConfiguration<UploadedVideo>
{
    public void Configure(EntityTypeBuilder<UploadedVideo> builder)
        => builder.Property(uploadedVideos => uploadedVideos.Id).ValueGeneratedOnAdd();
}