namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermissions>
    {
        public void Configure(EntityTypeBuilder<UserPermissions> typeBuilder)
        {
            typeBuilder.Property(userPermissions => userPermissions.Id).ValueGeneratedOnAdd();
            
            typeBuilder
                .HasOne(userPermissions => userPermissions.User)
                .WithMany(users => users.UserPermissions)
                .HasForeignKey(userPermissions => userPermissions.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Users");
            
            typeBuilder
                .HasOne(userPermissions => userPermissions.Permission)
                .WithMany(permissions => permissions.UserPermissions)
                .HasForeignKey(userPermissions => userPermissions.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Permissions");
        }
    }
}