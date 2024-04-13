using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.Notification;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Notification;

[ExcludeFromCodeCoverage]
public class PushNotificationLogsConfiguration : IEntityTypeConfiguration<PushNotificationLog>
{
    public void Configure(EntityTypeBuilder<PushNotificationLog> builder)
        => builder.Property(logs => logs.Id).ValueGeneratedOnAdd();
}