using TokanPages.Backend.Domain.Entities.Photography;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class AlbumsConfiguration : IEntityTypeConfiguration<Album>
{
    public void Configure(EntityTypeBuilder<Album> builder)
    {
        builder.Property(albums => albums.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(albums => albums.User)
            .WithMany(users => users.Albums)
            .HasForeignKey(albums => albums.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Albums_Users");

        builder
            .HasOne(albums => albums.UserPhoto)
            .WithMany(photos => photos.Albums)
            .HasForeignKey(albums => albums.UserPhotoId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Albums_UserPhotos");
    }
}