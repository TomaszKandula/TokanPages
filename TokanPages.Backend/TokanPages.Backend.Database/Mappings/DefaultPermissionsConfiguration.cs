namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class DefaultPermissionsConfiguration : IEntityTypeConfiguration<DefaultPermissions>
    {
        public void Configure(EntityTypeBuilder<DefaultPermissions> builder)
        {
            builder.Property(defaultPermissions => defaultPermissions.Id).ValueGeneratedOnAdd();
            
            builder
                .HasOne(defaultPermissions => defaultPermissions.Permission)
                .WithMany(permissions => permissions.DefaultPermissions)
                .HasForeignKey(defaultPermissions => defaultPermissions.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultPermissions_Permissions");

            builder
                .HasOne(defaultPermissions => defaultPermissions.Role)
                .WithMany(roles => roles.DefaultPermissions)
                .HasForeignKey(defaultPermissions => defaultPermissions.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultPermissions_Roles");
        }
    }
}