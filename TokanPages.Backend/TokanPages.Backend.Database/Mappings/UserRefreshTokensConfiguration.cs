namespace TokanPages.Backend.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshTokens>
    {
        public void Configure(EntityTypeBuilder<UserRefreshTokens> typeBuilder)
        {
            typeBuilder.Property(userRefreshTokens => userRefreshTokens.Id).ValueGeneratedOnAdd();
            
            typeBuilder
                .HasOne(userRefreshTokens => userRefreshTokens.User)
                .WithMany(users => users.UserRefreshTokens)
                .HasForeignKey(userRefreshTokens => userRefreshTokens.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRefreshTokens_Users");
        }
    }
}