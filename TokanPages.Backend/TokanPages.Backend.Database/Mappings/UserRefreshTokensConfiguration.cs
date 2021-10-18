namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshTokens>
    {
        public void Configure(EntityTypeBuilder<UserRefreshTokens> builder)
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
}