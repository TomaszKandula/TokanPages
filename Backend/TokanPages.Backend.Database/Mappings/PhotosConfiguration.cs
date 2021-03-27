using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class PhotosConfiguration : IEntityTypeConfiguration<Photos>
    {
        public void Configure(EntityTypeBuilder<Photos> AModelBuilder)
        {
            AModelBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
            AModelBuilder
                .HasOne(e => e.User)
                .WithMany(d => d.Photos)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_Users");
            
            AModelBuilder
                .HasOne(e => e.PhotoGear)
                .WithMany(d => d.Photos)
                .HasForeignKey(e => e.PhotoGearId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_PhotoGears");
            
            AModelBuilder
                .HasOne(e => e.PhotoCategory)
                .WithMany(d => d.Photos)
                .HasForeignKey(e => e.PhotoCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Photos_PhotoCategories");
        }
    }
}
