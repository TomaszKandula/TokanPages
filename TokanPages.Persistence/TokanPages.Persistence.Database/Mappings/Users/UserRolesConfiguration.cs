using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.Property(userRoles => userRoles.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userRoles => userRoles.UserNavigation)
            .WithMany(users => users.UserRolesNavigation)
            .HasForeignKey(userRoles => userRoles.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRoles_Users");

        builder
            .HasOne(userRoles => userRoles.RoleNavigation)
            .WithMany(roles => roles.UserRolesNavigation)
            .HasForeignKey(userRoles => userRoles.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRoles_Roles");
    }
}