using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.Property(userRefreshTokens => userRefreshTokens.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userRefreshTokens => userRefreshTokens.User)
            .WithMany(users => users.UserRefreshTokens)
            .HasForeignKey(userRefreshTokens => userRefreshTokens.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRefreshTokens_Users");
    }
}