using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class DefaultPermissionConfiguration : IEntityTypeConfiguration<DefaultPermission>
{
    public void Configure(EntityTypeBuilder<DefaultPermission> builder)
    {
        builder.Property(permission => permission.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(permission => permission.Permission)
            .WithMany(permission => permission.DefaultPermissions)
            .HasForeignKey(permission => permission.PermissionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DefaultPermissions_Permissions");

        builder
            .HasOne(permission => permission.Role)
            .WithMany(role => role.DefaultPermissions)
            .HasForeignKey(permission => permission.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_DefaultPermissions_Roles");
    }
}