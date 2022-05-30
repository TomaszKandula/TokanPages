namespace TokanPages.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class PhotosConfiguration : IEntityTypeConfiguration<UserPhotos>
{
    public void Configure(EntityTypeBuilder<UserPhotos> builder)
    {
        builder.Property(photos => photos.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(photos => photos.UserNavigation)
            .WithMany(users => users.UserPhotosNavigation)
            .HasForeignKey(photos => photos.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPhotos_Users");
            
        builder
            .HasOne(photos => photos.PhotoGearNavigation)
            .WithMany(photoGears => photoGears.UserPhotosNavigation)
            .HasForeignKey(photos => photos.PhotoGearId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPhotos_PhotoGears");
            
        builder
            .HasOne(photos => photos.PhotoCategoryNavigation)
            .WithMany(photoCategories => photoCategories.UserPhotosNavigation)
            .HasForeignKey(photos => photos.PhotoCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPhotos_PhotoCategories");
    }
}