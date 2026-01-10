using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.Property(userPermissions => userPermissions.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userPermissions => userPermissions.Users)
            .WithMany(users => users.UserPermissions)
            .HasForeignKey(userPermissions => userPermissions.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPermissions_Users");
            
        builder
            .HasOne(userPermissions => userPermissions.Permission)
            .WithMany(permissions => permissions.UserPermissions)
            .HasForeignKey(userPermissions => userPermissions.PermissionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPermissions_Permissions");
    }
}