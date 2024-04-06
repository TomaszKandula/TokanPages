using TokanPages.Backend.Domain.Entities.Photography;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class AlbumsConfiguration : IEntityTypeConfiguration<Albums>
{
    public void Configure(EntityTypeBuilder<Albums> builder)
    {
        builder.Property(albums => albums.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(albums => albums.UserNavigation)
            .WithMany(users => users.AlbumsNavigation)
            .HasForeignKey(albums => albums.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Albums_Users");

        builder
            .HasOne(albums => albums.UserPhotoNavigation)
            .WithMany(photos => photos.AlbumsNavigation)
            .HasForeignKey(albums => albums.UserPhotoId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Albums_UserPhotos");
    }
}