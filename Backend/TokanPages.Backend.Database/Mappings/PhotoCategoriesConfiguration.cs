using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class PhotoCategoriesConfiguration : IEntityTypeConfiguration<PhotoCategories>
    {
        public void Configure(EntityTypeBuilder<PhotoCategories> AModelBuilder)
        {
            AModelBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
        }
    }
}
