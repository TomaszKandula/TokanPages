using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class PhotoGearsConfiguration : IEntityTypeConfiguration<PhotoGears>
    {
        public void Configure(EntityTypeBuilder<PhotoGears> ABuilder)
        {
            ABuilder.Property(APhotoGears => APhotoGears.Id).ValueGeneratedOnAdd();
            ABuilder.Property(APhotoGears => APhotoGears.Aperture).HasColumnType("decimal(18,2)");
        }
    }
}
