namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class PhotoGearsConfiguration : IEntityTypeConfiguration<PhotoGears>
    {
        public void Configure(EntityTypeBuilder<PhotoGears> ABuilder)
        {
            ABuilder.Property(APhotoGears => APhotoGears.Id).ValueGeneratedOnAdd();
            ABuilder.Property(APhotoGears => APhotoGears.Aperture).HasColumnType("decimal(18,2)");
        }
    }
}