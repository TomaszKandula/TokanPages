namespace TokanPages.Backend.Database.Mappings
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    public class UserRefreshTokensConfiguration : IEntityTypeConfiguration<UserRefreshTokens>
    {
        public void Configure(EntityTypeBuilder<UserRefreshTokens> ABuilder)
        {
            ABuilder.Property(AUserRefreshTokens => AUserRefreshTokens.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AUserRefreshTokens => AUserRefreshTokens.User)
                .WithMany(AUsers => AUsers.UserRefreshTokens)
                .HasForeignKey(AUserRefreshTokens => AUserRefreshTokens.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRefreshTokens_Users");
        }
    }
}