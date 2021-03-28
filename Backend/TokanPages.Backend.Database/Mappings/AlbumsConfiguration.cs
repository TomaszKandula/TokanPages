using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class AlbumsConfiguration : IEntityTypeConfiguration<Albums>
    {
        public void Configure(EntityTypeBuilder<Albums> AModelBuilder)
        {
            AModelBuilder.Property(e => e.Id).ValueGeneratedOnAdd();
            AModelBuilder
                .HasOne(e => e.User)
                .WithMany(d => d.Albums)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Users");

            AModelBuilder
                .HasOne(e => e.Photo)
                .WithMany(d => d.Albums)
                .HasForeignKey(e => e.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Photos");
        }
    }
}
