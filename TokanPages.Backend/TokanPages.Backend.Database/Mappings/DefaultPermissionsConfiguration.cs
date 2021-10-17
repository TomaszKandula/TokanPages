namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class DefaultPermissionsConfiguration : IEntityTypeConfiguration<DefaultPermissions>
    {
        public void Configure(EntityTypeBuilder<DefaultPermissions> typeBuilder)
        {
            typeBuilder.Property(defaultPermissions => defaultPermissions.Id).ValueGeneratedOnAdd();
            
            typeBuilder
                .HasOne(defaultPermissions => defaultPermissions.Permission)
                .WithMany(permissions => permissions.DefaultPermissions)
                .HasForeignKey(defaultPermissions => defaultPermissions.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultPermissions_Permissions");

            typeBuilder
                .HasOne(defaultPermissions => defaultPermissions.Role)
                .WithMany(roles => roles.DefaultPermissions)
                .HasForeignKey(defaultPermissions => defaultPermissions.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultPermissions_Roles");
        }
    }
}