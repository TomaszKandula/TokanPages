using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserMessageCacheConfiguration : IEntityTypeConfiguration<UserMessageCache>
{
    public void Configure(EntityTypeBuilder<UserMessageCache> builder)
        => builder.Property(cache => cache.Id).ValueGeneratedOnAdd();
}