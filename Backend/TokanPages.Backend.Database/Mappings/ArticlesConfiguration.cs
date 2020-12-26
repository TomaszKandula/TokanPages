using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{

    public class ArticlesConfiguration : IEntityTypeConfiguration<Articles>
    {

        public void Configure(EntityTypeBuilder<Articles> AModelBuilder)
        {
            AModelBuilder.Property(e => e.Id).ValueGeneratedNever();
        }

    }

}
