using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Notifications;

namespace TokanPages.Persistence.DataAccess.Mappings.Notifications;

[ExcludeFromCodeCoverage]
public class PushNotificationLogConfiguration : IEntityTypeConfiguration<PushNotificationLog>
{
    public void Configure(EntityTypeBuilder<PushNotificationLog> builder)
        => builder.Property(log => log.Id).ValueGeneratedOnAdd();
}