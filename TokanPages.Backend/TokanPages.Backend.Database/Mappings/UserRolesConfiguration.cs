namespace TokanPages.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
{
    public void Configure(EntityTypeBuilder<UserRoles> builder)
    {
        builder.Property(userRoles => userRoles.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userRoles => userRoles.User)
            .WithMany(users => users.UserRoles)
            .HasForeignKey(userRoles => userRoles.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRoles_Users");

        builder
            .HasOne(userRoles => userRoles.Role)
            .WithMany(roles => roles.UserRoles)
            .HasForeignKey(userRoles => userRoles.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRoles_Roles");
    }
}