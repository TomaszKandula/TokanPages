using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class AlbumsConfiguration : IEntityTypeConfiguration<Albums>
    {
        public void Configure(EntityTypeBuilder<Albums> AModelBuilder)
        {
            AModelBuilder.Property(AAlbums => AAlbums.Id).ValueGeneratedOnAdd();
            
            AModelBuilder
                .HasOne(AAlbums => AAlbums.User)
                .WithMany(AUsers => AUsers.Albums)
                .HasForeignKey(AAlbums => AAlbums.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Users");

            AModelBuilder
                .HasOne(AAlbums => AAlbums.Photo)
                .WithMany(APhotos => APhotos.Albums)
                .HasForeignKey(AAlbums => AAlbums.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Photos");
        }
    }
}
