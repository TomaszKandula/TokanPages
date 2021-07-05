using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    [ExcludeFromCodeCoverage]
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> ABuilder)
        {
            ABuilder.Property(AUserRoles => AUserRoles.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AUserRoles => AUserRoles.User)
                .WithMany(AUsers => AUsers.UserRoles)
                .HasForeignKey(AUserRoles => AUserRoles.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");

            ABuilder
                .HasOne(AUserRoles => AUserRoles.Role)
                .WithMany(ARoles => ARoles.UserRoles)
                .HasForeignKey(AUserRoles => AUserRoles.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");
        }
    }
}