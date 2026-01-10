using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshToken>
{
    public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
    {
        builder.Property(userRefreshTokens => userRefreshTokens.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(userRefreshTokens => userRefreshTokens.Users)
            .WithMany(users => users.UserRefreshTokens)
            .HasForeignKey(userRefreshTokens => userRefreshTokens.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserRefreshTokens_Users");
    }
}