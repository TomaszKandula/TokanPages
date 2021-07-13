namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class DefaultPermissionsConfiguration : IEntityTypeConfiguration<DefaultPermissions>
    {
        public void Configure(EntityTypeBuilder<DefaultPermissions> ABuilder)
        {
            ABuilder.Property(ADefaultPermissions => ADefaultPermissions.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(ADefaultPermissions => ADefaultPermissions.Permission)
                .WithMany(APermission => APermission.DefaultPermissions)
                .HasForeignKey(ADefaultPermissions => ADefaultPermissions.PermissionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultPermissions_Permissions");

            ABuilder
                .HasOne(ADefaultPermissions => ADefaultPermissions.Role)
                .WithMany(ARoles => ARoles.DefaultPermissions)
                .HasForeignKey(ADefaultPermissions => ADefaultPermissions.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DefaultPermissions_Roles");
        }
    }
}