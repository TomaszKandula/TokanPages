using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    [ExcludeFromCodeCoverage]
    public class UserPermissionsConfiguration : IEntityTypeConfiguration<UserPermissions>
    {
        public void Configure(EntityTypeBuilder<UserPermissions> ABuilder)
        {
            ABuilder.Property(AUserPermissions => AUserPermissions.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AUserPermissions => AUserPermissions.User)
                .WithMany(AUsers => AUsers.UserPermissions)
                .HasForeignKey(AUserPermissions => AUserPermissions.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Users");
            
            ABuilder
                .HasOne(AUserPermissions => AUserPermissions.Permission)
                .WithMany(APermissions => APermissions.UserPermissions)
                .HasForeignKey(AUserPermissions => AUserPermissions.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserPermissions_Permissions");
        }
    }
}