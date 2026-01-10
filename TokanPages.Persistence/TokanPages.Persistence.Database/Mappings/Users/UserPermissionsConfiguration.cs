using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.Property(permission => permission.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(permission => permission.User)
            .WithMany(user => user.UserPermissions)
            .HasForeignKey(permission => permission.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPermissions_Users");
            
        builder
            .HasOne(permission => permission.Permission)
            .WithMany(permission => permission.UserPermissions)
            .HasForeignKey(permission => permission.PermissionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPermissions_Permissions");
    }
}