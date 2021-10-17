namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class PhotoGearsConfiguration : IEntityTypeConfiguration<PhotoGears>
    {
        public void Configure(EntityTypeBuilder<PhotoGears> typeBuilder)
        {
            typeBuilder.Property(photoGears => photoGears.Id).ValueGeneratedOnAdd();
            typeBuilder.Property(photoGears => photoGears.Aperture).HasColumnType("decimal(18,2)");
        }
    }
}