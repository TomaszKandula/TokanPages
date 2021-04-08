using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class PhotosConfiguration : IEntityTypeConfiguration<Photos>
    {
        public void Configure(EntityTypeBuilder<Photos> AModelBuilder)
        {
            AModelBuilder.Property(APhotos => APhotos.Id).ValueGeneratedOnAdd();

            AModelBuilder
                .HasOne(APhotos => APhotos.User)
                .WithMany(AUsers => AUsers.Photos)
                .HasForeignKey(APhotos => APhotos.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_Users");
            
            AModelBuilder
                .HasOne(APhotos => APhotos.PhotoGear)
                .WithMany(APhotoGears => APhotoGears.Photos)
                .HasForeignKey(APhotos => APhotos.PhotoGearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_PhotoGears");
            
            AModelBuilder
                .HasOne(APhotos => APhotos.PhotoCategory)
                .WithMany(APhotoCategories => APhotoCategories.Photos)
                .HasForeignKey(APhotos => APhotos.PhotoCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_PhotoCategories");
        }
    }
}
