using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.DataAccess.Mappings;

[ExcludeFromCodeCoverage]
public class UploadedVideoConfiguration : IEntityTypeConfiguration<UploadedVideo>
{
    public void Configure(EntityTypeBuilder<UploadedVideo> builder)
        => builder.Property(video => video.Id).ValueGeneratedOnAdd();
}