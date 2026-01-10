using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UsersConfiguration : IEntityTypeConfiguration<Backend.Domain.Entities.Users.User>
{
    public void Configure(EntityTypeBuilder<Backend.Domain.Entities.Users.User> builder) 
        => builder.Property(users => users.Id).ValueGeneratedOnAdd();
}