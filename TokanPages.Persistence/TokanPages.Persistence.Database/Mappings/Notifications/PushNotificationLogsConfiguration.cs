using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Notifications;

namespace TokanPages.Persistence.Database.Mappings.Notifications;

[ExcludeFromCodeCoverage]
public class PushNotificationLogsConfiguration : IEntityTypeConfiguration<PushNotificationLog>
{
    public void Configure(EntityTypeBuilder<PushNotificationLog> builder)
        => builder.Property(logs => logs.Id).ValueGeneratedOnAdd();
}