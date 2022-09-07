using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermissions>
{
    public void Configure(EntityTypeBuilder<UserPermissions> builder)
    {
        builder.Property(userPermissions => userPermissions.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userPermissions => userPermissions.UserNavigation)
            .WithMany(users => users.UserPermissionsNavigation)
            .HasForeignKey(userPermissions => userPermissions.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPermissions_Users");
            
        builder
            .HasOne(userPermissions => userPermissions.PermissionNavigation)
            .WithMany(permissions => permissions.UserPermissionsNavigation)
            .HasForeignKey(userPermissions => userPermissions.PermissionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPermissions_Permissions");
    }
}