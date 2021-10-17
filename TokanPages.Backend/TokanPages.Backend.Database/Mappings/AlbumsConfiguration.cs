namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class AlbumsConfiguration : IEntityTypeConfiguration<Albums>
    {
        public void Configure(EntityTypeBuilder<Albums> typeBuilder)
        {
            typeBuilder.Property(albums => albums.Id).ValueGeneratedOnAdd();
            
            typeBuilder
                .HasOne(albums => albums.User)
                .WithMany(users => users.Albums)
                .HasForeignKey(albums => albums.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Users");

            typeBuilder
                .HasOne(albums => albums.Photo)
                .WithMany(photos => photos.Albums)
                .HasForeignKey(albums => albums.PhotoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Albums_Photos");
        }
    }
}