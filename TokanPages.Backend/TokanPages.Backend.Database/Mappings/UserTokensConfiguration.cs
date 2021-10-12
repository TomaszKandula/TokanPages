namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class UserTokensConfiguration : IEntityTypeConfiguration<UserTokens>
    {
        public void Configure(EntityTypeBuilder<UserTokens> ABuilder)
        {
            ABuilder.Property(AUserTokens => AUserTokens.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AUserTokens => AUserTokens.User)
                .WithMany(AUsers => AUsers.UserTokens)
                .HasForeignKey(AUserTokens => AUserTokens.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserTokens_Users");
        }
    }
}