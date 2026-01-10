using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class UsersConfiguration : IEntityTypeConfiguration<Backend.Domain.Entities.User.User>
{
    public void Configure(EntityTypeBuilder<Backend.Domain.Entities.User.User> builder) 
        => builder.Property(users => users.Id).ValueGeneratedOnAdd();
}