using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshTokens>
{
    public void Configure(EntityTypeBuilder<UserRefreshTokens> builder)
    {
        builder.Property(userRefreshTokens => userRefreshTokens.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userRefreshTokens => userRefreshTokens.UserNavigation)
            .WithMany(users => users.UserRefreshTokensNavigation)
            .HasForeignKey(userRefreshTokens => userRefreshTokens.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRefreshTokens_Users");
    }
}