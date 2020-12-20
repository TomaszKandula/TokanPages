using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database
{

    public partial class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DatabaseContext()
        {
        }

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<Subscribers> Subscribers { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder AModelBuilder)
        {
            AModelBuilder.Entity<Articles>(AEntity =>
            {
                AEntity.Property(e => e.Id).ValueGeneratedNever();

                AEntity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(255);

                AEntity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                AEntity.Property(e => e.CreatedAt).HasColumnType("datetime");

                AEntity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            AModelBuilder.Entity<Subscribers>(AEntity =>
            {
                AEntity.Property(e => e.Id).ValueGeneratedNever();

                AEntity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                AEntity.Property(e => e.LastUpdated).HasColumnType("datetime");

                AEntity.Property(e => e.Registered).HasColumnType("datetime");
            });

            AModelBuilder.Entity<Users>(AEntity =>
            {
                AEntity.Property(e => e.Id).ValueGeneratedNever();

                AEntity.Property(e => e.EmailAddress)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                AEntity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                AEntity.Property(e => e.LastLogged).HasColumnType("datetime");

                AEntity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                AEntity.Property(e => e.LastUpdated).HasColumnType("datetime");

                AEntity.Property(e => e.Registered).HasColumnType("datetime");

                AEntity.Property(e => e.UserAlias)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

        }

    }
}
