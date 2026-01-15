using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.Database.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class PhotoConfiguration : IEntityTypeConfiguration<UserPhoto>
{
    public void Configure(EntityTypeBuilder<UserPhoto> builder)
    {
        builder.Property(photo => photo.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(photo => photo.User)
            .WithMany(user => user.UserPhotos)
            .HasForeignKey(photo => photo.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            
        builder
            .HasOne(photo => photo.PhotoGear)
            .WithMany(gear => gear.UserPhotos)
            .HasForeignKey(photo => photo.PhotoGearId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            
        builder
            .HasOne(photo => photo.PhotoCategory)
            .WithMany(category => category.UserPhotos)
            .HasForeignKey(photo => photo.PhotoCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}