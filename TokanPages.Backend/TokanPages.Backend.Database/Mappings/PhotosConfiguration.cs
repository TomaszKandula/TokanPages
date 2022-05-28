namespace TokanPages.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class PhotosConfiguration : IEntityTypeConfiguration<Photos>
{
    public void Configure(EntityTypeBuilder<Photos> builder)
    {
        builder.Property(photos => photos.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(photos => photos.UserNavigation)
            .WithMany(users => users.PhotosNavigation)
            .HasForeignKey(photos => photos.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Photos_Users");
            
        builder
            .HasOne(photos => photos.PhotoGearNavigation)
            .WithMany(photoGears => photoGears.PhotosNavigation)
            .HasForeignKey(photos => photos.PhotoGearId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Photos_PhotoGears");
            
        builder
            .HasOne(photos => photos.PhotoCategoryNavigation)
            .WithMany(photoCategories => photoCategories.PhotosNavigation)
            .HasForeignKey(photos => photos.PhotoCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Photos_PhotoCategories");
    }
}