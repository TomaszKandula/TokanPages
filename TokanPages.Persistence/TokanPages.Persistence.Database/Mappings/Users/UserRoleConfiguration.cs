using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.Property(role => role.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(role => role.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(role => role.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRoles_Users");

        builder
            .HasOne(role => role.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(role => role.RoleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRoles_Roles");
    }
}