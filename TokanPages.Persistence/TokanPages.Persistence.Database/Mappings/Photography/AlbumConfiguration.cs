using TokanPages.Backend.Domain.Entities.Photography;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class AlbumConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.Property(album => album.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(album => album.User)
            .WithMany(user => user.Albums)
            .HasForeignKey(album => album.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(album => album.UserPhoto)
            .WithMany(photo => photo.Albums)
            .HasForeignKey(album => album.UserPhotoId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}