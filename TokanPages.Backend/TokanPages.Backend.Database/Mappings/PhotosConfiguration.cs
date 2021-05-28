using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class PhotosConfiguration : IEntityTypeConfiguration<Photos>
    {
        public void Configure(EntityTypeBuilder<Photos> ABuilder)
        {
            ABuilder.Property(APhotos => APhotos.Id).ValueGeneratedOnAdd();

            ABuilder
                .HasOne(APhotos => APhotos.User)
                .WithMany(AUsers => AUsers.Photos)
                .HasForeignKey(APhotos => APhotos.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_Users");
            
            ABuilder
                .HasOne(APhotos => APhotos.PhotoGear)
                .WithMany(APhotoGears => APhotoGears.Photos)
                .HasForeignKey(APhotos => APhotos.PhotoGearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_PhotoGears");
            
            ABuilder
                .HasOne(APhotos => APhotos.PhotoCategory)
                .WithMany(APhotoCategories => APhotoCategories.Photos)
                .HasForeignKey(APhotos => APhotos.PhotoCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_PhotoCategories");
        }
    }
}
