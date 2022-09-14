using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class DefaultPermissionsConfiguration : IEntityTypeConfiguration<DefaultPermissions>
{
    public void Configure(EntityTypeBuilder<DefaultPermissions> builder)
    {
        builder.Property(defaultPermissions => defaultPermissions.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(defaultPermissions => defaultPermissions.PermissionNavigation)
            .WithMany(permissions => permissions.DefaultPermissionsNavigation)
            .HasForeignKey(defaultPermissions => defaultPermissions.PermissionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DefaultPermissions_Permissions");

        builder
            .HasOne(defaultPermissions => defaultPermissions.RoleNavigation)
            .WithMany(roles => roles.DefaultPermissionsNavigation)
            .HasForeignKey(defaultPermissions => defaultPermissions.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DefaultPermissions_Roles");
    }
}