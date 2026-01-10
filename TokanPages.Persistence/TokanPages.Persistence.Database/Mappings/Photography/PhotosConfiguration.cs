using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.Database.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class PhotosConfiguration : IEntityTypeConfiguration<UserPhoto>
{
    public void Configure(EntityTypeBuilder<UserPhoto> builder)
    {
        builder.Property(photos => photos.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(photos => photos.User)
            .WithMany(users => users.UserPhotos)
            .HasForeignKey(photos => photos.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPhotos_Users");
            
        builder
            .HasOne(photos => photos.PhotoGear)
            .WithMany(photoGears => photoGears.UserPhotos)
            .HasForeignKey(photos => photos.PhotoGearId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPhotos_PhotoGears");
            
        builder
            .HasOne(photos => photos.PhotoCategory)
            .WithMany(photoCategories => photoCategories.UserPhotos)
            .HasForeignKey(photos => photos.PhotoCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPhotos_PhotoCategories");
    }
}