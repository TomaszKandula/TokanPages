using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    [ExcludeFromCodeCoverage]
    public class AlbumsConfiguration : IEntityTypeConfiguration<Albums>
    {
        public void Configure(EntityTypeBuilder<Albums> ABuilder)
        {
            ABuilder.Property(AAlbums => AAlbums.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AAlbums => AAlbums.User)
                .WithMany(AUsers => AUsers.Albums)
                .HasForeignKey(AAlbums => AAlbums.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Users");

            ABuilder
                .HasOne(AAlbums => AAlbums.Photo)
                .WithMany(APhotos => APhotos.Albums)
                .HasForeignKey(AAlbums => AAlbums.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Photos");
        }
    }
}
